using DnDesigner.Models.ImportModels;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;
using DnDesigner.Models;

namespace DnDesigner.Data
{
    public static class ImportData
    {
        /// <summary>
        /// Extracts Class Data from the 5ETools JSON files and returns it as a list of Classes
        /// </summary>
        public static List<Class> ExtractClasses()
        {
            List<ClassRoot> classRoots = GetClassRoot();
            List<Class> classes = new List<Class>();
            foreach (ClassRoot classRoot in classRoots)
            {
                foreach (Class5ETools class5E in classRoot.@class)
                {
                    classes.Add(ConvertClass(class5E));
                }
            }
            return classes;
        }

        /// <summary>
        /// Extracts Subclass Data from the 5ETools JSON files and returns it as a list of Subclasses
        /// </summary>
        /// <param name="classes">The list of Classes to tie the subclasses to</param>
        public static List<Subclass> ExtractSubclasses(List<Class> classes)
        {
            List<ClassRoot> classRoots = GetClassRoot();
            List<Subclass> subclasses = new List<Subclass>();
            List<Subclass5ETools> subclasses5E = new List<Subclass5ETools>();
            foreach (ClassRoot classRoot in classRoots)
            {
                subclasses5E.AddRange(classRoot.subclass);
            }
            foreach (Class @class in classes)
            {
                foreach (Subclass5ETools subclass5E in subclasses5E.Where(s => s.className == @class.Name).ToList())
                {
                    subclasses.Add(ConvertSubclass(subclass5E, @class));
                }
            }
            return subclasses;
        }

        /// <summary>
        /// Extracts Background Data from the 5ETools JSON files and returns it as a list of Backgrounds
        /// </summary>
        public static List<Background> ExtractBackgrounds()
        {
            BackgroundRoot backgroundRoot = GetBackgroundRoot();
            List<Background> backgrounds = new List<Background>();
            foreach (Background5ETools background5E in backgroundRoot.background)
            {
                backgrounds.Add(ConvertBackground(background5E));
            }
            return backgrounds;
        }

        /// <summary>
        /// Extracts Race Data from the 5ETools JSON files and returns it as a list of Races
        /// </summary>
        public static List<Race> ExtractRaces()
        {
            RaceRoot raceRoot = GetRaceRoot();
            List<Race> races = new List<Race>();
            foreach (Race5ETools race5E in raceRoot.race)
            {
                races.Add(ConvertRace(race5E));
            }
            return races;
        }

        /// <summary>
        /// Extracts Item Data from the 5ETools JSON files and returns it as a list of Items
        /// </summary>
        public static List<Item> ExtractItems()
        {
            ItemRoot itemRoot = GetItemRoot();
            List<Item> items = new List<Item>();
            foreach (Item5ETools item5E in itemRoot.item)
            {
                items.Add(ConvertItem(item5E));
            }
            return items;
        }

        /// <summary>
        /// Extracts Spell Data from the 5ETools JSON files and returns it as a list of Spells
        /// </summary>
        public static List<Spell> ExtractSpells()
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
            return spells;
        }

