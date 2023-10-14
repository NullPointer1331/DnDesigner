using Newtonsoft.Json;

namespace DnDesigner.Models.ImportModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class BackgroundRoot
    {
        public Meta _meta { get; set; }
        public List<Background5ETools> background { get; set; }
    }

    public class BackgroundAdditionalSpell
    {
        public Expanded expanded { get; set; }
    }

    public class Background5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public bool srd { get; set; }
        public bool basicRules { get; set; }
        public List<SkillProficiency> skillProficiencies { get; set; }
        public List<LanguageProficiency> languageProficiencies { get; set; }
        public List<BackgroundStartingEquipment> startingEquipment { get; set; }
        public List<Entries> entries { get; set; }
        public bool hasFluff { get; set; }
        public List<ToolProficiency> toolProficiencies { get; set; }
        public List<BackgroundFeat> feats { get; set; }
        public FromFeature fromFeature { get; set; }
        public bool? hasFluffImages { get; set; }
        public Copy _copy { get; set; }
        public List<BackgroundAdditionalSpell> additionalSpells { get; set; }
        public List<AdditionalSource> additionalSources { get; set; }
    }

    public class C
    {
        public string special { get; set; }
    }

    public class D
    {
        public string special { get; set; }
    }

    public class Data
    {
        public bool isFeature { get; set; }
    }
    public class EntryItem
    {
        public string type { get; set; }
        public string name { get; set; }
        public object entry { get; set; }
    }
    public class Entries
    {
        public string mode { get; set; }
        public int index { get; set; }
        public List<EntryItem> items { get; set; }
        public string replace { get; set; }
        public string type { get; set; }
        public string style { get; set; }
        public string name { get; set; }
        public List<object> entries { get; set; }
        public Data data { get; set; }
        public int? page { get; set; }
    }


    public class BackgroundFeat
    {
        [JsonProperty("magic initiate|phb")]
        public bool magicinitiatephb { get; set; }

        [JsonProperty("scion of the outer planes|ua2022wondersofthemultiverse")]
        public bool? scionoftheouterplanesua2022wondersofthemultiverse { get; set; }

        [JsonProperty("strike of the giants|bgg")]
        public bool? strikeofthegiantsbgg { get; set; }

        [JsonProperty("strike of the giants|ua2022wondersofthemultiverse")]
        public bool? strikeofthegiantsua2022wondersofthemultiverse { get; set; }
    }

    public class FromFeature
    {
        public bool feats { get; set; }
    }

    public class BackgroundItem
    {
        public string type { get; set; }
        public string name { get; set; }
        public string entry { get; set; }
    }

    public class BackgroundStartingEquipment
    {
        public List<object> _ { get; set; }
        public List<object> a { get; set; }
        public List<object> b { get; set; }
        public List<C> c { get; set; }
        public List<D> d { get; set; }
    }

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
    }
}
