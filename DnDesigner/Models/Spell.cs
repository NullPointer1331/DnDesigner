namespace DnDesigner.Models
{
    public class Spell
    {
        public int SpellId { get; set; }
        public string SpellName { get; set; }
        public int SpellLevel { get; set; }
        public string SpellSchool { get; set; }
        public string CastingTime { get; set; }
        public string Range { get; set; }
        public string Components { get; set; }
        public string Duration { get; set; }
        public string SpellDescription { get; set; }
        public List<Spellcasting> Learnable { get; set; }
    }
}
