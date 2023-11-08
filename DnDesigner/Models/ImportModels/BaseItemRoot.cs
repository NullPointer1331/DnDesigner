namespace DnDesigner.Models.ImportModels
{// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BaseItem5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public string type { get; set; }
        public string rarity { get; set; }
        public double weight { get; set; }
        public string weaponCategory { get; set; }
        public string age { get; set; }
        public List<string> property { get; set; }
        public string range { get; set; }
        public int reload { get; set; }
        public string dmg1 { get; set; }
        public string dmgType { get; set; }
        public bool firearm { get; set; }
        public bool weapon { get; set; }
        public string ammoType { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
        public int? value { get; set; }
        public List<PackContent> packContents { get; set; }
        public string dmg2 { get; set; }
        public bool? axe { get; set; }
        public int? ac { get; set; }
        public bool? armor { get; set; }
        public List<object> entries { get; set; }
        public string strength { get; set; }
        public bool? stealth { get; set; }
        public bool? club { get; set; }
        public string scfType { get; set; }
        public bool? dagger { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public bool? sword { get; set; }
        public bool? hasFluff { get; set; }
        public bool? crossbow { get; set; }
        public bool? spear { get; set; }
        public bool? hammer { get; set; }
        public bool? bow { get; set; }
        public bool? mace { get; set; }
    }

    public class EntriesTemplate
    {
        public string type { get; set; }
        public string name { get; set; }
        public int page { get; set; }
        public List<string> entries { get; set; }
    }

    public class Entry
    {
        public string type { get; set; }
        public string name { get; set; }
        public int page { get; set; }
        public List<string> entries { get; set; }
    }

    public class ItemEntry
    {
        public string name { get; set; }
        public string source { get; set; }
        public List<object> entriesTemplate { get; set; }
    }

    public class ItemProperty
    {
        public string abbreviation { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<Entry> entries { get; set; }
        public string template { get; set; }
        public List<EntriesTemplate> entriesTemplate { get; set; }
        public string name { get; set; }
    }

    public class ItemType
    {
        public string abbreviation { get; set; }
        public string source { get; set; }
        public string name { get; set; }
        public int page { get; set; }
        public List<string> entriesTemplate { get; set; }
        public List<object> entries { get; set; }
    }

    public class ItemTypeAdditionalEntry
    {
        public string appliesTo { get; set; }
        public string source { get; set; }
        public List<object> entries { get; set; }
    }

    public class PackContent
    {
        public string item { get; set; }
        public int quantity { get; set; }
    }

    public class BaseItemRoot
    {
        public List<Item5ETools> baseitem { get; set; }
        public List<ItemProperty> itemProperty { get; set; }
        public List<ItemType> itemType { get; set; }
        public List<ItemTypeAdditionalEntry> itemTypeAdditionalEntries { get; set; }
        public List<ItemEntry> itemEntry { get; set; }
    }
}
