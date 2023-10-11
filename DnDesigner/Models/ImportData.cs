using DnDesigner.Models.ImportModels;
using System.Text.Json;

namespace DnDesigner.Models
{
    public static class ImportData
    {
        public static SpellRoot GetSpellRoots()
        {
            var json = File.ReadAllText("Data\\5EToolsData\\spells\\spells-phb.json");
            return JsonSerializer.Deserialize<SpellRoot>(json);
        }
        public static Spell ConvertSpell(SpellRoot spell)
        {
            return new Spell();
        }
    }
}
