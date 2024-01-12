using Newtonsoft.Json;
namespace DnDesigner.Models.ImportModels
{
    public class LanguageProficiency
    {
        public int anyStandard { get; set; }
        public int any { get; set; }
        public bool? primordial { get; set; }
        public Choose choose { get; set; }
        public bool? dwarvish { get; set; }

        [JsonProperty("thieves' cant")]
        public bool? thievescant { get; set; }
        public bool? draconic { get; set; }
        public bool? undercommon { get; set; }
        public bool? giant { get; set; }
        public bool? auran { get; set; }
        public bool? common { get; set; }
        public bool? other { get; set; }
        public bool? celestial { get; set; }
        public bool? goblin { get; set; }
        public bool? sylvan { get; set; }
        public bool? elvish { get; set; }
        public bool? aquan { get; set; }
        public bool? gnomish { get; set; }
    }
}