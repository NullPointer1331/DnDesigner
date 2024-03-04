using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
    public class Source
    {
        [Key]
        public int SourceId { get; set; }

        /// <summary>
        /// The name of the source 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The initials of the source 
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// The type of source, can be Official, Homebrew, or UA
        /// </summary>
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
