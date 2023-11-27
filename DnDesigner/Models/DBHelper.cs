using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DnDesigner.Data;
using System.Linq;

namespace DnDesigner.Models
{
    /// <summary>
    /// This class implements the IDBHelper interface and provides a way to access the database.
    /// </summary>
    public class DBHelper : IDBHelper
    {
        private readonly DnDesignerDbContext _context;

        public DBHelper(DnDesignerDbContext context)
        {
            _context = context;
        }


        /*
            TODO: Implement the complex functionality to get the full objects from the database.
            
            Race race = 
                await _context.Races.Where(r => r.RaceId == character.RaceId)
                              .Include(r => r.Proficiencies)
                              .ThenInclude(rp => rp.Proficiency)
                              .Include(r => r.Features)
        
            The above code is an example of what we need to do.
         */
        public async Task<Background> GetBackground(int id)
        {
            return await _context.Backgrounds.Where(b => b.BackgroundId == id)
                    .Include(bf => bf.Features)
                    .ThenInclude(be => be.Effects)
                    .Include(bi => bi.StarterEquipment)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Background>> GetAllBackgrounds()
        {
            return await _context.Set<Background>().ToListAsync();
        }

        public async Task<Character> GetCharacter(int id)
        {
            return await _context.Characters.Where(c => c.CharacterId == id)
                    .Include(cp => cp.Proficiencies)
                    .ThenInclude(cp => cp.Proficiency)
                    .Include(cc => cc.Classes)
                    .ThenInclude(cc => cc.Class)
                    .Include(cs => cs.Spellcasting)
                    .Include(cf => cf.Features)
                    .ThenInclude(ce => ce.Effects)
                    .Include(ca => ca.Actions)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Character>> GetAllCharacters(string userId)
        {
            return await _context.Set<Character>().Where(r => r.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<Class> GetClass(int id)
        {
            return await _context.Classes.Where(c => c.ClassId == id)
                    .Include(cf => cf.Features)
                    .ThenInclude(ce => ce.Effects)
                    .Include(cs => cs.Subclasses)
                    .ThenInclude(cf => cf.Features)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Class>> GetAllClasses()
        {
            return await _context.Set<Class>().ToListAsync();
        }

        public async Task<Inventory> GetInventory(int id)
        {
            return await _context.Inventory.Where(i => i.InventoryId == id)
                    .Include(ii => ii.Items)
                    .Include(io => io.OtherEquippedItems)
                    .Include(ia => ia.AttunedItems)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Inventory>> GetAllInventories()
        {
            return await _context.Set<Inventory>().ToListAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            return await _context.Items.Where(i => i.ItemId == id)
                    .Include(ie => ie.Effects)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _context.Set<Item>().ToListAsync();
        }

        public async Task<Proficiency> GetProficiency(int id)
        {
            return await _context.Proficiencies.Where(p => p.ProficiencyId == id)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Proficiency>> GetAllProficiencies()
        {
            return await _context.Set<Proficiency>().ToListAsync();
        }

        public async Task<Race> GetRace(int id)
        {
            return await _context.Races.Where(r => r.RaceId == id)
                    .Include(r => r.Features)
                    .ThenInclude(re => re.Effects)
                    .FirstOrDefaultAsync();
        }


        public async Task<List<Race>> GetAllRaces()
        {
            return await _context.Set<Race>().ToListAsync();
        }

        public async Task<Spell> GetSpell(int id)
        {
            return await _context.Spells.Where(s => s.SpellId == id)
                    .Include(ss => ss.LearnedBy)
                    .ThenInclude(ss => ss.LearnableSpells)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Spell>> GetAllSpells()
        {
            return await _context.Set<Spell>().ToListAsync();
        }

        public async Task<Spellcasting> GetSpellcasting(int id)
        {
            return await _context.Spellcasting.Where(s => s.SpellcastingId == id)
                    .Include(ss => ss.LearnableSpells)
                    .ThenInclude(ss => ss.LearnedBy)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Spellcasting>> GetAllSpellcastings()
        {
            return await _context.Set<Spellcasting>().ToListAsync();
        }

        public async Task<Subclass> GetSubclass(int id)
        {
            return await _context.Subclasses.Where(s => s.SubclassId == id)
                    .Include(id => id.Features)
                    .ThenInclude(ie => ie.Effects)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Subclass>> GetAllSubclasses()
        {
            return await _context.Set<Subclass>().ToListAsync();
        }
    }
}

