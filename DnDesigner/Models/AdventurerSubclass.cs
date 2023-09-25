namespace DnDesigner.Models
{
    public class AdventurerSubclass
    {
        public string Name { get; set; }
        public AdventurerClass Class { get; set; }
        public List<ClassFeature> Features { get; set; }
        public bool isSpellcaster { get; set; }
    }
}
