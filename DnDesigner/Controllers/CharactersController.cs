using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DnDesigner.Data;
using DnDesigner.Models;
using System.Security.Claims;

namespace DnDesigner.Controllers
{
    public class CharactersController : Controller
    {
        private readonly DnDesignerDbContext _context;
        private readonly IDBHelper _dbHelper;

        public CharactersController(DnDesignerDbContext context, IDBHelper dBHelper)
        {
            _context = context;
            _dbHelper = dBHelper;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            // checks if the user is logged in, if not redirects to login page
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            } 
            else 
            {
                return View
                    (
                        await _dbHelper.GetAllCharacters(User.FindFirstValue(ClaimTypes.NameIdentifier))
                    );
            }
        }

        // GET: Characters/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Characters == null)
            {
                return NotFound();
            }

            var character = await _dbHelper.GetCharacter((int)id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // checks if the user is logged in, if not redirects to login page
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            CreateCharacterViewModel characterViewModel = new()
            {
                AvailableClasses = await _dbHelper.GetAllClasses(),
                AvailableBackgrounds = await _dbHelper.GetAllBackgrounds(),
                AvailableRaces = await _dbHelper.GetAllRaces(),
                Classes = new List<int[]>()
            };
            while (characterViewModel.Classes.Count < characterViewModel.AvailableClasses.Count)
            {
                characterViewModel.Classes.Add(new int[] { 0, 
                    characterViewModel.AvailableClasses[0].ClassId, 
                    characterViewModel.AvailableClasses[0].Subclasses[0].SubclassId });
            }
            characterViewModel.Classes[0][0] = 1;
            return View(characterViewModel);
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCharacterViewModel character)
        {
            int totalLevel = 0;
            for(int i = 0; i < character.Classes.Count; i++)
            {
                totalLevel += character.Classes[i][0];
                for(int j = i + 1; j < character.Classes.Count; j++)
                {
                    if (character.Classes[i][1] == character.Classes[j][1] && character.Classes[i][0] != 0 && character.Classes[j][0] != 0)
                    {
                        ModelState.AddModelError("Classes", "You cannot have multiple instances of the same class.");
                    }
                }
            }
            if (totalLevel > 20)
            {
                ModelState.AddModelError("Classes", "You cannot have more than 20 levels.");
            }

            if (ModelState.IsValid)
            {
                Background background = await _dbHelper.GetBackground(character.BackgroundId);
                Race race = await _dbHelper.GetRace(character.RaceId);

                // This is to make sure all characters have all saving throws and skills even if they aren't proficient in them
                // There's probably a better way to do this
                List<Proficiency> defaultProficiencies = await _context.Proficiencies
                    .Where(p => p.Type == "saving throw" || p.Type == "skill")
                    .ToListAsync(); 
                
                Character newCharacter = new Character(character, race, background, defaultProficiencies, character.Alignment, User.FindFirstValue(ClaimTypes.NameIdentifier));

                for(int i = 0; i < character.Classes.Count; i++)
                {
                    if (character.Classes[i][0] > 0)
                    {
                        Class newClass = await _dbHelper.GetClass(character.Classes[i][1]);
                        if (character.Classes[i][0] >= newClass.SubclassLevel)
                        {
                            Subclass subclass = await _dbHelper.GetSubclass(character.Classes[i][2]);
                            newCharacter.Classes.Add(new CharacterClass(newCharacter, newClass, subclass, character.Classes[i][0]));
                        }
                        else
                        {
                            newCharacter.Classes.Add(new CharacterClass(newCharacter, newClass, character.Classes[i][0]));
                        }
                    }
                }
                newCharacter.d6HitDiceAvailable = newCharacter.MaxHitDice[0];
                newCharacter.d8HitDiceAvailable = newCharacter.MaxHitDice[1];
                newCharacter.d10HitDiceAvailable = newCharacter.MaxHitDice[2];
                newCharacter.d12HitDiceAvailable = newCharacter.MaxHitDice[3];
                newCharacter.Classes[0].InitialClass = true;
                newCharacter.SetActiveFeatures();

                _context.Add(newCharacter);
                await _context.SaveChangesAsync();

                return RedirectToAction("FeatureChoices", new { id = newCharacter.CharacterId });
            }
            character.AvailableClasses = await _dbHelper.GetAllClasses();
            character.AvailableBackgrounds = await _dbHelper.GetAllBackgrounds();
            character.AvailableRaces = await _dbHelper.GetAllRaces();
            return View(character);
            
        }

