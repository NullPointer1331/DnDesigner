using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
            CreateCharacterViewModel characterViewModel = new()
            {
                AvailableClasses = _context.Classes.ToList(),
                AvailableBackgrounds = _context.Backgrounds.ToList(),
                AvailableRaces = _context.Races.ToList(),
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
                    .Include(c => c.Proficiencies)
                    .ThenInclude(cp => cp.Proficiency)
                    .Include(c => c.Spellcasting)
                    .Include(c => c.Features)
                    .FirstOrDefaultAsync();
                Background background = await _context.Backgrounds
                    .Where(b => b.BackgroundId == character.BackgroundId)
                    .Include(b => b.Proficiencies)
                    .ThenInclude(bp => bp.Proficiency)
                    .Include(b => b.Features)
                    .FirstOrDefaultAsync();
                Race race = await _context.Races
                    .Where(r => r.RaceId == character.RaceId)
                    .Include(r => r.Proficiencies)
                    .ThenInclude(rp => rp.Proficiency)
                    .Include(r => r.Features)
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
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Characters == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,Name,Level,ProficiencyBonus,MaxHealth,CurrentHealth,TempHealth,AvailableHitDice,HitDieType,WalkingSpeed,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma,Resistances,Immunities,Vulnerabilities")] Character character)
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
