namespace DnDesigner.Models.ImportModels
{ 

    public class ItemRoot
    {
        public Meta _meta { get; set; }
        public List<Item5ETools> item { get; set; }
        public List<ItemGroup> itemGroup { get; set; }
    }
    public class Item5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public string rarity { get; set; }
        public object reqAttune { get; set; }
        public List<ReqAttuneTag> reqAttuneTags { get; set; }
        public bool wondrous { get; set; }
        public string bonusSpellAttack { get; set; }
        public string bonusSpellSaveDc { get; set; }
        public List<string> focus { get; set; }
        public List<object> entries { get; set; }
        public int? weight { get; set; }
        public string baseItem { get; set; }
        public string type { get; set; }
        public string weaponCategory { get; set; }
        public List<string> property { get; set; }
        public string dmg1 { get; set; }
        public string dmgType { get; set; }
        public string bonusWeapon { get; set; }
        public bool? grantsProficiency { get; set; }
        public string tier { get; set; }
        public List<string> lootTables { get; set; }
        public bool? srd { get; set; }
        public Copy _copy { get; set; }
        public string bonusAc { get; set; }
        public string bonusSavingThrow { get; set; }
        public List<string> optionalfeatures { get; set; }
        public bool? hasFluffImages { get; set; }
        public List<string> resist { get; set; }
        public int? ac { get; set; }
        public bool? basicRules { get; set; }
        public int? value { get; set; }
        public string recharge { get; set; }
        public string rechargeAmount { get; set; }
        public int? charges { get; set; }
        public List<string> miscTags { get; set; }
        public bool? tattoo { get; set; }
        public string detail1 { get; set; }
        public bool? hasRefs { get; set; }
        public List<string> attachedSpells { get; set; }
        public int? crew { get; set; }
        public int? vehAc { get; set; }
        public int? vehHp { get; set; }
        public int? vehSpeed { get; set; }
        public int? capPassenger { get; set; }
        public int? capCargo { get; set; }
        public List<string> conditionImmune { get; set; }
        public string dmg2 { get; set; }
        public List<AdditionalSource> additionalSources { get; set; }
        public List<object> additionalEntries { get; set; }
    }

    public class ItemGroup
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public string rarity { get; set; }
        public object reqAttune { get; set; }
        public bool wondrous { get; set; }
        public bool tattoo { get; set; }
        public List<object> entries { get; set; }
        public List<string> items { get; set; }
        public string baseItem { get; set; }
        public string type { get; set; }
        public List<ReqAttuneTag> reqAttuneTags { get; set; }
        public double? weight { get; set; }
        public string weaponCategory { get; set; }
        public List<string> property { get; set; }
        public string dmg1 { get; set; }
        public string dmgType { get; set; }
        public string bonusWeapon { get; set; }
        public bool? hasFluffImages { get; set; }
        public string scfType { get; set; }
        public List<string> focus { get; set; }
        public string tier { get; set; }
        public List<string> immune { get; set; }
        public List<string> resist { get; set; }
        public List<string> conditionImmune { get; set; }
        public int? ac { get; set; }
        public string bonusAc { get; set; }
        public bool? stealth { get; set; }
        public List<string> attachedSpells { get; set; }
        public bool? curse { get; set; }
        public string strength { get; set; }
        public List<string> lootTables { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
        public bool? sentient { get; set; }
        public string range { get; set; }
        public string recharge { get; set; }
        public int? charges { get; set; }
        public string ammoType { get; set; }
        public string bonusSavingThrow { get; set; }
        public string dmg2 { get; set; }
        public bool? hasFluff { get; set; }
        public bool? grantsProficiency { get; set; }
    }
    public class Preserve
    {
        public bool tier { get; set; }
        public bool? page { get; set; }
        public bool? fluff { get; set; }
    }

    public class ReqAttuneTag
    {
        public string @class { get; set; }
        public bool? spellcasting { get; set; }
        public List<string> alignment { get; set; }
        public string creatureType { get; set; }
    }
}