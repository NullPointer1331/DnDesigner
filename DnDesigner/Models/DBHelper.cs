using Microsoft.EntityFrameworkCore;

namespace DnDesigner.Models
{
    public static class DBHelper
    {
        /* TODO:
            Get methods -
                * Backgrounds
                * Character
                * Class
                * Feature
                * Inventory
                * Item
                * Proficiency
                * Race
                * Spell
                * Spellcasting
                * Subclass
         */

        /// <summary>
        /// Gets a <see cref="Background"/> from the database,
        /// using the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Background"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Background"/> with the specified id.</returns>
        public static async Task<Background> GetBackground(int id, DbContext context)
        {
            return await context.FindAsync<Background>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Background"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Background}"/> of all the <see cref="Background"/> objects in the database
        /// </returns>
        public static async Task<List<Background>> GetAllBackgrounds(DbContext context) 
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Character"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Character"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Character"/> with the specified id.</returns>
        public static async Task<Character> GetCharacterAsync(int id, DbContext context)
        {
            return await context.FindAsync<Character>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Character"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Character}"/> of all the <see cref="Character"/> objects in the database.
        /// </returns>
        public static List<Character> GetAllCharacters(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Class"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Character"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Character"/> with the specified id.</returns>
        public static async Task<Class> GetClass(int id, DbContext context)
        {
            return await context.FindAsync<Class>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Class"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>A <see cref="List{Class}"/> of <see cref="Class"/> objects from the database.</returns>
        public static List<Class> GetAllClasses(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Feature"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Feature"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Feature"/> with the specified id.</returns>
        public static async Task<Feature> GetFeature(int id, DbContext context)
        {
            return await context.FindAsync<Feature>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Feature"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Feature}"/> of all the <see cref="Feature"/> objects in the database.
        /// </returns>
        public static List<Feature> GetAllFeatures(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Inventory"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Inventory"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Inventory"/> with the specified id.</returns>
        public static async Task<Inventory> GetInventory(int id, DbContext context)
        {
            return await context.FindAsync<Inventory>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Inventory"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Inventory}"/> of all the <see cref="Inventory"/> objects in the database.
        /// </returns>
        public static List<Inventory> GetAllInventories(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Item"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Item"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Item"/> with the specified id.</returns>
        public static async Task<Item> GetItem(int id, DbContext context)
        {
            return await context.FindAsync<Item>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Inventory"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Inventory}"/> of all the <see cref="Inventory"/> objects in the database.
        /// </returns>
        public static List<Item> GetAllItems(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Proficiency"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Proficiency"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Item"/> with the specified id.</returns>
        public static async Task<Proficiency> GetProficiency(int id, DbContext context)
        {
            return await context.FindAsync<Proficiency>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Proficiency"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Proficiency}"/> of all the <see cref="Proficiency"/> objects in the database.
        /// </returns>
        public static List<Proficiency> GetAllProficiencies(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Race"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Race"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Race"/> with the specified id.</returns>
        public static async Task<Race> GetRace(int id, DbContext context)
        {
            return await context.FindAsync<Race>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Race"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Race}"/> of all the <see cref="Race"/> objects in the database.
        /// </returns>
        public static List<Race> GetAllRaces(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Spell"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Spell"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Spell"/> with the specified id.</returns>
        public static async Task<Spell> GetSpell(int id, DbContext context)
        {
            return await context.FindAsync<Spell>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Spell"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Spell}"/> of all the <see cref="Spell"/> objects in the database.
        /// </returns>
        public static List<Spell> GetAllSpells(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Spellcasting"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Spellcasting"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Spellcasting"/> with the specified id.</returns>
        public static async Task<Spellcasting> GetSpellcasting(int id, DbContext context)
        {
            return await context.FindAsync<Spellcasting>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Spellcasting"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Spellcasting}"/> of all the <see cref="Spellcasting"/> objects in the database.
        /// </returns>
        public static List<Spellcasting> GetAllSpellcastings(DbContext context)
        {
            return null;
        }

        /// <summary>
        /// Gets a <see cref="Subclass"/> from the database,
        /// with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Subclass"/> that you're looking for.</param>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>The <see cref="Subclass"/> with the specified id.</returns>
        public static async Task<Subclass> GetSubclass(int id, DbContext context)
        {
            return await context.FindAsync<Subclass>(id);
        }

        /// <summary>
        /// Gets all of the <see cref="Subclass"/> objects from the database.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that you're using.</param>
        /// <returns>
        /// A <see cref="List{Subclass}"/> of all the <see cref="Subclass"/> objects in the database.
        /// </returns>
        public static List<Subclass> GetAllSubclasses(DbContext context )
        {
            return null;
        }
    }
}
