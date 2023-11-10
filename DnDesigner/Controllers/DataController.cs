﻿using DnDesigner.Data;
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

            _context.Proficiencies.AddRange(proficiencies);
            _context.Items.AddRange(items);
            _context.Spells.AddRange(spells);
            _context.Backgrounds.AddRange(backgrounds);
            _context.Races.AddRange(races);
            _context.Classes.AddRange(classes);
            _context.Subclasses.AddRange(subclasses);
            _context.Actions.AddRange(actions);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
