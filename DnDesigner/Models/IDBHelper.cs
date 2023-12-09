namespace DnDesigner.Models
{
    /// <summary>
    /// This interface provides a model to make DBHelpers.
    /// </summary>
    public interface IDBHelper
    {
        Task<Background> GetBackground(int id);
        Task<List<Background>> GetAllBackgrounds();
        Task<Character> GetCharacter(int id);
        Task<List<Character>> GetAllCharacters(string userId);
        Task<Class> GetClass(int id);
        Task<List<Class>> GetAllClasses();
        Task<Inventory> GetInventory(int id);
        Task<Item> GetItem(int id);
        Task<List<Item>> GetAllItems();
        Task<Proficiency> GetProficiency(int id);
        Task<List<Proficiency>> GetAllProficiencies();
        Task<Race> GetRace(int id);
        Task<List<Race>> GetAllRaces();
        Task<Spell> GetSpell(int id);
        Task<List<Spell>> GetAllSpells();
        Task<Spellcasting> GetSpellcasting(int id);
        Task<List<Spellcasting>> GetAllSpellcastings();
        Task<Subclass> GetSubclass(int id);
        Task<List<Subclass>> GetAllSubclasses();
    }
}
