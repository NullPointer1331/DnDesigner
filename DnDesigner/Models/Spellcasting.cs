namespace DnDesigner.Models
{
    public class Spellcasting
    {
        public int SpellcastingId { get; set; }
    }
    public class CharacterSpellcasting
    {
        public Character Character { get; set; }
        public Spellcasting Spellcasting { get; set; }
    }
}
