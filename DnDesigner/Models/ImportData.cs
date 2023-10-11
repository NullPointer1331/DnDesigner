using DnDesigner.Models.ImportModels;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Components.Web;

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

        public static ItemRoot GetItemRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\items.json");
            return JsonSerializer.Deserialize<ItemRoot>(contents);
        }
        public static Item ConvertItem(Item5ETools item5E)
        {
            Item item = new Item();
            item.Name = item5E.name;
            item.Sourcebook = item5E.source;
            item.Description = "";
            foreach (string entry in item5E.entries)
            {
                item.Description += entry;
            }
            item.Price = item5E.value ?? 0; //Might be the wrong unit
            item.Weight = item5E.weight ?? 0;
            item.Attuneable = item5E.reqAttune;
            item.Traits = item5E.type;
            foreach (string trait in item5E.property)
            {
                item.Traits += $", {trait}";
            }
            string traits = item.Traits.ToLower();
            if (traits.Contains("armor"))
            {
                item.Equipable = 1;
            }
            else if(traits.Contains("weapon"))
            {
                if(traits.Contains("light"))
                {
                    item.Equipable = 4;
                }
                else
                {
                    item.Equipable = 2;
                }
            }
            else if(traits.Contains("shield"))
            {
                item.Equipable = 3;
            }
            else
            {
                item.Equipable = 0;
            }
            return item;
        }
    }
}
