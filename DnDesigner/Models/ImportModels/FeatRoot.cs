using DnDesigner.Models.ImportModels;
using Newtonsoft.Json;

namespace DnDesigner.Models.ImportModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class _9
    {
        public Daily daily { get; set; }
    }

    public class _1
    {
        public string choose { get; set; }
    }

    public class AdditionalSpell
    {
        public Prepared prepared { get; set; }
        public object ability { get; set; }
        public Innate innate { get; set; }
        public Known known { get; set; }
        public string name { get; set; }
    }

    public class Feat5ETools
    {
        public int any { get; set; }
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public List<Prerequisite> prerequisite { get; set; }
        public List<Ability> ability { get; set; }
        public List<AdditionalSpell> additionalSpells { get; set; }
        public List<object> entries { get; set; }
        public List<SkillProficiency> skillProficiencies { get; set; }
        public bool? hasFluffImages { get; set; }
        public List<ToolProficiency> toolProficiencies { get; set; }
        public List<LanguageProficiency> languageProficiencies { get; set; }
    }

    public class Prerequisite
    {
        public string other { get; set; }
        public object level { get; set; }
        public List<string> feat { get; set; }
        public List<string> alignment { get; set; }
        public List<Race> race { get; set; }
        public List<Ability> ability { get; set; }
        public List<string> campaign { get; set; }
    }

    public class Rest
    {
        [JsonProperty("1")]
        public List<_1> _1 { get; set; }
    }

    public class FeatRoot
    {
        public List<Feat5ETools> feat { get; set; }
    }
}
