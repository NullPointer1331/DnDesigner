using Newtonsoft.Json;

namespace DnDesigner.Models.ImportModels
{
    public class SkillProficiency
    {

        public bool? intimidation { get; set; }
        public bool? perception { get; set; }
        public bool? stealth { get; set; }
        public Choose choose { get; set; }
        public bool? survival { get; set; }
        public bool? deception { get; set; }
        public int? any { get; set; }
        public bool? insight { get; set; }
        public bool? religion { get; set; }
        public bool? history { get; set; }
        public bool? nature { get; set; }
        public bool? acrobatics { get; set; }
        public bool? athletics { get; set; }

        [JsonProperty("animal handling")]
        public bool? animalhandling { get; set; }
        public bool? performance { get; set; }

        [JsonProperty("sleight of hand")]
        public bool? sleightofhand { get; set; }
        public bool? persuasion { get; set; }
        public bool? investigation { get; set; }
        public bool? arcana { get; set; }
    }
}
