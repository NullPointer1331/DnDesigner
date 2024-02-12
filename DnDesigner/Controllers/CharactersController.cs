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

        // GET: Characters/Details/5
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
            if (ModelState.IsValid)
            {
                Background background = await _dbHelper.GetBackground(character.BackgroundId);
                Race race = await _dbHelper.GetRace(character.RaceId);

                // This is to make sure all characters have all saving throws and skills even if they aren't proficient in them
                // There's probably a better way to do this
                List<Proficiency> defaultProficiencies = await _context.Proficiencies
                    .Where(p => p.Type == "saving throw" || p.Type == "skill")
                    .ToListAsync(); 
                
                Character newCharacter = new Character(character, race, background, defaultProficiencies, 
                    character.Alignment, User.FindFirstValue(ClaimTypes.NameIdentifier));

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

                foreach (string error in newCharacter.GetMajorErrors())
                {
					ModelState.AddModelError("Model", error);
				}

                List<string> minorErrors = newCharacter.GetMinorErrors();
				if (minorErrors.Any())
                {
                    TempData["MinorErrors"] = minorErrors;
                }

                if (ModelState.IsValid)
                {
                    _context.Add(newCharacter);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("FeatureChoices", new { id = newCharacter.CharacterId });
                }
            }
            character.AvailableClasses = await _dbHelper.GetAllClasses();
            character.AvailableBackgrounds = await _dbHelper.GetAllBackgrounds();
            character.AvailableRaces = await _dbHelper.GetAllRaces();
            return View(character);
            
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
				IgnoreLimits = character.IgnoreLimits,
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
				character.IgnoreLimits = characterViewModel.IgnoreLimits;
				character.Background = background;
				character.Race = race;
				character.Classes = classes;
				character.Name = characterViewModel.Name;
				character.MaxHealth = characterViewModel.MaxHealth;
				character.BaseStrength = characterViewModel.Strength;
				character.BaseDexterity = characterViewModel.Dexterity;
				character.BaseConstitution = characterViewModel.Constitution;
				character.BaseIntelligence = characterViewModel.Intelligence;
				character.BaseWisdom = characterViewModel.Wisdom;
				character.BaseCharisma = characterViewModel.Charisma;
				character.d6HitDiceAvailable = character.MaxHitDice[0];
				character.d8HitDiceAvailable = character.MaxHitDice[1];
				character.d10HitDiceAvailable = character.MaxHitDice[2];
				character.d12HitDiceAvailable = character.MaxHitDice[3];

				character.SetActiveFeatures();

				foreach (string error in character.GetMajorErrors())
				{
					ModelState.AddModelError("Model", error);
				}

				List<string> minorErrors = character.GetMinorErrors();
				if (minorErrors.Any())
				{
					TempData["MinorErrors"] = minorErrors;
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
					return RedirectToAction("FeatureChoices", new { id });
				}
			}
			characterViewModel.AvailableClasses = await _dbHelper.GetAllClasses();
			characterViewModel.AvailableBackgrounds = await _dbHelper.GetAllBackgrounds();
			characterViewModel.AvailableRaces = await _dbHelper.GetAllRaces();
			return View(characterViewModel);
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
                CharacterFeatures = character.Features.Where(f => f.Feature is not SelectableFeature).ToList(),
                ChoiceValues = new Dictionary<int, int>(),
                FeatsOnly = false
            };
            foreach (CharacterFeature feature in featureChoiceViewModel.CharacterFeatures)
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

				foreach (string error in character.GetMajorErrors())
				{
					ModelState.AddModelError("Model", error);
				}

				List<string> minorErrors = character.GetMinorErrors();
				if (minorErrors.Any())
				{
					TempData["MinorErrors"] = minorErrors;
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
                    if (character.Features.Where(f => f.Feature is SelectableFeature).Any())
                    {
                        return RedirectToAction("FeatChoices", new { id = character.CharacterId });
                    }
                    else
                    {
                        return RedirectToAction("CharacterSheet", new { id = character.CharacterId });
                    }
                }
            }
            featureChoiceViewModel = new FeatureChoiceViewModel()
            {
                CharacterId = character.CharacterId,
                CharacterFeatures = character.Features.Where(f => f.Feature is not SelectableFeature).ToList(),
                ChoiceValues = new Dictionary<int, int>(),
                FeatsOnly = false
            };
            foreach (CharacterFeature feature in featureChoiceViewModel.CharacterFeatures)
            {
                foreach (CharacterChoice choice in feature.Choices)
                {
                    featureChoiceViewModel.ChoiceValues.Add(choice.CharacterChoiceId, choice.ChoiceValue);
                }
            }
            return View(featureChoiceViewModel);
        }

        public async Task<IActionResult> FeatChoices(int id)
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
                CharacterFeatures = character.Features.Where(f => f.Feature is SelectableFeature).ToList(),
                ChoiceValues = new Dictionary<int, int>(),
                FeatsOnly = true
            };
            foreach (CharacterFeature feature in featureChoiceViewModel.CharacterFeatures)
            {
                foreach (CharacterChoice choice in feature.Choices)
                {
                    featureChoiceViewModel.ChoiceValues.Add(choice.CharacterChoiceId, choice.ChoiceValue);
                }
            }
            return View("FeatureChoices", featureChoiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeatChoices(int id, FeatureChoiceViewModel featureChoiceViewModel)
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
            foreach (KeyValuePair<int, int> choice in featureChoiceViewModel.ChoiceValues)
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

				foreach (string error in character.GetMajorErrors())
				{
					ModelState.AddModelError("Model", error);
				}

				List<string> minorErrors = character.GetMinorErrors();
				if (minorErrors.Any())
				{
					TempData["MinorErrors"] = minorErrors;
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
            featureChoiceViewModel = new FeatureChoiceViewModel()
            {
                CharacterId = character.CharacterId,
                CharacterFeatures = character.Features.Where(f => f.Feature is not SelectableFeature).ToList(),
                ChoiceValues = new Dictionary<int, int>(),
                FeatsOnly = false
            };
            foreach (CharacterFeature feature in featureChoiceViewModel.CharacterFeatures)
            {
                foreach (CharacterChoice choice in feature.Choices)
                {
                    featureChoiceViewModel.ChoiceValues.Add(choice.CharacterChoiceId, choice.ChoiceValue);
                }
            }
            return View("FeatureChoices", featureChoiceViewModel);
        }

        // GET: Characters/Edit/5
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
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Characters/Delete/5
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

        // POST: Characters/Delete/5
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
