using DnDesigner.Data;
using DnDesigner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDesigner.Controllers
{

    public class DataController : Controller
    {
        private readonly DnDesignerDbContext _context;
        private readonly IDBHelper _dbHelper;

        public DataController(DnDesignerDbContext context, IDBHelper dbHelper)
        {
            _context = context;
            _dbHelper = dbHelper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewProficiencies()
        {
            return View(await _dbHelper.GetAllProficiencies());
        }
        public async Task<IActionResult> ViewItems()
        {
            return View(await _dbHelper.GetAllItems());
        }
        public async Task<IActionResult> ViewSpells()
        {
            return View(await _dbHelper.GetAllSpells());
        }
        public async Task<IActionResult> ViewBackgrounds()
        {
            return View(await _dbHelper.GetAllBackgrounds());
        }
        public async Task<IActionResult> ViewRaces()
        {
            return View(await _dbHelper.GetAllRaces());
        }
        public async Task<IActionResult> ViewClasses()
        {
            return View(await _dbHelper.GetAllClasses());
        }
        public async Task<IActionResult> DisplayClass(int id)
        {
            Class? @class = await _dbHelper.GetClass(id);
            if(@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }
        public async Task<IActionResult> ViewSubclasses()
        {
            return View(await _dbHelper.GetAllSubclasses());
        }
        public async Task<IActionResult> ViewFeatures()
        {
            List<Feature> features = new List<Feature>();
            features.AddRange(await _context.Features.ToListAsync());
            return View(features);
        }
        public async Task<IActionResult> Import()
        {
            _context.Effects.RemoveRange(_context.Effects);
            _context.Choices.RemoveRange(_context.Choices);
            _context.CharacterFeatures.RemoveRange(_context.CharacterFeatures);
            await _context.SaveChangesAsync();

            _context.Proficiencies.RemoveRange(_context.Proficiencies);
            _context.Items.RemoveRange(_context.Items);
            _context.Spells.RemoveRange(_context.Spells);
            _context.Backgrounds.RemoveRange(_context.Backgrounds);
            _context.Races.RemoveRange(_context.Races);
            _context.Classes.RemoveRange(_context.Classes);
            _context.Subclasses.RemoveRange(_context.Subclasses);
            _context.Actions.RemoveRange(_context.Actions);
            _context.Features.RemoveRange(_context.Features);

            List<Item> items = ImportData.ExtractItems();
            List<Proficiency> proficiencies = ImportData.ExtractProficiencies(items);
            List<Spell> spells = ImportData.ExtractSpells(); 
            List<Background> backgrounds = ImportData.ExtractBackgrounds(proficiencies);
            List<Race> races = ImportData.ExtractRaces(proficiencies);
            List<Class> classes = ImportData.ExtractClasses(proficiencies); 
            List<Subclass> subclasses = ImportData.ExtractSubclasses(classes); 
            List<SelectableFeature> feats = ImportData.ExtractFeats();

            _context.SelectableFeatures.AddRange(feats);
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
