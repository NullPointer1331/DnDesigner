﻿using DnDesigner.Models.ImportModels;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace DnDesigner.Models
{
    public static class ImportData
    {
        public static void ImportDataFrom5ETools()
        {
            ImportSpells();
            ImportItems();
            ImportRaces();
            ImportBackgrounds();
            ImportClasses();
        }

        private static void ImportClasses()
        {
            List<ClassRoot> classRoots = GetClassRoot();
            List<Class> classes = new List<Class>();
            List<Subclass> subclasses = new List<Subclass>();
            foreach (ClassRoot classRoot in classRoots)
            {
                foreach (Class5ETools class5E in classRoot.@class)
                {
                    Class @class = ConvertClass(class5E);
                    classes.Add(@class);
                    List<Subclass5ETools> subclasses5E = classRoot.subclass.Where(s => s.className == @class.Name).ToList();
                    foreach (Subclass5ETools subclass5E in subclasses5E)
                    {
                        subclasses.Add(ConvertSubclass(subclass5E, @class));
                    }
                }
            }
            //TODO: Add to database
        }

        private static void ImportBackgrounds()
        {
            BackgroundRoot backgroundRoot = GetBackgroundRoot();
            List<Background> backgrounds = new List<Background>();
            foreach (Background5ETools background5E in backgroundRoot.background)
            {
                backgrounds.Add(ConvertBackground(background5E));
            }
            //TODO: Add to database
        }

        private static void ImportRaces()
        {
            RaceRoot raceRoot = GetRaceRoot();
            List<Race> races = new List<Race>();
            foreach (Race5ETools race5E in raceRoot.race)
            {
                races.Add(ConvertRace(race5E));
            }
            //TODO: Add to database
        }

        private static void ImportItems()
        {
            ItemRoot itemRoot = GetItemRoot();
            List<Item> items = new List<Item>();
            foreach (Item5ETools item5E in itemRoot.item)
            {
                items.Add(ConvertItem(item5E));
            }
            //TODO: Add to database
        }

        private static void ImportSpells()
        {
            List<SpellRoot> spellRoots = GetSpellRoots();
            List<Spell> spells = new List<Spell>();
            foreach (SpellRoot spellRoot in spellRoots)
            {
                foreach (Spell5ETools spell5E in spellRoot.spell)
                {
                    spells.Add(ConvertSpell(spell5E));
                }
            }
            //TODO: Add to database
        }

        public static List<SpellRoot> GetSpellRoots()
        {
            List<SpellRoot> spellRoots = new List<SpellRoot>();
            foreach (string file in Directory.EnumerateFiles("Data\\5EToolsData\\spells", " *.json"))
            {
                string contents = File.ReadAllText(file);
                spellRoots.Add(JsonSerializer.Deserialize<SpellRoot>(contents));
            }
            return spellRoots;
            //TODO: Add to database
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
            //TODO: Spell Lists
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
        
        public static RaceRoot GetRaceRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\races.json");
            return JsonSerializer.Deserialize<RaceRoot>(contents);
        }
        public static Race ConvertRace(Race5ETools race5E)
        {
            Race race = new Race();
            race.Name = race5E.name;
            race.Sourcebook = race5E.source;
            race.Description = "";
            foreach (string entry in race5E.entries)
            {
                race.Description += entry;
            }
            race.StatBonuses = "";
            foreach (Ability ability in race5E.ability)
            {
                if(ability.choose != null)
                {
                    race.StatBonuses += $"+{ability.choose.amount} to {ability.choose.count} attribute. ";
                }
                if(ability.cha != null)
                {
                    race.StatBonuses += $"+{ability.cha} Charisma. ";
                }
                if(ability.con != null)
                {
                    race.StatBonuses += $"+{ability.con} Constitution. ";
                }
                if(ability.dex != null)
                {
                    race.StatBonuses += $"+{ability.dex} Dexterity. ";
                }
                if (ability.@int != null)
                {
                    race.StatBonuses += $"+{ability.@int} Intelligence. ";
                }
                if (ability.str != null)
                {
                    race.StatBonuses += $"+{ability.str} Strength. ";
                }
                if (ability.wis != null)
                {
                    race.StatBonuses += $"+{ability.wis} Wisdom. ";
                }
            }
            race.Size = race5E.size[0];
            int.TryParse(race5E.speed, out int speed);
            race.Speed = speed;
            //TODO: Proficiencies and Features
            return race;
        }

        public static BackgroundRoot GetBackgroundRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\backgrounds.json");
            return JsonSerializer.Deserialize<BackgroundRoot>(contents);
        }
        public static Background ConvertBackground(Background5ETools background5E)
        {
            Background background = new Background();
            background.Name = background5E.name;
            background.Sourcebook = background5E.source;
            //TODO: basically everything
            return background;
        }

        public static List<ClassRoot> GetClassRoot()
        {
            List<ClassRoot> classRoots = new List<ClassRoot>();
            foreach (string file in Directory.EnumerateFiles("Data\\5EToolsData\\class", " *.json"))
            {
                string contents = File.ReadAllText(file);
                classRoots.Add(JsonSerializer.Deserialize<ClassRoot>(contents));
            }
            return classRoots;
        }
        public static Class ConvertClass(Class5ETools class5E)
        {
            Class @class = new Class();
            @class.Name = class5E.name;
            @class.Sourcebook = class5E.source;
            @class.HitDie = class5E.hd.faces;
            if(class5E.spellcastingAbility != null)
            {
                Spellcasting spellcasting = new Spellcasting();
                spellcasting.Name = @class.Name;
                spellcasting.SpellcastingAttribute = class5E.spellcastingAbility;
                spellcasting.SpellcastingType = class5E.casterProgression ?? "none";
                @class.Spellcasting = spellcasting;
            }
            foreach (ClassFeature5ETools feature5E in class5E.classFeatures)
            {
                string description = "";
                foreach (string entry in feature5E.entries)
                {
                    description += entry;
                }
                ClassFeature feature = new ClassFeature(@class, feature5E.name, description, feature5E.level);
                @class.Features.Add(feature);
            }
            //TODO: Proficiencies
            return @class;
        }
        public static Subclass ConvertSubclass(Subclass5ETools subclass5E, Class @class)
        {
            Subclass subclass = new Subclass(@class, subclass5E.name);
            subclass.Sourcebook = subclass5E.source;
            if(subclass5E.spellcastingAbility != null)
            {
                Spellcasting spellcasting = new Spellcasting();
                spellcasting.Name = subclass.Name;
                spellcasting.SpellcastingAttribute = subclass5E.spellcastingAbility;
                spellcasting.SpellcastingType = "third";
                subclass.Spellcasting = spellcasting;
            }
            foreach (SubclassFeature5ETools feature5E in subclass5E.subclassFeatures)
            {
                string description = "";
                foreach (string entry in feature5E.entries)
                {
                    description += entry;
                }
                SubclassFeature feature = new SubclassFeature(subclass, feature5E.name, description, feature5E.level);
                subclass.Features.Add(feature);
            }
            return subclass;
        }
    }
}