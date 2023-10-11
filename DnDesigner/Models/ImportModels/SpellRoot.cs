using Newtonsoft.Json;
namespace DnDesigner.Models.ImportModels
{

    public class SpellRoot
    {
        public List<Spell5ETools> spell { get; set; }
    }
    public class Spell5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public object srd { get; set; }
        public bool basicRules { get; set; }
        public int level { get; set; }
        public string school { get; set; }
        public List<Time> time { get; set; }
        public Range range { get; set; }
        public Components components { get; set; }
        public List<Duration> duration { get; set; }
        public List<object> entries { get; set; }
        public ScalingLevelDice scalingLevelDice { get; set; }
        public List<string> damageInflict { get; set; }
        public List<string> savingThrow { get; set; }
        public List<string> miscTags { get; set; }
        public List<string> areaTags { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public List<EntriesHigherLevel> entriesHigherLevel { get; set; }
        public SpellMeta meta { get; set; }
        public List<string> conditionInflict { get; set; }
        public List<string> affectsCreatureType { get; set; }
        public List<string> damageResist { get; set; }
        public bool? hasFluffImages { get; set; }
        public List<string> spellAttack { get; set; }
        public List<string> abilityCheck { get; set; }
    }
    public class Components
    {
        public bool v { get; set; }
        public bool s { get; set; }
        public object m { get; set; }
    }
    public class Distance
    {
        public string type { get; set; }
        public int amount { get; set; }
    }
    public class Duration
    {
        public string type { get; set; }
        public Duration duration { get; set; }
        public bool? concentration { get; set; }
        public List<string> ends { get; set; }
        public int amount { get; set; }
    }
    public class EntriesHigherLevel
    {
        public string type { get; set; }
        public string name { get; set; }
        public List<string> entries { get; set; }
    }
    public class SpellMeta
    {
        public bool ritual { get; set; }
    }
    public class Range
    {
        public string type { get; set; }
        public Distance distance { get; set; }
    }
    public class Scaling
    {
        [JsonProperty("1")]
        public string _1 { get; set; }

        [JsonProperty("5")]
        public string _5 { get; set; }

        [JsonProperty("11")]
        public string _11 { get; set; }

        [JsonProperty("17")]
        public string _17 { get; set; }
    }
    public class ScalingLevelDice
    {
        public string label { get; set; }
        public Scaling scaling { get; set; }
    }
    public class Time
    {
        public int number { get; set; }
        public string unit { get; set; }
    }
}