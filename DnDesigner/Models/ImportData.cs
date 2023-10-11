using DnDesigner.Models.ImportModels;
using System.Text.Json;
using System.IO;

namespace DnDesigner.Models
{
    public static class ImportData
    {
        public static List<SpellRoot> GetSpellRoots()
        {
            List<SpellRoot> spellRoots = new List<SpellRoot>();
            foreach (string file in Directory.EnumerateFiles("Data\\5EToolsData\\spells", " *.json"))
            {
                string contents = File.ReadAllText(file);
                spellRoots.Add(JsonSerializer.Deserialize<SpellRoot>(contents));
            }
            return spellRoots;
        }
        public static Spell ConvertSpell(Spell5ETools spell5E)
        {
            Spell spell = new Spell();
            spell.Name = spell5E.name;
            spell.Sourcebook = spell5E.source;
            spell.SpellLevel = spell5E.level;
            spell.SpellSchool = spell5E.school;
            spell.CastingTime = $"{spell5E.time[0].number} {spell5E.time[0].unit}";
            spell.Range = $"{spell5E.range.distance.amount} {spell5E.range.distance.type}";
            spell.Components = "";
            if (spell5E.components.v)
            {
                spell.Components += "V ";
            }
            if (spell5E.components.s)
            {
                spell.Components += "S ";
            }
            if (spell5E.components.m != null)
            {
                spell.Components += $"M ({spell5E.components.m})";
            }
            spell.Duration = $"{spell5E.duration[0].amount} {spell5E.duration[0].type}";
            spell.RequiresConcentration = spell5E.duration[0].concentration ?? false;
            spell.IsRitual = spell5E.meta.ritual;
            spell.Description = "";
            foreach (string entry in spell5E.entries)
            {
                spell.Description += entry;
            }
            return spell;
        }
    }
}
