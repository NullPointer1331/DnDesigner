using DnDesigner.Data;
using DnDesigner.Models;
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
        public async Task<IActionResult> ViewClasses()
        {
            return View(await _context.Classes.ToListAsync());
        }
        public async Task<IActionResult> Import()
        {
            await _context.SaveChangesAsync();
            List<Proficiency> proficiencies = ImportData.ExtractProficiencies();
            List<Item> items = ImportData.ExtractItems();
            List<Spell> spells = ImportData.ExtractSpells(); //Doesn't seem to work yet
            List<Background> backgrounds = ImportData.ExtractBackgrounds();
            List<Race> races = ImportData.ExtractRaces();
            List<Class> classes = ImportData.ExtractClasses(); //Doesn't seem to work yet
            List<Subclass> subclasses = ImportData.ExtractSubclasses(classes); //Doesn't seem to work yet

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
