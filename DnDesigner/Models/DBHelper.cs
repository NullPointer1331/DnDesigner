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

        /// <summary>
        /// Gets the <see cref="Background"/> from the database with the given id
        /// </summary>
        /// <param name="id">An id of a <see cref="Background"/> in the database</param>
        /// <returns>The <see cref="Background"/> with the specified id</returns>
        public async Task<Background> GetBackground(int id)
        {
            return await _context.Backgrounds.Where(b => b.BackgroundId == id)
                    .Include(b => b.Features)
                    .ThenInclude(be => be.Effects)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Background"/>s from the database
        /// </summary>
        /// <returns>
        /// A <see cref="List{Background}"/> that contains all of the backgrounds in the database
        /// </returns>
        public async Task<List<Background>> GetAllBackgrounds()
        {
            return await _context.Set<Background>()
                    .Include(b => b.Features)
                    .ThenInclude(be => be.Effects)
                    .Include(b => b.StarterEquipment)
                    .ToListAsync();
        }

        /// <summary>
        /// Gets the <see cref="Character"/> from the database with the given id
        /// </summary>
        /// <param name="id">An id of a <see cref="Character"/> in the database</param>
        /// <returns>The <see cref="Character"/> with the given id</returns>
        public async Task<Character> GetCharacter(int id)
        {
            return await _context.Characters.Where(c => c.CharacterId == id)
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
                    .Include(c => c.CharacterEffects)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Character"/>s from the database with the given userId
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>
        /// A <see cref="List{Character}"/> that contains all of the characters made by that user
        /// </returns>
        public async Task<List<Character>> GetAllCharacters(string userId)
        {
            return await _context.Set<Character>().Where(r => r.UserId.Equals(userId))
                    .ToListAsync();
        }

        /// <summary>
        /// Gets the <see cref="Class"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Character"/> in the database</param>
        /// <returns>The <see cref="Character"/> with the given id</returns>
        public async Task<Class> GetClass(int id)
        {
            return await _context.Classes.Where(c => c.ClassId == id)
                    .Include(c => c.Spellcasting)
                    .Include(c => c.Features)
                    .ThenInclude(cf => cf.Effects)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Class"/>es from the database
        /// </summary>
        /// <returns>A <see cref="List{Class}"/> of all the classes in the database</returns>
        public async Task<List<Class>> GetAllClasses()
        {
            return await _context.Set<Class>().ToListAsync();
        }

        /// <summary>
        /// Gets the <see cref="Inventory"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of an <see cref="Inventory"/> in the database</param>
        /// <returns>The <see cref="Inventory"/> in from the database with the given id</returns>
        public async Task<Inventory> GetInventory(int id)
        {
            return await _context.Inventory.Where(i => i.InventoryId == id)
                    .Include(i => i.Items)
                    .Include(i => i.OtherEquippedItems)
                    .Include(i => i.AttunedItems)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the <see cref="Item"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of an <see cref="Item"/> in the database</param>
        /// <returns>The <see cref="Item"/> from the database with the given id</returns>
        public async Task<Item> GetItem(int id)
        {
            return await _context.Items.Where(i => i.ItemId == id)
                    .Include(i => i.Effects)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Item"/>s from the database
        /// </summary>
        /// <returns>A <see cref="List{Item}"/> that contains all of the items in the database</returns>
        public async Task<List<Item>> GetAllItems()
        {
            return await _context.Set<Item>()
                    .Include(i => i.Effects)
                    .ToListAsync();
        }

        /// <summary>
        /// Gets the <see cref="Proficiency"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Proficiency"/> in the database</param>
        /// <returns>A <see cref="Proficiency"/> from the database with the given id.</returns>
        public async Task<Proficiency> GetProficiency(int id)
        {
            return await _context.Proficiencies.Where(p => p.ProficiencyId == id)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Proficiency"/>s from the database
        /// </summary>
        /// <returns>A <see cref="List{Proficiency}"/> with all the proficiencies from the database</returns>
        public async Task<List<Proficiency>> GetAllProficiencies()
        {
            return await _context.Set<Proficiency>()
                    .ToListAsync();
        }

        /// <summary>
        /// Gets a <see cref="Race"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Race"/> in the database</param>
        /// <returns>The <see cref="Race"/> from the database with the given id</returns>
        public async Task<Race> GetRace(int id)
        {
            return await _context.Races.Where(r => r.RaceId == id)
                    .Include(r => r.Features)
                    .ThenInclude(re => re.Effects)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all the <see cref="Race"/>s from the database
        /// </summary>
        /// <returns>A <see cref="List{Race}"/> with all the races from the database</returns>
        public async Task<List<Race>> GetAllRaces()
        {
            return await _context.Set<Race>()
                    .Include (r => r.Features)
                    .ThenInclude(re => re.Effects)
                    .ToListAsync();
        }

        /// <summary>
        /// Gets a <see cref="Spell"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Spell"/> from the database</param>
        /// <returns>The <see cref="Spell"/> from the database with the given id</returns>
        public async Task<Spell> GetSpell(int id)
        {
            return await _context.Spells.Where(s => s.SpellId == id)
                    .Include(s => s.LearnedBy)
                    .ThenInclude(ss => ss.LearnableSpells)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Spell"/>s from the database
        /// </summary>
        /// <returns>A <see cref="List{Spell}"/> with all the spells from the database</returns>
        public async Task<List<Spell>> GetAllSpells()
        {
            return await _context.Set<Spell>()
                    .Include(ss => ss.LearnedBy)
                    .ThenInclude(ss => ss.LearnableSpells)
                    .ToListAsync();
        }

        /// <summary>
        /// Gets a <see cref="Spellcasting"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Spellcasting"/> in the database</param>
        /// <returns>The <see cref="Spellcasting"/> from the database with the given id</returns>
        public async Task<Spellcasting> GetSpellcasting(int id)
        {
            return await _context.Spellcasting.Where(s => s.SpellcastingId == id)
                    .Include(ss => ss.LearnableSpells)
                    .ThenInclude(ss => ss.LearnedBy)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Spellcasting"/>s from the database
        /// </summary>
        /// <returns>
        /// A <see cref="List{Spellcasting}"/> with all the spellcasting objects from the database
        /// </returns>
        public async Task<List<Spellcasting>> GetAllSpellcastings()
        {
            return await _context.Set<Spellcasting>()
                    .Include(ss => ss.LearnableSpells)
                    .ThenInclude(ss => ss.LearnedBy)
                    .ToListAsync();
        }

        /// <summary>
        /// Gets a <see cref="Subclass"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Subclass"/> in the database</param>
        /// <returns>The <see cref="Subclass"/> from the database with the given id</returns>
        public async Task<Subclass> GetSubclass(int id)
        {
            return await _context.Subclasses.Where(s => s.SubclassId == id)
                    .Include(sf => sf.Features)
                    .ThenInclude(se => se.Effects)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all <see cref="Subclass"/>es from the database
        /// </summary>
        /// <returns>A <see cref="List{Subclass}"/> with all the subclasses from the database</returns>
        public async Task<List<Subclass>> GetAllSubclasses()
        {
            return await _context.Set<Subclass>()
                    .Include(sf => sf.Features)
                    .ThenInclude(se => se.Effects)
                    .ToListAsync();
        }
    }
}

