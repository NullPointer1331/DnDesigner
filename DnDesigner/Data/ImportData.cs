using DnDesigner.Models.ImportModels;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;
using DnDesigner.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Humanizer;
using System.Collections.Generic;
using DnDesigner.Controllers;
using Elfie.Serialization;
using System.Linq.Expressions;

namespace DnDesigner.Data
{
    public static class ImportData
    {
        #region Extraction methods
        /// <summary>
        /// Extracts Class Data from the 5ETools JSON files and returns it as a list of Classes
        /// </summary>
        public static List<Class> ExtractClasses(List<Proficiency> proficiencies)
        {
            List<ClassRoot> classRoots = GetClassRoot();
            List<Class> classes = new List<Class>();
            foreach (ClassRoot classRoot in classRoots)
            {
                if(classRoot.@class != null)
                {
                    foreach (Class5ETools class5E in classRoot.@class)
                    {
                        if (!class5E.source.Contains("UA"))
                        {
                            List<ClassFeature5ETools> classFeatures = classRoot.classFeature
                                .Where(f => f.className == class5E.name && f.source.Contains("UA")).ToList();
                            classes.Add(ConvertClass(class5E, classFeatures, proficiencies));
                        }
                    }
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
                if (classRoot.subclass != null)
                {
                    foreach (Subclass5ETools subclass5E in classRoot.subclass)
                    {
                        if (!subclass5E.source.Contains("UA"))
                        {
                            Class @class = classes.Where(c => c.Name == subclass5E.className).FirstOrDefault();
                            List<SubclassFeature5ETools> subclassFeatures = classRoot.subclassFeature.Where(f => f.subclassShortName == subclass5E.shortName).ToList();
                            if (@class != null)
                            {
                                subclasses.Add(ConvertSubclass(subclass5E, @class, subclassFeatures));
                            }
                        }
                    }
                }
            }
            return subclasses;
        }

        /// <summary>
        /// Extracts Background Data from the 5ETools JSON files and returns it as a list of Backgrounds
        /// </summary>
        public static List<Background> ExtractBackgrounds(List<Proficiency> proficiencies)
        {
            BackgroundRoot backgroundRoot = GetBackgroundRoot();
            List<Background> backgrounds = new List<Background>();
            foreach (Background5ETools background5E in backgroundRoot.background)
            {
                if(!background5E.source.Contains("UA"))
                {
                    backgrounds.Add(ConvertBackground(background5E, proficiencies));
                }
            }
            return backgrounds;
        }

        /// <summary>
        /// Extracts Race Data from the 5ETools JSON files and returns it as a list of Races
        /// </summary>
        public static List<Race> ExtractRaces(List<Proficiency> proficiencies)
        {
            RaceRoot raceRoot = GetRaceRoot();
            List<Race> races = new List<Race>();
            foreach (Race5ETools race5E in raceRoot.race)
            {
                if(!race5E.source.Contains("UA"))
                {
                    races.Add(ConvertRace(race5E, proficiencies));
                }
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
                if(!item5E.source.Contains("UA"))
                {
                    items.Add(ConvertItem(item5E));
                }
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
                if (spellRoot.spell != null)
                {
                    foreach (Spell5ETools spell5E in spellRoot.spell)
                    {
                        if (!spell5E.source.Contains("UA"))
                        {
                            spells.Add(ConvertSpell(spell5E));
                        }
                    }
                }
            }
            return spells;
        }

        /// <summary>
        /// Extracts Language Data from the 5ETools JSON files and returns it as a list of Proficiencies
        /// Also includes a list of hardcoded proficiencies
        /// </summary>
        public static List<Proficiency> ExtractProficiencies(List<Item> items)
        {
            List<Proficiency> proficiencies = new List<Proficiency>();

            //Languages
            string contents = File.ReadAllText("Data\\5EToolsData\\languages.json");
            LanguageRoot languageRoot = JsonSerializer.Deserialize<LanguageRoot>(contents);
            foreach (Language language in languageRoot.language)
            {
                Proficiency proficiency = new Proficiency(language.name, null, "language");
                if(!proficiencies.Where(p => p.Name == proficiency.Name).Any())
                {
                    proficiencies.Add(proficiency);
                }
            }

            //Tools and Instruments
            foreach (Item item in items)
            {
                if(item.Traits.Contains("Tool"))
                {
                    Proficiency proficiency = new Proficiency(item.Name, null, "tool");
                    if (!proficiencies.Where(p => p.Name == proficiency.Name).Any())
                    {
                        proficiencies.Add(proficiency);
                    }
                }
                else if (item.Traits.Contains("Instrument"))
                {
                    Proficiency proficiency = new Proficiency(item.Name, null, "instrument");
                    if (!proficiencies.Where(p => p.Name == proficiency.Name).Any())
                    {
                        proficiencies.Add(proficiency);
                    }
                }
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
        #endregion

        #region Conversion methods
        public static List<SpellRoot> GetSpellRoots()
        {
            List<SpellRoot> spellRoots = new List<SpellRoot>();
            foreach (string file in Directory.EnumerateFiles("Data\\5EToolsData\\spells", "*.json"))
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
            if(spell5E.school == "A")
            {
                spell.SpellSchool = "Abjuration";
            }
            else if (spell5E.school == "C")
            {
                spell.SpellSchool = "Conjuration";
            }
            else if(spell5E.school == "D")
            {
                spell.SpellSchool = "Divination";
            }
            else if(spell5E.school == "E")
            {
                spell.SpellSchool = "Enchantment";
            }
            else if(spell5E.school == "I")
            {
                spell.SpellSchool = "Illusion";
            }
            else if(spell5E.school == "N")
            {
                spell.SpellSchool = "Necromancy";
            }
            else if(spell5E.school == "T")
            {
                spell.SpellSchool = "Transmutation";
            }
            else if(spell5E.school == "V")
            {
                spell.SpellSchool = "Evocation";
            }
            spell.CastingTime = $"{spell5E.time[0].number} {spell5E.time[0].unit}";
            if(spell5E.range.distance != null)
            {
                spell.Range = "";
                if (spell5E.range.distance.amount > 0)
                {
                    spell.Range = spell5E.range.distance.amount.ToString();
                }
                spell.Range += $" {spell5E.range.distance.type}";
            }
            else
            {
                spell.Range = "self";
            }
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
            if(spell5E.duration[0].type == "instant")
            {
                spell.Duration = "instantaneous";
            }
            else
            {
                spell.Duration = $"{spell5E.duration[0].amount} {spell5E.duration[0].type}";
            }
            spell.RequiresConcentration = spell5E.duration[0].concentration ?? false;
            if(spell5E.meta != null)
            {
                spell.IsRitual = spell5E.meta.ritual ?? false;
            }
            else
            {
                spell.IsRitual = false;
            }
            spell.Description = "";
            foreach (object entry in spell5E.entries)
            {
                spell.Description += $"{entry} ";
            }
            spell.Description = CleanText(spell.Description);
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
                    item.Description += $"{entry} ";
                }
            }
            item.Description = CleanText(item.Description);
            item.Price = item5E.value / 100 ?? 0; 
            item.Weight = item5E.weight ?? 0;
            if (item5E.reqAttune != null && item5E.reqAttune.ToString().ToLower().Equals("true"))
            {
                item.Attuneable = true;
            }
            else
            {
                item.Attuneable = false;
            }

            if(item5E.wondrous)
            {
                item.Traits = "WI ";
            }
            else
            {
                item.Traits = $"{item5E.type} ";
            }
            if (item5E.property != null)
            {
                foreach (string trait in item5E.property)
                {
                    if(trait == "T")
                    {
                        item.Traits = "TH ";
                    }
                    else
                    {
                        item.Traits += $"{trait} ";
                    }
                }
            }
            item.Traits = DecodeTraits(item.Traits);
            item.Equipable = 0;
            if (item.Traits.Contains("Armor"))
            {
                item.Equipable = 1;
            }
            else if (item.Traits.Contains("Light"))
            {
                item.Equipable = 4;
            }
            else if (item.Traits.Contains("2 Handed"))
            {
                item.Equipable = 5;
            }
            else if (item.Traits.Contains("Weapon"))
            {
                item.Equipable = 2;
            }
            else if (item.Traits.Contains("Shield"))
            {
                item.Equipable = 3;
            }
            return item;
        }

        public static RaceRoot GetRaceRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\races.json");
            return JsonSerializer.Deserialize<RaceRoot>(contents);
        }
        public static Race ConvertRace(Race5ETools race5E, List<Proficiency> proficiencies)
        {
            Race race = new Race();
            race.Name = race5E.name;
            race.Sourcebook = race5E.source;
            race.Description = "";
            if (race5E.entries != null)
            {
                foreach (object entry in race5E.entries)
                {
                    race.Description += $"{entry} ";
                }
            }
            race.Description = CleanText(race.Description);
            race.StatBonuses = "";
            if (race5E.ability != null)
            {
                foreach (Ability ability in race5E.ability)
                {
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
            if(race.StatBonuses == "")
            {
                race.StatBonuses = "+2 +1, or 3 +1s to any stats of your choice.";
            }
            if (race5E.size != null)
            {
                race.Size = "";
                for (int i = 0; i < race5E.size.Count(); i++)
                {
                    if (race.Size.Length > 0)
                    {
                        race.Size += "or ";
                    }
                    if (race5E.size[i] == "S")
                    {
                        race.Size += "Small ";
                    }
                    else if (race5E.size[i] == "M")
                    {
                        race.Size += "Medium ";
                    }
                }
            }
            else
            {
                race.Size = "Medium";
            }
            race.Speed = 30;
            List<RaceProficiency> raceProficiencies = new List<RaceProficiency>();
            if (race5E.skillProficiencies != null)
            {
                foreach (SkillProficiency skill in race5E.skillProficiencies)
                {
                    List<Proficiency> skills = new List<Proficiency>();
                    skills.AddRange(FindSkills(skill, proficiencies));
                    foreach (Proficiency proficiency in skills)
                    {
                        if (!raceProficiencies.Where(p => p.Proficiency.Name == proficiency.Name).Any())
                        {
                            raceProficiencies.Add(new RaceProficiency(race, proficiency));
                        }
                    }
                }
            }
            if (race5E.languageProficiencies != null)
            {
                foreach (LanguageProficiency language in race5E.languageProficiencies)
                {
                    List<Proficiency> languages = new List<Proficiency>();
                    languages.AddRange(FindLanguages(language, proficiencies));
                    foreach (Proficiency proficiency in languages)
                    {
                        if(!raceProficiencies.Where(p => p.Proficiency.Name == proficiency.Name).Any())
                        {
                            raceProficiencies.Add(new RaceProficiency(race, proficiency));
                        }
                    }
                }
            }
            race.Proficiencies = raceProficiencies;
            //TODO: Features, Subraces, actually check speed
            return race;
        }

        public static BackgroundRoot GetBackgroundRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\backgrounds.json");
            return JsonSerializer.Deserialize<BackgroundRoot>(contents);
        }
        public static Background ConvertBackground(Background5ETools background5E, List<Proficiency> proficiencies)
        {
            Background background = new Background();
            background.Name = background5E.name;
            background.Sourcebook = background5E.source;
            background.Description = "";
            if (background5E.entries != null)
            {
                foreach (Entries entry in background5E.entries)
                {
                    if (entry.items != null)
                    {
                        foreach (EntryItem item in entry.items)
                        {
                            background.Description += $"{item.name} ";
                            if (item.entry != null)
                            {
                                background.Description += $"{item.entry} ";
                            }
                        }
                    }
                    if (entry.entries != null)
                    {
                        foreach (object subEntry in entry.entries)
                        {
                            background.Description += $"{subEntry} ";
                        }
                    }
                    background.Description += ".";
                }
            }
            background.Description = CleanText(background.Description);
            List<BackgroundProficiency> backgroundProficiencies = new List<BackgroundProficiency>();
            if (background5E.skillProficiencies != null)
            {

                foreach (SkillProficiency skill in background5E.skillProficiencies)
                {
                    List<Proficiency> skills = new List<Proficiency>();
                    skills.AddRange(FindSkills(skill, proficiencies));
                    foreach (Proficiency proficiency in skills)
                    {
                        if (!backgroundProficiencies.Where(p => p.Proficiency.Name == proficiency.Name).Any())
                        {
                            backgroundProficiencies.Add(new BackgroundProficiency(background, proficiency));
                        }
                    }
                }
            }
            if (background5E.toolProficiencies != null)
            {
                foreach (ToolProficiency tool in background5E.toolProficiencies)
                {
                    List<Proficiency> tools = new List<Proficiency>();
                    tools.AddRange(FindTools(tool, proficiencies));
                    foreach (Proficiency proficiency in tools)
                    {
                        if (!backgroundProficiencies.Where(p => p.Proficiency.Name == proficiency.Name).Any())
                        {
                            backgroundProficiencies.Add(new BackgroundProficiency(background, proficiency));
                        }
                    }
                }
            }
            if (background5E.languageProficiencies != null)
            {
                foreach (LanguageProficiency language in background5E.languageProficiencies)
                {
                    List<Proficiency> languages = new List<Proficiency>();
                    languages.AddRange(FindLanguages(language, proficiencies));
                    foreach (Proficiency proficiency in languages)
                    {
                        if (!backgroundProficiencies.Where(p => p.Proficiency.Name == proficiency.Name).Any())
                        {
                            backgroundProficiencies.Add(new BackgroundProficiency(background, proficiency));
                        }
                    }
                }
            }
            background.Proficiencies = backgroundProficiencies;
            //TODO:equipment, features
            return background;
        }

        public static List<ClassRoot> GetClassRoot()
        {
            List<ClassRoot> classRoots = new List<ClassRoot>();
            foreach (string file in Directory.EnumerateFiles("Data\\5EToolsData\\class", "*.json"))
            {
                string contents = File.ReadAllText(file);
                classRoots.Add(JsonSerializer.Deserialize<ClassRoot>(contents));
            }
            return classRoots;
        }
        public static Class ConvertClass(Class5ETools class5E, List<ClassFeature5ETools> classFeatures, List<Proficiency> proficiencies)
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
                if(spellcasting.SpellcastingType == "1/2" || spellcasting.SpellcastingType == "artificer")
                {
                    spellcasting.SpellcastingType = "half";
                }
                if(!class5E.preparedSpells.IsNullOrEmpty())
                {
                    spellcasting.PreparedCasting = true;
                }
                @class.Spellcasting = spellcasting;
            }
            foreach (ClassFeature5ETools feature5E in classFeatures)
            {
                string description = "";
                foreach (object entry in feature5E.entries)
                {
                    description += entry.ToString();
                }
                description = CleanText(description);
                ClassFeature feature = new ClassFeature(@class, feature5E.name, description, feature5E.level);
                feature.Source = $"{feature5E.source}, Class, {@class.Name}";
                @class.Features.Add(feature);
            }
            List<ClassProficiency> classProficiencies = new List<ClassProficiency>();
            if (class5E.startingProficiencies.skills != null)
            {

                foreach (SkillProficiency skill in class5E.startingProficiencies.skills)
                {
                    List<Proficiency> skills = new List<Proficiency>();
                    skills.AddRange(FindSkills(skill, proficiencies));
                    if (skill.choose != null)
                    {
                        foreach (string str in skill.choose.from)
                        {
                            Proficiency? proficiency = FindProficiency(str, proficiencies);
                            if (proficiency != null)
                            {
                                skills.Add(proficiency);
                            }
                        }
                    }
                    foreach (Proficiency proficiency in skills)
                    {
                        if (!classProficiencies.Where(p => p.Proficiency.Name == proficiency.Name).Any())
                        {
                            classProficiencies.Add(new ClassProficiency(@class, proficiency));
                        }
                    }
                }
            }
            if (class5E.startingProficiencies.toolProficiencies != null)
            {
                foreach (ToolProficiency tool in class5E.startingProficiencies.toolProficiencies)
                {
                    List<Proficiency> tools = new List<Proficiency>();
                    tools.AddRange(FindTools(tool, proficiencies));
                    if (tool.choose != null)
                    {
                        foreach (string str in tool.choose.from)
                        {
                            Proficiency? proficiency = FindProficiency(str, proficiencies);
                            if (proficiency != null)
                            {
                                tools.Add(proficiency);
                            }
                        }
                    }
                    foreach (Proficiency proficiency in tools)
                    {
                        if (!classProficiencies.Where(p => p.Proficiency.Name == proficiency.Name).Any())
                        {
                            classProficiencies.Add(new ClassProficiency(@class, proficiency));
                        }
                    }
                }
            }
            foreach(string proficiencyname in class5E.proficiency)
            {
                Proficiency? proficiency = FindProficiency(proficiencyname, proficiencies);
                if(proficiency != null)
                {
                    classProficiencies.Add(new ClassProficiency(@class, proficiency));
                }
            }
            @class.Proficiencies = classProficiencies;
            return @class;
        }
        public static Subclass ConvertSubclass(Subclass5ETools subclass5E, Class @class, List<SubclassFeature5ETools> subclassFeatures)
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
            foreach (SubclassFeature5ETools feature5E in subclassFeatures)
            {
                string description = "";
                foreach (object entry in feature5E.entries)
                {
                    description += entry.ToString();
                }
                description = CleanText(description);
                SubclassFeature feature = new SubclassFeature(subclass, feature5E.name, description, feature5E.level);
                subclass.Features.Add(feature);
            }
            @class.Subclasses.Add(subclass);
            return subclass;
        }
        #endregion

        #region Helper methods
        public static string CleanText(string text)
        {
            if(text.IsNullOrEmpty())
            {
                return "";
            }
            text = text.Replace("}", "");
            text = text.Replace("{", "");
            text = text.Replace("[", "");
            text = text.Replace("]", "");
            string[] textList = text.Split(" ");
            string cleanText = "";
            foreach (string word in textList)
            {
                if(word.Contains("|"))
                {
                    cleanText += word.Substring(0, word.IndexOf("|") - 1);
                }
                else if (!word.Contains("@"))
                {
                    cleanText += $"{word} ";
                }
            }
            return cleanText;
        }
        public static List<Proficiency> FindLanguages(LanguageProficiency language, List<Proficiency> proficiencies)
        {
            List<Proficiency> languages = new List<Proficiency>();
            if (language.auran)
            {
                languages.Add(FindProficiency("auran", proficiencies));
            }
            if(language.aquan != null && (bool)language.aquan)
            {
                languages.Add(FindProficiency("aquan", proficiencies));
            }
            if (language.common != null && (bool)language.common)
            {
                languages.Add(FindProficiency("common", proficiencies));
            }
            if (language.dwarvish != null && (bool)language.dwarvish)
            {
                languages.Add(FindProficiency("dwarvish", proficiencies));
            }
            if (language.elvish != null && (bool)language.elvish)
            {
                languages.Add(FindProficiency("elvish", proficiencies));
            }
            if (language.draconic != null && (bool)language.draconic)
            {
                languages.Add(FindProficiency("draconic", proficiencies));
            }
            if (language.celestial != null && (bool)language.celestial)
            {
                languages.Add(FindProficiency("celestial", proficiencies));
            }
            if (language.primordial != null && (bool)language.primordial)
            {
                languages.Add(FindProficiency("primordial", proficiencies));
            }
            if (language.thievescant != null && (bool)language.thievescant)
            {
                languages.Add(FindProficiency("thieves' cant", proficiencies));
            }
            if (language.undercommon != null && (bool)language.undercommon)
            {
                languages.Add(FindProficiency("undercommon", proficiencies));
            }
            if (language.giant != null && (bool)language.giant)
            {
                languages.Add(FindProficiency("giant", proficiencies));
            }
            if (language.goblin != null && (bool)language.goblin)
            {
                languages.Add(FindProficiency("goblin", proficiencies));
            }
            if (language.sylvan != null && (bool)language.sylvan)
            {
                languages.Add(FindProficiency("sylvan", proficiencies));
            }
            if (language.gnomish != null && (bool)language.gnomish)
            {
                languages.Add(FindProficiency("gnomish", proficiencies));
            }
            return proficiencies;
        }
        public static List<Proficiency> FindSkills(SkillProficiency skill, List<Proficiency> proficiencies)
        {
            List<Proficiency> skills = new List<Proficiency>();

            if (skill.intimidation)
            {
                skills.Add(FindProficiency("Intimidation", proficiencies));
            }
            if (skill.insight)
            {
                skills.Add(FindProficiency("Insight", proficiencies));
            }
            if (skill.religion)
            {
                skills.Add(FindProficiency("Religion", proficiencies));
            }
            if (skill.perception.HasValue && skill.perception.Value)
            {
                skills.Add(FindProficiency("Perception", proficiencies));
            }
            if (skill.stealth.HasValue && skill.stealth.Value)
            {
                skills.Add(FindProficiency("Stealth", proficiencies));
            }
            if (skill.survival.HasValue && skill.survival.Value)
            {
                skills.Add(FindProficiency("Survival", proficiencies));
            }
            if (skill.deception.HasValue && skill.deception.Value)
            {
                skills.Add(FindProficiency("Deception", proficiencies));
            }
            if (skill.history.HasValue && skill.history.Value)
            {
                skills.Add(FindProficiency("History", proficiencies));
            }
            if (skill.nature.HasValue && skill.nature.Value)
            {
                skills.Add(FindProficiency("Nature", proficiencies));
            }
            if (skill.acrobatics.HasValue && skill.acrobatics.Value)
            {
                skills.Add(FindProficiency("Acrobatics", proficiencies));
            }
            if (skill.athletics.HasValue && skill.athletics.Value)
            {
                skills.Add(FindProficiency("Athletics", proficiencies));
            }
            if (skill.animalhandling.HasValue && skill.animalhandling.Value)
            {
                skills.Add(FindProficiency("Animal Handling", proficiencies));
            }
            if (skill.performance.HasValue && skill.performance.Value)
            {
                skills.Add(FindProficiency("Performance", proficiencies));
            }
            if (skill.sleightofhand.HasValue && skill.sleightofhand.Value)
            {
                skills.Add(FindProficiency("Sleight of Hand", proficiencies));
            }
            if (skill.persuasion.HasValue && skill.persuasion.Value)
            {
                skills.Add(FindProficiency("Persuasion", proficiencies));
            }
            if (skill.investigation.HasValue && skill.investigation.Value)
            {
                skills.Add(FindProficiency("Investigation", proficiencies));
            }

            return skills;
        }
        public static List<Proficiency> FindTools(ToolProficiency tool, List<Proficiency> proficiencies)
        {
            List<Proficiency> tools = new List<Proficiency>();
            if(tool.herbalismkit.HasValue &&  tool.herbalismkit.Value)
            {
                tools.Add(FindProficiency("herbalism kit", proficiencies));
            }
            if (tool.disguisekit.HasValue && tool.disguisekit.Value)
            {
                tools.Add(FindProficiency("disguise kit", proficiencies));
            }
            if (tool.forgerykit.HasValue && tool.forgerykit.Value)
            {
                tools.Add(FindProficiency("forgery kit", proficiencies));
            }
            if (tool.tinkerstools)
            {
                tools.Add(FindProficiency("tinker's tools", proficiencies));
            }
            if (tool.thievestools.HasValue && tool.thievestools.Value)
            {
                tools.Add(FindProficiency("thieves' tools", proficiencies));
            }
            return tools;
        }
        public static Proficiency? FindProficiency(string proficiencyName, List<Proficiency> proficiencies)
        {
            return proficiencies.Where(p => p.Name.ToLower().Contains(proficiencyName.Trim().ToLower())).FirstOrDefault();
        }
        public static string DecodeTraits(string traits)
        {
            string allTraits = "";
            if (traits != null)
            {
                string[] traitList = traits.Trim().Split(" ");
                Dictionary<string, string> dict = new Dictionary<string, string>
            {
                {"A", "Ammunition" },
                {"G", "Adventuring Gear" },
                {"AT", "Artisan's Tools" },
                {"ER", "Extended Reach" },
                {"EXP", "Explosive" },
                {"H", "Heavy" },
                {"HA", "Heavy Armor" },
                {"2H", "2 Handed" },
                {"GS", "Gaming Set" },
                {"IDG", "Illegal Drug" },
                { "INS", "Instrument" },
                { "L", "Light" },
                { "LA", "Light Armor" },
                { "LD", "Loading" },
                {"MA", "Medium Armor" },
                { "M", "Melee Weapon" },
                {"R", "Ranged Weapon" },
                {"S", "Shield" },
                {"SC", "Sroll" },
                {"SCF", "Spellcasting Focus" },
                { "RD", "Rod" },
                { "RG", "Ring" },
                { "P", "Potion" },
                { "$", "Treasure" },
                { "FD", "Food and Drink" },
                { "F", "Finesse" },
                {"T", "Tool" },
                {"TH", "Thrown" },
                {"TAH", "Tack and Harness" },
                {"OTH", "Other" },
                {"TG", "Trade Good" },
                {"V", "Versatile" },
                {"Vst", "Vestige" },
                {"SPC", "Vehicle (space)" },
                {"SHC", "Vehicle (water)" },
                {"AIR", "Vehicle (air)" },
                {"VEH", "Vehicle (land)" },
                {"MNT", "Mount" },
                {"WD", "Wand" },
                {"WI", "Wondrous Item" }
            };
                if (traitList.Length > 0)
                {
                    if (dict.ContainsKey(traitList[0]))
                    {
                        traitList[0] = dict[traitList[0]];
                    }
                }
                for (int i = 1; i < traitList.Length; i++)
                {
                    if (dict.ContainsKey(traitList[i]))
                    {
                        traitList[i] = $", {dict[traitList[i]]}";
                    }
                }
                foreach (string trait in traitList)
                { allTraits += trait; }
            }
            return allTraits;
        }
        #endregion
    }
}
