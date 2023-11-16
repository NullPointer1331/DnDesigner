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
            List<Item> existingItems = await _context.Items.AsNoTracking().ToListAsync();
            List<Proficiency> existingProficiencies = await _context.Proficiencies.AsNoTracking().ToListAsync();
            List<Spell> existingSpells = await _context.Spells.AsNoTracking().ToListAsync();
            List<Background> existingBackgrounds = await _context.Backgrounds.AsNoTracking().ToListAsync();
            List<Race> existingRaces = await _context.Races.AsNoTracking().ToListAsync();
            List<Class> existingClasses = await _context.Classes.AsNoTracking().ToListAsync();
            List<Subclass> existingSubclasses = await _context.Subclasses.AsNoTracking().ToListAsync();

            List<Item> items = ImportData.ExtractItems();
            List<Proficiency> proficiencies = ImportData.ExtractProficiencies(items);
            List<Spell> spells = ImportData.ExtractSpells(); 
            List<Background> backgrounds = ImportData.ExtractBackgrounds(proficiencies);
            List<Race> races = ImportData.ExtractRaces(proficiencies);
            List<Class> classes = ImportData.ExtractClasses(proficiencies); 
            List<Subclass> subclasses = ImportData.ExtractSubclasses(classes); 

            for (int j = 0; j < items.Count; j++)
            {
                Item? existingItem = existingItems.Find(i => i.Name == items[j].Name && i.Sourcebook == items[j].Sourcebook);
                if (existingItem != null)
                {
                    items[j].ItemId = existingItem.ItemId;
                    existingItem = items[j];
                    items.RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < proficiencies.Count; j++)
            {
                Proficiency? existingProficiency = existingProficiencies.Find(p => p.Name == proficiencies[j].Name && p.Type == proficiencies[j].Type);
                if (existingProficiency != null)
                {
                    proficiencies[j].ProficiencyId = existingProficiency.ProficiencyId;
                    existingProficiency = proficiencies[j];
                    proficiencies.RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < spells.Count; j++)
            {
                Spell? existingSpell = existingSpells.Find(s => s.Name == spells[j].Name && s.Sourcebook == spells[j].Sourcebook);
                if (existingSpell != null)
                {
                    spells[j].SpellId = existingSpell.SpellId;
                    existingSpell = spells[j];
                    spells.RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < backgrounds.Count; j++)
            {
                Background? existingBackground = existingBackgrounds.Find(b => b.Name == backgrounds[j].Name && b.Sourcebook == backgrounds[j].Sourcebook);
                if (existingBackground != null)
                {
                    backgrounds[j].BackgroundId = existingBackground.BackgroundId;
                    existingBackground = backgrounds[j];
                    backgrounds.RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < races.Count; j++)
            {
                Race? existingRace = existingRaces.Find(r => r.Name == races[j].Name && r.Sourcebook == races[j].Sourcebook);
                if (existingRace != null)
                {
                    races[j].RaceId = existingRace.RaceId;
                    existingRace = races[j];
                    races.RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < classes.Count; j++)
            {
                Class? existingClass = existingClasses.Find(c => c.Name == classes[j].Name && c.Sourcebook == classes[j].Sourcebook);
                if (existingClass != null)
                {
                    classes[j].ClassId = existingClass.ClassId;
                    existingClass = classes[j];
                    classes.RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < subclasses.Count; j++)
            {
                Subclass? existingSubclass = existingSubclasses.Find(s => s.Name == subclasses[j].Name && s.Sourcebook == subclasses[j].Sourcebook);
                if (existingSubclass != null)
                {
                    subclasses[j].SubclassId = existingSubclass.SubclassId;
                    existingSubclass = subclasses[j];
                    subclasses.RemoveAt(j);
                    j--;
                }
            }

            _context.UpdateRange(existingItems);
            _context.UpdateRange(existingProficiencies);
            _context.UpdateRange(existingSpells);
            _context.UpdateRange(existingBackgrounds);
            _context.UpdateRange(existingRaces);
            _context.UpdateRange(existingClasses);
            _context.UpdateRange(existingSubclasses);

            _context.Proficiencies.AddRange(proficiencies);
            _context.Items.AddRange(items);
            _context.Spells.AddRange(spells);
            _context.Backgrounds.AddRange(backgrounds);
            _context.Races.AddRange(races);
            _context.Classes.AddRange(classes);
            _context.Subclasses.AddRange(subclasses);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
