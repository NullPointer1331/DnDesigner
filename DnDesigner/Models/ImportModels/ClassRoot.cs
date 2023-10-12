using Newtonsoft.Json;

namespace DnDesigner.Models.ImportModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ClassRoot
    {
        public List<Class5ETools> @class { get; set; }
        public List<Subclass5ETools> subclass { get; set; }
        public List<ClassFeature5ETools> classFeature { get; set; }
        public List<SubclassFeature5ETools> subclassFeature { get; set; }
    }

    public class AdditionalSpell
    {
        public Prepared prepared { get; set; }
    }
    public class B
    {
        public string equipmentType { get; set; }
    }

    public class Class5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public bool isReprinted { get; set; }
        public Hd hd { get; set; }
        public List<string> proficiency { get; set; }
        public string? spellcastingAbility { get; set; }
        public string? casterProgression { get; set; }
        public List<int> spellsKnownProgression { get; set; }
        public StartingProficiencies startingProficiencies { get; set; }
        public StartingEquipment startingEquipment { get; set; }
        public List<ClassTableGroup> classTableGroups { get; set; }
        public List<object> classFeatures { get; set; }
        public string subclassTitle { get; set; }
        public string preparedSpells { get; set; }
        public List<int> cantripProgression { get; set; }
        public List<object> optionalfeatureProgression { get; set; }
        public Multiclassing multiclassing { get; set; }
        public List<Fluff> fluff { get; set; }
        public List<OtherSource> otherSources { get; set; }
    }

    public class ClassFeature5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public bool srd { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public string? className { get; set; }
        public string? classSource { get; set; }
        public int level { get; set; }
        public List<object> entries { get; set; }
        public int? header { get; set; }
        public bool? isClassFeatureVariant { get; set; }
    }

    public class ClassTableGroup
    {
        public List<string> colLabels { get; set; }
        public List<List<object>> rows { get; set; }
        public string title { get; set; }
        public List<List<int>> rowsSpellProgression { get; set; }
    }

    public class DefaultDatum
    {
        public List<object> a { get; set; }
        public List<object> b { get; set; }
        public List<object> _ { get; set; }
    }

    public class Fluff
    {
        public string name { get; set; }
        public int page { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public List<object> entries { get; set; }
    }

    public class Hd
    {
        public int number { get; set; }
        public int faces { get; set; }
    }

    public class Multiclassing
    {
        public Requirements requirements { get; set; }
        public ProficienciesGained proficienciesGained { get; set; }
    }

    public class OptionalfeatureProgression
    {
        public string name { get; set; }
        public List<string> featureType { get; set; }
        public List<int> progression { get; set; }
    }

    public class Prepared
    {
        [JsonProperty("3")]
        public List<string> _3 { get; set; }

        [JsonProperty("5")]
        public List<string> _5 { get; set; }

        [JsonProperty("9")]
        public List<string> _9 { get; set; }

        [JsonProperty("13")]
        public List<string> _13 { get; set; }

        [JsonProperty("17")]
        public List<string> _17 { get; set; }
    }

    public class ProficienciesGained
    {
        public List<object> armor { get; set; }
        public List<object> tools { get; set; }
        public List<object> weapons { get; set; }
        public List<ToolProficiency> toolProficiencies { get; set; }
    }

    public class Progression
    {
        [JsonProperty("1")]
        public int _1 { get; set; }

        [JsonProperty("3")]
        public int _3 { get; set; }

        [JsonProperty("9")]
        public int _9 { get; set; }

        [JsonProperty("14")]
        public int _14 { get; set; }

        [JsonProperty("17")]
        public int _17 { get; set; }
    }

    public class Requirements
    {
        public int @int { get; set; }
        public int str { get; set; }
        public int dex { get; set; }
        public int cha { get; set; }
        public int con { get; set; }
        public int wis { get; set; }
    }

    public class Skill
    {
        public Choose choose { get; set; }
        public List<string> weapons { get; set; }
        public List<Skill> skills { get; set; }
    }

    public class StartingEquipment
    {
        public bool additionalFromBackground { get; set; }
        public List<string> @default { get; set; }
        public List<DefaultDatum> defaultData { get; set; }
        public string goldAlternative { get; set; }
    }

    public class StartingProficiencies
    {
        public List<object> armor { get; set; }
        public List<object> weapons { get; set; }
        public List<object> tools { get; set; }
        public List<ToolProficiency> toolProficiencies { get; set; }
        public List<Skill> skills { get; set; }
    }

    public class Subclass5ETools
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public string source { get; set; }
        public string className { get; set; }
        public string classSource { get; set; }
        public int page { get; set; }
        public List<object> optionalfeatureProgression { get; set; }
        public List<object> subclassFeatures { get; set; }
        public List<AdditionalSpell> additionalSpells { get; set; }
        public bool? isReprinted { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public string? spellcastingAbility { get; set; }
    }

    public class SubclassFeature5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public string className { get; set; }
        public string classSource { get; set; }
        public string subclassShortName { get; set; }
        public string subclassSource { get; set; }
        public int level { get; set; }
        public List<object> entries { get; set; }
        public int? header { get; set; }
        public bool? srd { get; set; }
        public string type { get; set; }
    }


}
