using Newtonsoft.Json;

namespace DnDesigner.Models.ImportModels
{
    public class ToolProficiency
    {
        public Choose choose { get; set; }

        [JsonProperty("herbalism kit")]
        public bool? herbalismkit { get; set; }

        [JsonProperty("vehicles (land)")]
        public bool? vehiclesland { get; set; }

        [JsonProperty("disguise kit")]
        public bool? disguisekit { get; set; }

        [JsonProperty("gaming set")]
        public bool? gamingset { get; set; }

        [JsonProperty("forgery kit")]
        public bool? forgerykit { get; set; }
        public int? anyArtisansTool { get; set; }

        [JsonProperty("tinker's tools")]
        public bool tinkerstools { get; set; }

        [JsonProperty("thieves' tools")]
        public bool? thievestools { get; set; }

        [JsonProperty("vehicles (water)")]
        public bool? vehicleswater { get; set; }

        [JsonProperty("musical instrument")]
        public bool? musicalinstrument { get; set; }

        [JsonProperty("alchemist's supplies")]
        public bool? alchemistssupplies { get; set; }

        [JsonProperty("cook's utensils")]
        public bool? cooksutensils { get; set; }
    }
}
