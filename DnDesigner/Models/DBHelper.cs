using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DnDesigner.Data;

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

        public async Task<Background> GetBackground(int id)
        {
            return await _context.FindAsync<Background>(id);
        }

        public async Task<List<Background>> GetAllBackgrounds()
        {
            return await _context.Set<Background>().ToListAsync();
        }

        public async Task<Character> GetCharacter(int id)
        {
            return await _context.FindAsync<Character>(id);
        }

        public async Task<List<Character>> GetAllCharacters(string userId)
        {
            return await _context.Set<Character>().Where(r => r.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<Class> GetClass(int id)
        {
            return await _context.FindAsync<Class>(id);
        }

        public async Task<List<Class>> GetAllClasses()
        {
            return await _context.Set<Class>().ToListAsync();
        }

        public async Task<Feature> GetFeature(int id)
        {
            return await _context.FindAsync<Feature>(id);
        }

        public async Task<List<Feature>> GetAllFeatures()
        {
            return await _context.Set<Feature>().ToListAsync();
        }

        public async Task<Inventory> GetInventory(int id)
        {
            return await _context.FindAsync<Inventory>(id);
        }

        public async Task<List<Inventory>> GetAllInventories()
        {
            return await _context.Set<Inventory>().ToListAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            return await _context.FindAsync<Item>(id);
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _context.Set<Item>().ToListAsync();
        }

        public async Task<Proficiency> GetProficiency(int id)
        {
            return await _context.FindAsync<Proficiency>(id);
        }

        public async Task<List<Proficiency>> GetAllProficiencies()
        {
            return await _context.Set<Proficiency>().ToListAsync();
        }

        public async Task<Race> GetRace(int id)
        {
            return await _context.FindAsync<Race>(id);
        }

        public async Task<List<Race>> GetAllRaces()
        {
            return await _context.Set<Race>().ToListAsync();
        }

        public async Task<Spell> GetSpell(int id)
        {
            return await _context.FindAsync<Spell>(id);
        }

        public async Task<List<Spell>> GetAllSpells()
        {
            return await _context.Set<Spell>().ToListAsync();
        }

        public async Task<Spellcasting> GetSpellcasting(int id)
        {
            return await _context.FindAsync<Spellcasting>(id);
        }

        public async Task<List<Spellcasting>> GetAllSpellcastings()
        {
            return await _context.Set<Spellcasting>().ToListAsync();
        }

        public async Task<Subclass> GetSubclass(int id)
        {
            return await _context.FindAsync<Subclass>(id);
        }

        public async Task<List<Subclass>> GetAllSubclasses()
        {
            return await _context.Set<Subclass>().ToListAsync();
        }

        public async Task<string> GetUserName(string id)
        {
            return (await _context.FindAsync<IdentityUser>(id)).UserName;
        }
    }
}

