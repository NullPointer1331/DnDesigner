using DnDesigner.Data;
using DnDesigner.Models;
using DnDesigner.Models.ImportModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDesigner.Controllers
{
    public class DataController : Controller
    {
        private readonly DnDesignerDbContext _context;

        public DataController(DnDesignerDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewProficiencies()
        {
            return View(await _context.Proficiencies.ToListAsync());
        }
        public async Task<IActionResult> ViewItems()
        {
            return View(await _context.Items.ToListAsync());
        }
        public async Task<IActionResult> ViewSpells()
        {
            return View(await _context.Spells.ToListAsync());
        }
        public async Task<IActionResult> ViewBackgrounds()
        {
            return View(await _context.Backgrounds.ToListAsync());
        }
        public async Task<IActionResult> ViewRaces()
        {
            return View(await _context.Races.ToListAsync());
        }
        public async Task<IActionResult> ViewClasses()
        {
            return View(await _context.Classes.ToListAsync());
        }
        public async Task<IActionResult> DisplayClass(int id)
        {
            Class? @class = await _context.Classes.FindAsync(id);
            if(@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }
        public async Task<IActionResult> ViewSubclasses()
        {
            return View(await _context.Subclasses.ToListAsync());
        }
        public async Task<IActionResult> ViewFeatures()
        {
            List<Feature> features = new List<Feature>();
            features.AddRange(await _context.ClassFeatures.ToListAsync());
            features.AddRange(await _context.SubclassFeatures.ToListAsync());
            features.AddRange(await _context.BackgroundFeatures.ToListAsync());
            features.AddRange(await _context.RaceFeatures.ToListAsync());
            return View(features);
        }
        public async Task<IActionResult> Import()
        {
            // TODO: do this better
            // Removing existing data also removes all characters
            _context.Proficiencies.RemoveRange(_context.Proficiencies);
            _context.Items.RemoveRange(_context.Items);
            _context.Spells.RemoveRange(_context.Spells);
            _context.Backgrounds.RemoveRange(_context.Backgrounds);
            _context.Races.RemoveRange(_context.Races);
            _context.Classes.RemoveRange(_context.Classes);
            _context.Subclasses.RemoveRange(_context.Subclasses);
            _context.Actions.RemoveRange(_context.Actions);

            List<Item> items = ImportData.ExtractItems();
            List<Proficiency> proficiencies = ImportData.ExtractProficiencies(items);
            List<Spell> spells = ImportData.ExtractSpells(); 
            List<Background> backgrounds = ImportData.ExtractBackgrounds(proficiencies);
            List<Race> races = ImportData.ExtractRaces(proficiencies);
            List<Class> classes = ImportData.ExtractClasses(proficiencies); 
            List<Subclass> subclasses = ImportData.ExtractSubclasses(classes); 

            List<Models.Action> actions = new List<Models.Action>();
            foreach (Item item in items)
            {
                foreach (AddAction action in item.CharacterModifiers)
                {
                    actions.Add(action.Action);
                }
            }

            _context.Actions.AddRange(actions);
            _context.Proficiencies.AddRange(proficiencies);
            _context.Items.AddRange(items);
            _context.Spells.AddRange(spells);
            _context.Backgrounds.AddRange(backgrounds);
            _context.Races.AddRange(races);
            _context.Classes.AddRange(classes);
            _context.Subclasses.AddRange(subclasses);
            await _context.SaveChangesAsync();

            // Manually fixing the Ids for serialized objects
            items = await _context.Items.ToListAsync();
            backgrounds = await _context.Backgrounds.ToListAsync();
            races = await _context.Races.ToListAsync();
            classes = await _context.Classes.ToListAsync();
            subclasses = await _context.Subclasses.ToListAsync();

            List<CharacterModifier> characterModifiers = new List<CharacterModifier>();
            foreach (Item item in items)
            {
                foreach (CharacterModifier modifier in item.CharacterModifiers)
                {
                    characterModifiers.Add(modifier);
                }
            }
            foreach (Background background in backgrounds)
            {
                foreach (Feature feature in background.Features)
                {
                    foreach (CharacterModifier modifier in feature.CharacterModifiers)
                    {
                        characterModifiers.Add(modifier);
                    }
                }
            }
            foreach (Race race in races)
            {
                foreach (Feature feature in race.Features)
                {
                    foreach (CharacterModifier modifier in feature.CharacterModifiers)
                    {
                        characterModifiers.Add(modifier);
                    }
                }
            }
            foreach (Class @class in classes)
            {
                foreach (Feature feature in @class.Features)
                {
                    foreach (CharacterModifier modifier in feature.CharacterModifiers)
                    {
                        characterModifiers.Add(modifier);
                    }
                }
            }
            foreach (Subclass subclass in subclasses)
            {
                foreach (Feature feature in subclass.Features)
                {
                    foreach (CharacterModifier modifier in feature.CharacterModifiers)
                    {
                        characterModifiers.Add(modifier);
                    }
                }
            }
            foreach (CharacterModifier modifier in characterModifiers)
            {
                await SetModifierIds(modifier);
            }

            _context.Items.UpdateRange(items);
            _context.Backgrounds.UpdateRange(backgrounds);
            _context.Races.UpdateRange(races);
            _context.Classes.UpdateRange(classes);
            _context.Subclasses.UpdateRange(subclasses);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task SetModifierIds(CharacterModifier modifier) 
        {
            if (modifier is GrantProficiencies grantProficiencies)
            {
                for (int i = 0; i < grantProficiencies.Proficiencies.Count; i++)
                {
                    Proficiency? p = await _context.Proficiencies.FirstOrDefaultAsync(p => p.Name == grantProficiencies.Proficiencies[i].Name);
                    if (p != null)
                    {
                        grantProficiencies.Proficiencies[i] = p;
                    }
                }
            }
            else if (modifier is AddAction addAction)
            {
                Models.Action? a = await _context.Actions.FirstOrDefaultAsync(a => a.Name == addAction.Action.Name);
                if (a != null)
                {
                    addAction.Action = a;
                }
            }
            else if (modifier is CharacterModifierChoice choice)
            {
                foreach (CharacterModifier option in choice.Modifiers)
                {
                    await SetModifierIds(option);
                }
            }
        }
    }
}
