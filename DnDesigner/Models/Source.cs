namespace DnDesigner.Models
{
    public class Source
    {
        public int SourceId { get; set; }

        public string Name { get; set; }

        public string Initials { get; set; }

        public string Type { get; set; }

        public List<Class> Classes { get; set; }

        public List<Subclass> Subclasses { get; set; }

        public List<Race> Races { get; set; }

        public List<Background> Backgrounds { get; set; }

        public List<Feature> Features { get; set; }

        public List<Spell> Spells { get; set; }

        public List<Item> Items { get; set; }

        public Source(string name, string initials, string type)
        {
            Name = name;
            Initials = initials;
            Type = type;
            Classes = new List<Class>();
            Subclasses = new List<Subclass>();
            Races = new List<Race>();
            Backgrounds = new List<Background>();
            Features = new List<Feature>();
            Spells = new List<Spell>();
            Items = new List<Item>();
        }

        private Source() { }
    }
}
