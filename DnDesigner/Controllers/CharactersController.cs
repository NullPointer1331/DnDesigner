﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DnDesigner.Data;
using DnDesigner.Models;

namespace DnDesigner.Controllers
{
    public class CharactersController : Controller
    {
        private readonly DnDesignerDbContext _context;

        public CharactersController(DnDesignerDbContext context)
        {
            _context = context;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
              return _context.Characters != null ? 
                          View(await _context.Characters.ToListAsync()) :
                          Problem("Entity set 'DnDesignerDbContext.Characters'  is null.");
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Characters == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .FirstOrDefaultAsync(m => m.CharacterId == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public async Task<IActionResult> Create()
        {
            CreateCharacterViewModel characterViewModel = new()
            {
                AvailableClasses = await _context.Classes.ToListAsync(),
                AvailableBackgrounds = await _context.Backgrounds.ToListAsync(),
                AvailableRaces = await _context.Races.ToListAsync()
            };
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
                Class @class = await _context.Classes
                    .Where(c => c.ClassId == character.ClassId)
                    .Include(c => c.Spellcasting)
                    .Include(c => c.Features)
                    .ThenInclude(cf => cf.Effects)
                    .FirstOrDefaultAsync();
                Background background = await _context.Backgrounds
                    .Where(b => b.BackgroundId == character.BackgroundId)
                    .Include(b => b.Features)
                    .ThenInclude(bf => bf.Effects)
                    .FirstOrDefaultAsync();
                Race race = await _context.Races
                    .Where(r => r.RaceId == character.RaceId)
                    .Include(r => r.Features)
                    .ThenInclude(rf => rf.Effects)
                    .FirstOrDefaultAsync();

                // This is to make sure all characters have all saving throws and skills even if they aren't proficient in them
                // There's probably a better way to do this
                List<Proficiency> defaultProficiencies = await _context.Proficiencies
                    .Where(p => p.Type == "saving throw" || p.Type == "skill")
                    .ToListAsync(); 
                
                character.MaxHealth = @class.HitDie + (character.Constitution - 10) / 2;
                Character newCharacter = new Character(character, @class, race, background, defaultProficiencies);

                _context.Add(newCharacter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            character.AvailableClasses = await _context.Classes.ToListAsync();
            character.AvailableBackgrounds = await _context.Backgrounds.ToListAsync();
            character.AvailableRaces = await _context.Races.ToListAsync();
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> CharacterSheet(int? id)
        {
            if (id == null || _context.Characters == null)
            {
                return NotFound();
            }

            Character character = await _context.Characters
                .Where(c => c.CharacterId == id)
                .Include(c => c.Race)
                .Include(c => c.Background)
                .Include(c => c.Classes)
                .ThenInclude(cc => cc.Class)
                .Include(c => c.Classes)
                .ThenInclude(cc => cc.Subclass)
                .Include(c => c.Proficiencies)
                .ThenInclude(cp => cp.Proficiency)
                .Include(c => c.Features)
                .Include(c => c.Inventory)
                .FirstOrDefaultAsync();

            
            if (character == null)
            {
                return NotFound();
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
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Characters == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .FirstOrDefaultAsync(m => m.CharacterId == id);
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
            if (_context.Characters == null)
            {
                return Problem("Entity set 'DnDesignerDbContext.Characters'  is null.");
            }
            var character = await _context.Characters.FindAsync(id);
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
