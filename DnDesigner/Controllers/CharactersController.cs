using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                AvailableClasses = await _dbHelper.GetAllClasses(),
                AvailableBackgrounds = await _dbHelper.GetAllBackgrounds(),
                AvailableRaces = await _dbHelper.GetAllRaces()
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
                Class @class = await _dbHelper.GetClass(character.ClassId);
                Background background = await _dbHelper.GetBackground(character.BackgroundId);
                Race race = await _dbHelper.GetRace(character.RaceId);

                //A temporary measure to load everything we need, this will be replaced once we have the dbhelper
                foreach (Feature feature in @class.Features)
                {
                    foreach (Effect effect in feature.Effects)
                    {
                        await PopulateEffect(effect);
                    }
                }
                foreach (Feature feature in background.Features)
                {
                    foreach (Effect effect in feature.Effects)
                    {
                        await PopulateEffect(effect);
                    }
                }
                foreach (Feature feature in race.Features)
                {
                    foreach (Effect effect in feature.Effects)
                    {
                        await PopulateEffect(effect);
                    }
                }
                // This is to make sure all characters have all saving throws and skills even if they aren't proficient in them
                // There's probably a better way to do this
                List<Proficiency> defaultProficiencies = await _context.Proficiencies
                    .Where(p => p.Type == "saving throw" || p.Type == "skill")
                    .ToListAsync(); 
                
                    character.MaxHealth = @class.HitDie + (character.Constitution - 10) / 2;
                    Character newCharacter = new Character(character, @class, race, background, defaultProficiencies, User.FindFirstValue(ClaimTypes.NameIdentifier));

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

            Character character = await _dbHelper.GetCharacter((int)id);

            foreach (CharacterEffect characterEffect in character.CharacterEffects)
            {
                await PopulateEffect(characterEffect.Effect);
            }
            
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

        /// <summary>
        /// Loads all the data for an effect
        /// Will be replaced once we have the dbhelper
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        private async Task PopulateEffect(Effect effect)
        {
            if (effect is GrantProficiencies grantProficiencies)
            {
                await _context.Entry(grantProficiencies)
                    .Collection(gp => gp.Proficiencies)
                    .LoadAsync();
            }
            else if (effect is GrantAction grantAction)
            {
                await _context.Entry(grantAction)
                    .Reference(ga => ga.Action)
                    .LoadAsync();
            }
            else if (effect is EffectChoice effectChoice)
            {
                await _context.Entry(effectChoice)
                    .Collection(ec => ec.Effects)
                    .LoadAsync();
                foreach (Effect effectChoiceEffect in effectChoice.Effects)
                {
                    await PopulateEffect(effectChoiceEffect);
                }
            }
        }
    }
}
