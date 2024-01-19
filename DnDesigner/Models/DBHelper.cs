using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Gets the <see cref="Background"/> from the database with the given id
        /// </summary>
        /// <param name="id">An id of a <see cref="Background"/> in the database</param>
        /// <returns>The <see cref="Background"/> with the specified id</returns>
        public async Task<Background> GetBackground(int id)
        {
            Background background = await _context.Backgrounds.Where(b => b.BackgroundId == id)
                    .Include(b => b.Features)
                    .ThenInclude(be => be.Effects)
                    .FirstOrDefaultAsync();
            foreach (BackgroundFeature feature in background.Features)
            {
                await LoadEffects(feature.Effects);
                await LoadChoices(feature.Choices);
            }
            return background;
        }

        /// <summary>
        /// Gets the <see cref="Background"/> from the database with the given id, 
        /// loads everything directly in the background object, but not objects nested further
        /// </summary>
        /// <param name="id">An id of a <see cref="Background"/> in the database</param>
        /// <returns>The <see cref="Background"/> with the specified id</returns>
        public async Task<Background> GetMinBackground(int id)
        {
            return await _context.Backgrounds.Where(b => b.BackgroundId == id)
                    .Include(b => b.Features)
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
            return  await _context.Backgrounds
                    .Include(b => b.Features)
                    .ToListAsync();
        }

        /// <summary>
        /// Gets the <see cref="Character"/> from the database with the given id
        /// </summary>
        /// <param name="id">An id of a <see cref="Character"/> in the database</param>
        /// <returns>The <see cref="Character"/> with the given id</returns>
        public async Task<Character> GetCharacter(int id)
        {
            Character character = await _context.Characters.Where(c => c.CharacterId == id)
                    .Include(c => c.Race)
                    .Include(c => c.Background)
                    .Include(c => c.Classes)
                    .ThenInclude(cc => cc.Class)
                    .Include(c => c.Classes)
                    .ThenInclude(cc => cc.Subclass)
                    .Include(c => c.Proficiencies)
                    .ThenInclude(cp => cp.Proficiency)
                    .Include(c => c.Features)
                    .ThenInclude(cf => cf.Feature)
                    .ThenInclude(f => f.Effects)
                    .Include(c => c.Features)
                    .ThenInclude(cf => cf.Choices)
                    .ThenInclude(cfc => cfc.Choice)
                    .Include(c => c.Inventory)
                    .ThenInclude(ci => ci.Items)
                    .ThenInclude(cii => cii.Item)
                    .Include(c => c.CharacterEffects)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync();
            foreach (CharacterEffect characterEffect in character.CharacterEffects)
            {
                await LoadEffect(characterEffect.Effect);
            }
            foreach (CharacterFeature feature in character.Features)
            {
                await LoadEffects(feature.Feature.Effects);
                foreach (Choice choice in feature.Feature.Choices)
                {
                    await LoadChoice(choice);
                }
            }
            return character;
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
            return await _context.Characters.Where(r => r.UserId.Equals(userId))
                .Include(r => r.Race)
                .Include(r => r.Background)
                .Include(r => r.Classes)
                .ThenInclude(cc => cc.Class)
                .Include(r => r.Classes)
                .ThenInclude(cc => cc.Subclass)
                .AsSplitQuery()
                .ToListAsync();
        }

        /// <summary>
        /// Gets the <see cref="Class"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Character"/> in the database</param>
        /// <returns>The <see cref="Character"/> with the given id</returns>
        public async Task<Class> GetClass(int id)
        {
            Class @class = await _context.Classes.Where(c => c.ClassId == id)
                    .Include(c => c.Spellcasting)
                    .Include(c => c.Features)
                    .ThenInclude(cf => cf.Effects)
                    .Include(c => c.Features)
                    .ThenInclude(cf => cf.Choices)
                    .FirstOrDefaultAsync();
            foreach (Feature feature in @class.Features)
            {
                await LoadEffects(feature.Effects);
                await LoadChoices(feature.Choices);
            }
            return @class;
        }

        /// <summary>
        /// Gets all <see cref="Class"/>es from the database
        /// </summary>
        /// <returns>A <see cref="List{Class}"/> of all the classes in the database</returns>
        public async Task<List<Class>> GetAllClasses()
        {
            return await _context.Classes
                .Include(c => c.Spellcasting)
                .Include(c => c.Subclasses)
                .Include(c => c.Features)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the <see cref="Inventory"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of an <see cref="Inventory"/> in the database</param>
        /// <returns>The <see cref="Inventory"/> in from the database with the given id</returns>
        public async Task<Inventory> GetInventory(int id)
        {
            Inventory inventory = await _context.Inventory.Where(i => i.InventoryId == id)
                    .Include(i => i.Items)
                    .ThenInclude(ii => ii.Item)
                    .FirstOrDefaultAsync();
            inventory.PopulateEquipmentSlots();
            return inventory;
        }

        /// <summary>
        /// Gets the <see cref="Item"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of an <see cref="Item"/> in the database</param>
        /// <returns>The <see cref="Item"/> from the database with the given id</returns>
        public async Task<Item> GetItem(int id)
        {
            Item item = await _context.Items.Where(i => i.ItemId == id)
                    .Include(i => i.Effects)
                    .FirstOrDefaultAsync();
            await LoadEffects(item.Effects);
            return item;
        }

        /// <summary>
        /// Gets all <see cref="Item"/>s from the database
        /// </summary>
        /// <returns>A <see cref="List{Item}"/> that contains all of the items in the database</returns>
        public async Task<List<Item>> GetAllItems()
        {
            List<Item> items = await _context.Items
                    .Include(i => i.Effects)
                    .ToListAsync();
            foreach (Item item in items)
            {
                await LoadEffects(item.Effects);
            }
            return items;
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
            return await _context.Proficiencies
                    .ToListAsync();
        }

        /// <summary>
        /// Gets a <see cref="Race"/> from the database with the given id
        /// </summary>
        /// <param name="id">The id of a <see cref="Race"/> in the database</param>
        /// <returns>The <see cref="Race"/> from the database with the given id</returns>
        public async Task<Race> GetRace(int id)
        {
            Race race = await _context.Races.Where(r => r.RaceId == id)
                    .Include(r => r.Features)
                    .ThenInclude(re => re.Effects)
                    .Include(r => r.Features)
                    .ThenInclude(rf => rf.Choices)
                    .FirstOrDefaultAsync();
            foreach (Feature feature in race.Features)
            {
                await LoadEffects(feature.Effects);
                await LoadChoices(feature.Choices);
            }
            return race;
        }

        /// <summary>
        /// Gets all the <see cref="Race"/>s from the database
        /// </summary>
        /// <returns>A <see cref="List{Race}"/> with all the races from the database</returns>
        public async Task<List<Race>> GetAllRaces()
        {
            return await _context.Races
                    .Include (r => r.Features)
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
            return await _context.Spells
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
            return await _context.Spellcasting
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
            Subclass subclass = await _context.Subclasses.Where(s => s.SubclassId == id)
                    .Include(s => s.Features)
                    .ThenInclude(se => se.Effects)
                    .Include(s => s.Features)
                    .ThenInclude(sf => sf.Choices)
                    .FirstOrDefaultAsync();
            foreach (Feature feature in subclass.Features)
            {
                await LoadEffects(feature.Effects);
                await LoadChoices(feature.Choices);
            }
            return subclass;
        }

        /// <summary>
        /// Gets all <see cref="Subclass"/>es from the database
        /// </summary>
        /// <returns>A <see cref="List{Subclass}"/> with all the subclasses from the database</returns>
        public async Task<List<Subclass>> GetAllSubclasses()
        {
            return await _context.Subclasses
                    .Include(s => s.Class)
                    .Include(s => s.Features)
                    .ToListAsync();
        }

        /// <summary>
        /// Loads all the data for a list of <see cref="Effect"/>s
        /// </summary>
        /// <param name="effects">The effects to load</param>
        private async Task LoadEffects(List<Effect> effects)
        {
            foreach (Effect effect in effects)
            {
                await LoadEffect(effect);
            }
        }

        /// <summary>
        /// Loads all the data for an <see cref="Effect"/>
        /// </summary>
        /// <param name="effect">The effect to load</param>
        private async Task LoadEffect(Effect effect)
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
        }

        private async Task LoadChoices(List<Choice> choices)
        {
            foreach (Choice choice in choices)
            {
                await LoadChoice(choice);
            }
        }

        private async Task LoadChoice(Choice choice)
        {
            if (choice is EffectChoice effectChoice)
            {
                await _context.Entry(effectChoice)
                    .Collection(ec => ec.Options)
                    .LoadAsync();
                await LoadEffects(effectChoice.Options);
            }
        }
    }
}

