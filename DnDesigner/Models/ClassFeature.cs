namespace DnDesigner.Models
{
    public class ClassFeature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public List<Modifier> Effects { get; set; }
    }
}
