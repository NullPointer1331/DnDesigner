namespace DnDesigner.Models
{
    public class Spellcasting
    {
        public Character character { get; set; }
        public int SpellAttackBonus { get; set; }
        public int SpellSaveDC { get; set; }
        public List<Spell> KnownSpells { get; set; }
    }
}