        /// <summary>
        /// Extracts Language Data from the 5ETools JSON files and returns it as a list of Proficiencies
        /// Also includes a list of hardcoded proficiencies
        /// </summary>
        public static List<Proficiency> ExtractProficiencies()
        {
            List<Proficiency> proficiencies = new List<Proficiency>();

            //Languages
            string contents = File.ReadAllText("Data\\5EToolsData\\languages.json");
            LanguageRoot languageRoot = JsonSerializer.Deserialize<LanguageRoot>(contents);
            foreach (Language language in languageRoot.language)
            {
                proficiencies.Add(new Proficiency(language.name, null, "language"));
            }

            //Weapons and armor
            proficiencies.Add(new Proficiency("Light Armor", null, "armor"));
            proficiencies.Add(new Proficiency("Medium Armor", null, "armor"));
            proficiencies.Add(new Proficiency("Heavy Armor", null, "armor"));
            proficiencies.Add(new Proficiency("Shield", null, "armor"));
            proficiencies.Add(new Proficiency("Simple Weapons", null, "weapon"));
            proficiencies.Add(new Proficiency("Martial Weapons", null, "weapon"));

            //Saving Throws
            proficiencies.Add(new Proficiency("Strength Saving Throw", "Strength", "saving throw"));
            proficiencies.Add(new Proficiency("Dexterity Saving Throw", "Dexterity", "saving throw"));
            proficiencies.Add(new Proficiency("Constitution Saving Throw", "Constitution", "saving throw"));
            proficiencies.Add(new Proficiency("Intelligence Saving Throw", "Intelligence", "saving throw"));
            proficiencies.Add(new Proficiency("Wisdom Saving Throw", "Wisdom", "saving throw"));
            proficiencies.Add(new Proficiency("Charisma Saving Throw", "Charisma", "saving throw"));

            //Skills
            proficiencies.Add(new Proficiency("Acrobatics", "Dexterity", "skill"));
            proficiencies.Add(new Proficiency("Animal Handling", "Wisdom", "skill"));
            proficiencies.Add(new Proficiency("Arcana", "Intelligence", "skill"));
            proficiencies.Add(new Proficiency("Athletics", "Strength", "skill"));
            proficiencies.Add(new Proficiency("Deception", "Charisma", "skill"));
            proficiencies.Add(new Proficiency("History", "Intelligence", "skill"));
            proficiencies.Add(new Proficiency("Insight", "Wisdom", "skill"));
            proficiencies.Add(new Proficiency("Intimidation", "Charisma", "skill"));
            proficiencies.Add(new Proficiency("Investigation", "Intelligence", "skill"));
            proficiencies.Add(new Proficiency("Medicine", "Wisdom", "skill"));
            proficiencies.Add(new Proficiency("Nature", "Intelligence", "skill"));
            proficiencies.Add(new Proficiency("Perception", "Wisdom", "skill"));
            proficiencies.Add(new Proficiency("Performance", "Charisma", "skill"));
            proficiencies.Add(new Proficiency("Persuasion", "Charisma", "skill"));
            proficiencies.Add(new Proficiency("Religion", "Intelligence", "skill"));
            proficiencies.Add(new Proficiency("Sleight of Hand", "Dexterity", "skill"));
            proficiencies.Add(new Proficiency("Stealth", "Dexterity", "skill"));
            proficiencies.Add(new Proficiency("Survival", "Wisdom", "skill"));

            //TODO: add tool and instrument proficiencies
            return proficiencies;
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
            if (item5E.entries != null)
            {
                foreach (object entry in item5E.entries)
                {
                    item.Description += entry.ToString();
                }
            }
            item.Price = item5E.value ?? 0; //Might be the wrong unit
            item.Weight = item5E.weight ?? 0;
            if (item5E.reqAttune != null && item5E.reqAttune.ToString().ToLower().Equals("true"))
            {
                item.Attuneable = true;
            }
            else
            {
                item.Attuneable = false;
            }

            item.Traits = item5E.type;
            if (item5E.property != null)
            {
                foreach (string trait in item5E.property)
                {
                    item.Traits += $", {trait}";
                }
            }
            item.Equipable = 0;
            if (item.Traits != null)
            {
                string traits = item.Traits.ToLower();
                if (traits.Contains("armor"))
                {
                    item.Equipable = 1;
                }
                else if (traits.Contains("weapon"))
                {
                    if (traits.Contains("light"))
                    {
                        item.Equipable = 4;
                    }
                    else
                    {
                        item.Equipable = 2;
                    }
                }
                else if (traits.Contains("shield"))
                {
                    item.Equipable = 3;
                }
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
            if (race5E.entries != null)
            {
                foreach (object entry in race5E.entries)
                {
                    race.Description += entry.ToString();
                }
            }
            race.StatBonuses = "";
            if (race5E.ability != null)
            {
                foreach (Ability ability in race5E.ability)
                {
                    if (ability.choose != null)
                    {
                        race.StatBonuses += $"+{ability.choose.amount} to {ability.choose.count} attribute. ";
                    }
                    if (ability.cha != null)
                    {
                        race.StatBonuses += $"+{ability.cha} Charisma. ";
                    }
                    if (ability.con != null)
                    {
                        race.StatBonuses += $"+{ability.con} Constitution. ";
                    }
                    if (ability.dex != null)
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
            }
            race.Size = "medium";
            race.Speed = 30;
            //TODO: Proficiencies, Features, Subraces, actually check size and speed
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
            if (class5E.spellcastingAbility != null)
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
            if (subclass5E.spellcastingAbility != null)
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
            @class.Subclasses.Add(subclass);
            return subclass;
        }
    }
}
