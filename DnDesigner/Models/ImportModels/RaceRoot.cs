using Newtonsoft.Json;
namespace DnDesigner.Models.ImportModels
{

    public class RaceRoot
    {
        public Meta _meta { get; set; }
        public List<Race5ETools> race { get; set; }
        public List<Subrace5ETools> subrace { get; set; }
    }
    
    public class Race5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<string> size { get; set; }
        public object speed { get; set; }
        public List<Ability> ability { get; set; }
        public List<string> traitTags { get; set; }
        public List<LanguageProficiency> languageProficiencies { get; set; }
        public List<object> entries { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public List<string> reprintedAs { get; set; }
        public Age age { get; set; }
        public SoundClip soundClip { get; set; }
        public bool? hasFluff { get; set; }
        public bool? hasFluffImages { get; set; }
        public object lineage { get; set; }
        public List<RaceAdditionalSpell> additionalSpells { get; set; }
        public int? darkvision { get; set; }
        public List<object> resist { get; set; }
        public List<Version> _versions { get; set; }
        public HeightAndWeight heightAndWeight { get; set; }
        public List<SkillProficiency> skillProficiencies { get; set; }
        public List<object> creatureTypes { get; set; }
        public List<string> creatureTypeTags { get; set; }
        public List<RaceToolProficiency> toolProficiencies { get; set; }
        public List<string> conditionImmune { get; set; }
        public Copy _copy { get; set; }
        public List<Feat5ETools> feats { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
        public List<WeaponProficiency> weaponProficiencies { get; set; }
        public List<AdditionalSource> additionalSources { get; set; }
        public string subrace { get; set; }
    }
    public class Subrace5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public string raceName { get; set; }
        public string raceSource { get; set; }
        public int page { get; set; }
        public List<Ability> ability { get; set; }
        public List<object> entries { get; set; }
        public bool hasFluff { get; set; }
        public bool hasFluffImages { get; set; }
        public List<SkillProficiency> skillProficiencies { get; set; }
        public bool? srd { get; set; }
        public List<Version> _versions { get; set; }
        public int? darkvision { get; set; }
        public List<string> resist { get; set; }
        public Overwrite overwrite { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public List<string> reprintedAs { get; set; }
        public List<string> traitTags { get; set; }
        public List<LanguageProficiency> languageProficiencies { get; set; }
        public List<RaceAdditionalSpell> additionalSpells { get; set; }
        public bool? basicRules { get; set; }
        public HeightAndWeight heightAndWeight { get; set; }
        public List<ArmorProficiency> armorProficiencies { get; set; }
        public object speed { get; set; }
        public List<string> alias { get; set; }
        public List<WeaponProficiency> weaponProficiencies { get; set; }
        public List<SkillToolLanguageProficiency> skillToolLanguageProficiencies { get; set; }
        public Age age { get; set; }
    }
    public class _3
    {
        public Daily daily { get; set; }
    }
    public class _5
    {
        public Daily daily { get; set; }
    }
    public class Ability
    {
        public int? dex { get; set; }
        public int? wis { get; set; }
        public int? cha { get; set; }
        public Choose? choose { get; set; }
        public int? str { get; set; }
        public int? @int { get; set; }
        public int? con { get; set; }
    }
    public class RaceAdditionalSpell
    {
        public Innate innate { get; set; }
        public object ability { get; set; }
        public Known known { get; set; }
        public Expanded expanded { get; set; }
    }
    public class Age
    {
        public int mature { get; set; }
        public int max { get; set; }
    }
    public class ArmorProficiency
    {
        public bool light { get; set; }
        public bool medium { get; set; }
    }
    public class Daily
    {
        [JsonProperty("1e")]
        public List<object> _1e { get; set; }
        [JsonProperty("1")]
        public List<string> _1 { get; set; }
    }
    public class HeightAndWeight
    {
        public int baseHeight { get; set; }
        public string heightMod { get; set; }
        public int baseWeight { get; set; }
        public string weightMod { get; set; }
    }
    public class Implementation
    {
        public Variables _variables { get; set; }
        public List<string> resist { get; set; }
    }
    public class Innate
    {
        [JsonProperty("9")]
        public _9 _9 { get; set; }

        [JsonProperty("3")]
        public object _3 { get; set; }

        [JsonProperty("5")]
        public _5 _5 { get; set; }

        [JsonProperty("1")]
        public object _1 { get; set; }
    }
    public class Known
    {
        public List<object> _ { get; set; }

        [JsonProperty("1")]
        public object _1 { get; set; }
    }
    public class Items
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<string> entries { get; set; }
    }
    public class Overwrite
    {
        public bool ability { get; set; }
        public bool traitTags { get; set; }
        public bool? languageProficiencies { get; set; }
    }
    public class SkillToolLanguageProficiency
    {
        public List<Choose> choose { get; set; }
    }
    public class SoundClip
    {
        public string type { get; set; }
        public string path { get; set; }
    }
    public class Template
    {
        public string name { get; set; }
        public string source { get; set; }
        public Mod _mod { get; set; }
    }
    public class RaceToolProficiency
    {
        public int any { get; set; }
    }
    public class Variables
    {
        public string color { get; set; }
        public string damageType { get; set; }
        public string area { get; set; }
        public string savingThrow { get; set; }
    }
    public class Version
    {
        public string name { get; set; }
        public string source { get; set; }
        public Mod _mod { get; set; }
        public List<string> traitTags { get; set; }
        public object skillProficiencies { get; set; }
        public object darkvision { get; set; }
        public Template _template { get; set; }
        public List<Implementation> _implementations { get; set; }
    }
    public class WeaponProficiency
    {
        [JsonProperty("battleaxe|phb")]
        public bool battleaxephb { get; set; }

        [JsonProperty("handaxe|phb")]
        public bool handaxephb { get; set; }

        [JsonProperty("light hammer|phb")]
        public bool lighthammerphb { get; set; }

        [JsonProperty("warhammer|phb")]
        public bool warhammerphb { get; set; }

        [JsonProperty("longsword|phb")]
        public bool? longswordphb { get; set; }

        [JsonProperty("shortsword|phb")]
        public bool? shortswordphb { get; set; }

        [JsonProperty("shortbow|phb")]
        public bool? shortbowphb { get; set; }

        [JsonProperty("longbow|phb")]
        public bool? longbowphb { get; set; }

        [JsonProperty("rapier|phb")]
        public bool rapierphb { get; set; }

        [JsonProperty("hand crossbow|phb")]
        public bool handcrossbowphb { get; set; }

        [JsonProperty("spear|phb")]
        public bool? spearphb { get; set; }

        [JsonProperty("net|phb")]
        public bool? netphb { get; set; }

        [JsonProperty("scimitar|phb")]
        public bool? scimitarphb { get; set; }

        [JsonProperty("double-bladed scimitar|erlw")]
        public bool? doublebladedscimitarerlw { get; set; }

        [JsonProperty("trident|phb")]
        public bool? tridentphb { get; set; }

        [JsonProperty("light crossbow|phb")]
        public bool? lightcrossbowphb { get; set; }

        [JsonProperty("greatsword|phb")]
        public bool? greatswordphb { get; set; }
    }
}