        public async Task<IActionResult> FeatureChoices(int id)
        {
            Character character = await _dbHelper.GetCharacter(id);
            if (character == null)
            {
                return NotFound();
            }
            if (character.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            character.RemoveEffects();
            FeatureChoiceViewModel featureChoiceViewModel = new FeatureChoiceViewModel()
            {
                CharacterId = character.CharacterId,
                CharacterFeatures = character.Features,
                ChoiceValues = new Dictionary<int, int>()
            };
            foreach (CharacterFeature feature in character.Features)
            {
                foreach (CharacterChoice choice in feature.Choices)
                {
                    featureChoiceViewModel.ChoiceValues.Add(choice.CharacterChoiceId, choice.ChoiceValue);
                }
            }
            return View(featureChoiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeatureChoices(int id, FeatureChoiceViewModel featureChoiceViewModel)
        {
            Character character = await _dbHelper.GetCharacter(id);
            if (character == null)
            {
                return NotFound();
            }
            if (character.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            foreach(KeyValuePair<int, int> choice in featureChoiceViewModel.ChoiceValues)
            {
                CharacterChoice? characterChoice = character.GetCharacterChoice(choice.Key);
                if (characterChoice != null)
                {
                    characterChoice.ChoiceValue = choice.Value;
                }
            }
            ModelState.Remove("Character.Background");
            ModelState.Remove("Character.Race");
            if (ModelState.IsValid)
            {
                character.ApplyEffects();
                if (character.Strength < 1 || character.Strength > 20)
                {
                    ModelState.AddModelError("Character.Strength", $"A Strength score of {character.Strength} is invalid. Strength must be between 1 and 20.");
                }
                else if (character.Dexterity < 1 || character.Dexterity > 20)
                {
                    ModelState.AddModelError("Character.Dexterity", $"A Dexterity score of {character.Dexterity} is invalid. Dexterity must be between 1 and 20.");
                }
                else if (character.Constitution < 1 || character.Constitution > 20)
                {
                    ModelState.AddModelError("Character.Constitution", $"A Constitution score of {character.Constitution} is invalid. Constitution must be between 1 and 20.");
                }
                else if (character.Intelligence < 1 || character.Intelligence > 20)
                {
                    ModelState.AddModelError("Character.Intelligence", $"An Intelligence score of {character.Intelligence} is invalid. Intelligence must be between 1 and 20.");
                }
                else if (character.Wisdom < 1 || character.Wisdom > 20)
                {
                    ModelState.AddModelError("Character.Wisdom", $"A Wisdom score of {character.Wisdom} is invalid. Wisdom must be between 1 and 20.");
                }
                else if (character.Charisma < 1 || character.Charisma > 20)
                {
                    ModelState.AddModelError("Character.Charisma", $"A Charisma score of {character.Charisma} is invalid. Charisma must be between 1 and 20.");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(character);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CharacterExists(character.CharacterId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("CharacterSheet", new { id = character.CharacterId });
                }
            }
            return View(featureChoiceViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Character character = await _dbHelper.GetCharacter(id);
            if (character == null)
            {
                return NotFound();
            }
            if (character.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            character.RemoveEffects();
            CreateCharacterViewModel characterViewModel = new CreateCharacterViewModel()
            {
                AvailableClasses = await _dbHelper.GetAllClasses(),
                AvailableBackgrounds = await _dbHelper.GetAllBackgrounds(),
                AvailableRaces = await _dbHelper.GetAllRaces(),
                Classes = new List<int[]>(),
                RaceId = character.Race.RaceId,
                BackgroundId = character.Background.BackgroundId,
                Name = character.Name,
                MaxHealth = character.MaxHealth,
                Strength = character.Strength,
                Dexterity = character.Dexterity,
                Constitution = character.Constitution,
                Intelligence = character.Intelligence,
                Wisdom = character.Wisdom,
                Charisma = character.Charisma,
                Alignment = character.Alignment
            };
            foreach (CharacterClass characterClass in character.Classes)
            {
                characterViewModel.Classes.Add(new int[] { characterClass.Level, characterClass.Class.ClassId, characterClass.Subclass?.SubclassId ?? 0 });
            }
            while (characterViewModel.Classes.Count < characterViewModel.AvailableClasses.Count)
            {
                characterViewModel.Classes.Add(new int[] { 0, 0, 0 });
            }
            return View(characterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateCharacterViewModel characterViewModel)
        {
            Character character = await _dbHelper.GetCharacter(id);
            if (id != character.CharacterId)
            {
                return NotFound();
            }
            int totalLevel = 0;
            for (int i = 0; i < characterViewModel.Classes.Count; i++)
            {
                totalLevel += characterViewModel.Classes[i][0];
                for (int j = i + 1; j < characterViewModel.Classes.Count; j++)
                {
                    if (characterViewModel.Classes[i][1] == characterViewModel.Classes[j][1] 
                        && characterViewModel.Classes[i][0] != 0 && characterViewModel.Classes[j][0] != 0)
                    {
                        ModelState.AddModelError("Classes", "You cannot have multiple instances of the same class.");
                    }
                }
            }
            if (totalLevel > 20)
            {
                ModelState.AddModelError("Classes", "You cannot have more than 20 levels.");
            }
            if (ModelState.IsValid)
            {
                character.RemoveEffects();
                Background background = await _dbHelper.GetBackground(characterViewModel.BackgroundId);
                Race race = await _dbHelper.GetRace(characterViewModel.RaceId);
                List<CharacterClass> classes = new List<CharacterClass>();
                for (int i = 0; i < characterViewModel.Classes.Count; i++)
                {
                    if (characterViewModel.Classes[i][0] > 0)
                    {
                        Class newClass = await _dbHelper.GetClass(characterViewModel.Classes[i][1]);
                        if (characterViewModel.Classes[i][0] >= newClass.SubclassLevel)
                        {
                            Subclass subclass = await _dbHelper.GetSubclass(characterViewModel.Classes[i][2]);
                            classes.Add(new CharacterClass(character, newClass, subclass, characterViewModel.Classes[i][0]));
                        }
                        else
                        {
                            classes.Add(new CharacterClass(character, newClass, characterViewModel.Classes[i][0]));
                        }
                    }
                }
                character.Background = background;
                character.Race = race;
                character.Classes = classes;
                character.Name = characterViewModel.Name;
                character.MaxHealth = characterViewModel.MaxHealth;
                character.Strength = characterViewModel.Strength;
                character.Dexterity = characterViewModel.Dexterity;
                character.Constitution = characterViewModel.Constitution;
                character.Intelligence = characterViewModel.Intelligence;
                character.Wisdom = characterViewModel.Wisdom;
                character.Charisma = characterViewModel.Charisma;
                character.d6HitDiceAvailable = character.MaxHitDice[0];
                character.d8HitDiceAvailable = character.MaxHitDice[1];
                character.d10HitDiceAvailable = character.MaxHitDice[2];
                character.d12HitDiceAvailable = character.MaxHitDice[3];

                character.SetActiveFeatures();
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.CharacterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FeatureChoices", new { id });
            }
            characterViewModel.AvailableClasses = await _dbHelper.GetAllClasses();
            characterViewModel.AvailableBackgrounds = await _dbHelper.GetAllBackgrounds();
            characterViewModel.AvailableRaces = await _dbHelper.GetAllRaces();
            return View(characterViewModel);
        }

        // GET: Characters/CharacterSheet
        public async Task<IActionResult> CharacterSheet(int id)
        {
            Character character = await _dbHelper.GetCharacter(id);
            
            if (character == null)
            {
                return NotFound();
            }
            if (character.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            List<Item> allItems = await _dbHelper.GetAllItems();
            CharacterSheetViewModel characterSheetViewModel = new CharacterSheetViewModel();
            characterSheetViewModel.Character = character;
            characterSheetViewModel.AllItems = allItems;
            return View(characterSheetViewModel);
        }

        // Add Item to Character Inventory
        [HttpPost]
        public async Task AddItem(int characterId, int itemId, int quantity)
        {
            Character character = await _dbHelper.GetCharacter(characterId);

            if (character == null)
            {
                return;
            }
            if (character.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return;
            }

            Item item = await _dbHelper.GetItem(itemId);

            if (item == null)
            {
                return;
            }
            character.Inventory.AddItem(item, quantity);
            await _context.SaveChangesAsync();
            return;
        }

        // POST: Characters/CharacterSheet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CharacterSheet(int id, Character character)
        {
            if (id != character.CharacterId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    character.Background = await _dbHelper.GetBackground(character.Background.BackgroundId);
                    character.Race = await _dbHelper.GetRace(character.Race.RaceId);
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.CharacterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            character = await _dbHelper.GetCharacter(id);
            return View(character);
        }

        // GET: Characters/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Characters == null)
            {
                return NotFound();
            }

            var character = await _dbHelper.GetCharacter((int) id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null || _context.Characters == null)
            {
                return Problem("Entity set 'DnDesignerDbContext.Characters'  is null.");
            }
            var character = await _dbHelper.GetCharacter((int) id);
            if (character != null)
            {
                _context.Characters.Remove(character);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
          return (_context.Characters?.Any(e => e.CharacterId == id)).GetValueOrDefault();
        }
    }
}
