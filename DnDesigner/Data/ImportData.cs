using DnDesigner.Models.ImportModels;
using System.Text.Json;
using DnDesigner.Models;
using Microsoft.IdentityModel.Tokens;
using System;

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
                                .Where(f => f.className == class5E.name && !f.source.Contains("UA")).ToList();
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
                            List<SubclassFeature5ETools> subclassFeatures = classRoot.subclassFeature
                                .Where(f => f.subclassShortName == subclass5E.shortName && !f.source.Contains("UA"))
                                .ToList();
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
        /// Extracts Feat Data from the 5ETools JSON files and returns it as a list of Feats
        /// </summary>
        public static List<Feat> ExtractFeats()
        {
            FeatRoot featRoot = GetFeatRoot();
            List<Feat> feats = new List<Feat>();
            foreach (Feat5ETools feat5E in featRoot.feat)
            {
                if(!feat5E.source.Contains("UA"))
                {
                    feats.Add(ConvertFeat(feat5E));
                }
            }
            return feats;
        }

        /// <summary>
        /// Extracts Item Data from the 5ETools JSON files and returns it as a list of Items
        /// </summary>
        public static List<Item> ExtractItems()
        {
            ItemRoot itemRoot = GetItemRoot();
            BaseItemRoot baseItemRoot = GetBaseItemRoot();
            List<Item> items = new List<Item>();
            foreach (Item5ETools item5E in itemRoot.item)
            {
                if(!item5E.source.Contains("UA"))
                {
                    items.Add(ConvertItem(item5E));
                }
            }
            foreach (Item5ETools item5E in baseItemRoot.baseitem)
            {
                if (!item5E.source.Contains("UA"))
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
            List<Proficiency> proficiencies = new List<Proficiency>
            {
                //Saving Throws
                new Proficiency("Strength Saves", "Strength", "saving throw"),
                new Proficiency("Dexterity Saves", "Dexterity", "saving throw"),
                new Proficiency("Constitution Saves", "Constitution", "saving throw"),
                new Proficiency("Intelligence Saves", "Intelligence", "saving throw"),
                new Proficiency("Wisdom Saves", "Wisdom", "saving throw"),
                new Proficiency("Charisma Saves", "Charisma", "saving throw"),

                //Skills
                new Proficiency("Acrobatics", "Dexterity", "skill"),
                new Proficiency("Animal Handling", "Wisdom", "skill"),
                new Proficiency("Arcana", "Intelligence", "skill"),
                new Proficiency("Athletics", "Strength", "skill"),
                new Proficiency("Deception", "Charisma", "skill"),
                new Proficiency("History", "Intelligence", "skill"),
                new Proficiency("Insight", "Wisdom", "skill"),
                new Proficiency("Intimidation", "Charisma", "skill"),
                new Proficiency("Investigation", "Intelligence", "skill"),
                new Proficiency("Medicine", "Wisdom", "skill"),
                new Proficiency("Nature", "Intelligence", "skill"),
                new Proficiency("Perception", "Wisdom", "skill"),
                new Proficiency("Performance", "Charisma", "skill"),
                new Proficiency("Persuasion", "Charisma", "skill"),
                new Proficiency("Religion", "Intelligence", "skill"),
                new Proficiency("Sleight of Hand", "Dexterity", "skill"),
                new Proficiency("Stealth", "Dexterity", "skill"),
                new Proficiency("Survival", "Wisdom", "skill"),

                //Weapons and armor
                new Proficiency("Light Armor", null, "armor"),
                new Proficiency("Medium Armor", null, "armor"),
                new Proficiency("Heavy Armor", null, "armor"),
                new Proficiency("Shield", null, "armor"),
                new Proficiency("Simple Weapons", null, "weapon"),
                new Proficiency("Martial Weapons", null, "weapon")
            };

            //Weapons, Tools and Instruments
            foreach (Item item in items.Where(i => i.Rarity == 0))
            {
                if (item.Traits.Contains("Tool"))
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
                else if (item.Traits.Contains("Weapon") && item.Sourcebook == "PHB")
                {
                    Proficiency proficiency = new Proficiency(item.Name, null, "weapon");
                    if (!proficiencies.Where(p => p.Name == proficiency.Name).Any())
                    {
                        proficiencies.Add(proficiency);
                    }
                }
            }

            //Languages
            string contents = File.ReadAllText("Data\\5EToolsData\\languages.json");
            LanguageRoot languageRoot = JsonSerializer.Deserialize<LanguageRoot>(contents);
            foreach (Language language in languageRoot.language)
            {
                Proficiency proficiency = new Proficiency(language.name, null, "language");
                if (!proficiencies.Where(p => p.Name == proficiency.Name).Any())
                {
                    proficiencies.Add(proficiency);
                }
            }

            return proficiencies;
        }
        #endregion

        #region Conversion methods
        public static FeatRoot GetFeatRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\feats.json");
            return JsonSerializer.Deserialize<FeatRoot>(contents);
        }

        public static Feat ConvertFeat(Feat5ETools feat5E)
        {
            string name = feat5E.name;
            string source = feat5E.source;
            string description = "";
            if (feat5E.entries != null)
            {
                foreach (object entry in feat5E.entries)
                {
                    description += $"{entry} ";
                }
            }
            description = CleanText(description);
            int level = 0;
            Feat feat = new Feat(name, description, level, source, false, "");
            return feat;
        }

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
        public static BaseItemRoot GetBaseItemRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\items-base.json");
            return JsonSerializer.Deserialize<BaseItemRoot>(contents);
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

            // Set rarity
            if(item5E.rarity == "none")
            {
                item.Rarity = 0;
            } 
            else if (item5E.rarity == "common")
            {
                item.Rarity = 1;
            }
            else if (item5E.rarity == "uncommon")
            {
                item.Rarity = 2;
            }
            else if (item5E.rarity == "rare")
            {
                item.Rarity = 3;
            }
            else if (item5E.rarity == "very rare")
            {
                item.Rarity = 4;
            }
            else if (item5E.rarity == "legendary")
            {
                item.Rarity = 5;
            }
            else if (item5E.rarity == "artifact")
            {
                item.Rarity = 6;
            }
            else
            {
                item.Rarity = -1;
            }

            // Set attunement
            if (item5E.reqAttune != null && item5E.reqAttune.ToString().ToLower().Equals("true"))
            {
                item.Attuneable = true;
            }
            else
            {
                item.Attuneable = false;
            }

            // Set traits
            if(item5E.weaponCategory == "simple")
            {
                item.Traits = "Simple ";
            }
            else if(item5E.weaponCategory == "martial")
            {
                item.Traits = "Martial ";
            }
            if(item5E.wondrous)
            {
                item.Traits += "WI ";
            }
            else
            {
                item.Traits += $"{item5E.type} ";
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
            item.Traits = ParseTraits(item.Traits);

            // Set equipable
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

            // Set effects
            if(item5E.dmg1 != null)
            {
                Models.Action action = new Models.Action();
                action.Name = item.Name;
                action.Description = item.Description;
                action.ActionTime = "1 action";
                action.Range = (item5E.range ?? "5") + " feet";
                action.DamageType = ParseDamageType(item5E.dmgType);
                if(item.Traits.Contains("Finesse") || item.Traits.Contains("Ranged"))
                {
                    action.AttackBonusCalculation = "proficiency + dexteritymod ";
                    action.Damage = item5E.dmg1 + " + dexterity ";
                }
                else
                {
                    action.AttackBonusCalculation = $"proficiency + strengthmod ";
                    action.Damage = item5E.dmg1 + " + strength ";
                }
                if(!item5E.bonusWeapon.IsNullOrEmpty())
                {
                    action.AttackBonusCalculation += "+ " + item5E.bonusWeapon;
                    action.Damage += "+ " + item5E.bonusWeapon;
                }
                item.Effects.Add(new GrantAction(action));
            }
            if (item5E.ac != null)
            {
                int ac = 0;
                if (item5E.bonusAc != null && int.TryParse(item5E.bonusAc, out int bonus))
                {
                    ac = item5E.ac.Value + bonus;
                }
                else
                {
                    ac = item5E.ac.Value;
                }
                string acString = ac.ToString();
                if (item.Traits.Contains("Light Armor"))
                {
                    acString += " + dexteritymod";
                }
                else if (item.Traits.Contains("Medium Armor"))
                {
                    acString += " + dexteritymod(2)";
                }
                if (item.Traits.Contains("Shield"))
                {
                    item.Effects.Add(new ModifyArmorClass(item5E.ac.Value));
                }
                else
                {
                    item.Effects.Add(new SetArmorClass(acString, true, false, false));
                }
            }
            else if (item5E.bonusAc != null)
            {
                if (int.TryParse(item5E.bonusAc, out int ac))
                {
                    item.Effects.Add(new ModifyArmorClass(ac));
                }
            }
            return item;
        }

        public static RaceRoot GetRaceRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\races.json");
            return JsonSerializer.Deserialize<RaceRoot>(contents);
        }
        public static Race ConvertRace(Race5ETools race5E, List<Proficiency> allProficiencies)
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
            race.Description = race.Description.Replace("\"", "");
            RaceFeature statBonuses = new RaceFeature(race, "Racial Stat Boosts", "", 0);
            race.StatBonuses = "";
            if (race5E.ability != null)
            {
                foreach (Ability ability in race5E.ability)
                {
                    if (ability.cha != null)
                    {
                        race.StatBonuses += $"+{ability.cha} Charisma. ";
                        ModifyAttribute statBonus = new ModifyAttribute("cha", ability.cha.Value);
                        statBonuses.Effects.Add(statBonus);
                    }
                    if (ability.con != null)
                    {
                        race.StatBonuses += $"+{ability.con} Constitution. ";
                        ModifyAttribute statBonus = new ModifyAttribute("con", ability.con.Value);
                        statBonuses.Effects.Add(statBonus);
                    }
                    if (ability.dex != null)
                    {
                        race.StatBonuses += $"+{ability.dex} Dexterity. ";
                        ModifyAttribute statBonus = new ModifyAttribute("dex", ability.dex.Value);
                        statBonuses.Effects.Add(statBonus);
                    }
                    if (ability.@int != null)
                    {
                        race.StatBonuses += $"+{ability.@int} Intelligence. ";
                        ModifyAttribute statBonus = new ModifyAttribute("int", ability.@int.Value);
                        statBonuses.Effects.Add(statBonus);
                    }
                    if (ability.str != null)
                    {
                        race.StatBonuses += $"+{ability.str} Strength. ";
                        ModifyAttribute statBonus = new ModifyAttribute("str", ability.str.Value);
                        statBonuses.Effects.Add(statBonus);
                    }
                    if (ability.wis != null)
                    {
                        race.StatBonuses += $"+{ability.wis} Wisdom. ";
                        ModifyAttribute statBonus = new ModifyAttribute("wis", ability.wis.Value);
                        statBonuses.Effects.Add(statBonus);
                    }
                }
            }
            if(race.StatBonuses == "")
            {
                race.StatBonuses = "+2 +1, or 3 +1s to different stats.";
                statBonuses.Choices.Add(new EffectChoice("ASI"));
                statBonuses.Choices.Add(new EffectChoice("ASI"));
                statBonuses.Choices.Add(new EffectChoice("ASI"));
            }
            statBonuses.Description = race.StatBonuses;
            race.Features.Add(statBonuses);
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
            List<Proficiency> raceProficiencies = new List<Proficiency>();
            if (race5E.skillProficiencies != null)
            {
                foreach (SkillProficiency skill in race5E.skillProficiencies)
                {
                    List<Proficiency> skills = FindSkills(skill, allProficiencies);
                    foreach (Proficiency proficiency in skills)
                    {
                        if (!raceProficiencies.Where(p => p.Name == proficiency.Name).Any())
                        {
                            raceProficiencies.Add(proficiency);
                        }
                    }
                }
            }
            if (race5E.languageProficiencies != null)
            {
                foreach (LanguageProficiency language in race5E.languageProficiencies)
                {
                    List<Proficiency> languages = FindLanguages(language, allProficiencies);
                    foreach (Proficiency proficiency in languages)
                    {
                        if (!raceProficiencies.Where(p => p.Name == proficiency.Name).Any())
                        {
                            raceProficiencies.Add(proficiency);
                        }
                    }
                }
            }
            if (raceProficiencies.Any())
            {
                RaceFeature proficiencies = new RaceFeature(race, "Racial Proficiencies", "You gain proficiency in ", 0);
                foreach (Proficiency proficiency in raceProficiencies)
                {
                    proficiencies.Description += $"{proficiency.Name}, ";
                }
                proficiencies.Description = proficiencies.Description.Substring(0, proficiencies.Description.Length - 2);
                proficiencies.Effects.Add(new GrantProficiencies(raceProficiencies, false));
                race.Features.Add(proficiencies);
            }
            //TODO: Features, Subraces, actually check speed
            return race;
        }

        public static BackgroundRoot GetBackgroundRoot()
        {
            string contents = File.ReadAllText("Data\\5EToolsData\\backgrounds.json");
            return JsonSerializer.Deserialize<BackgroundRoot>(contents);
        }
        public static Background ConvertBackground(Background5ETools background5E, List<Proficiency> allProficiencies)
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
                                background.Description += $"{item.entry}. ";
                            }
                        }
                    }
                    if (entry.entries != null)
                    {
                        foreach (object subEntry in entry.entries)
                        {
                            background.Description += $"{subEntry} ";
                        }
                        background.Description += ". ";
                    }
                }
            }
            background.Description = CleanText(background.Description);
            List<Proficiency> backgroundProficiencies = new List<Proficiency>();
            if (background5E.skillProficiencies != null)
            {

                foreach (SkillProficiency skill in background5E.skillProficiencies)
                {
                    List<Proficiency> skills = FindSkills(skill, allProficiencies);
                    foreach (Proficiency proficiency in skills)
                    {
                        if (!backgroundProficiencies.Where(p => p.Name == proficiency.Name).Any())
                        {
                            backgroundProficiencies.Add(proficiency);
                        }
                    }
                }
            }
            if (background5E.toolProficiencies != null)
            {
                foreach (ToolProficiency tool in background5E.toolProficiencies)
                {
                    List<Proficiency> tools = FindTools(tool, allProficiencies);
                    foreach (Proficiency proficiency in tools)
                    {
                        if (!backgroundProficiencies.Where(p => p.Name == proficiency.Name).Any())
                        {
                            backgroundProficiencies.Add(proficiency);
                        }
                    }
                }
            }
            if (background5E.languageProficiencies != null)
            {
                foreach (LanguageProficiency language in background5E.languageProficiencies)
                {
                    List<Proficiency> languages = FindLanguages(language, allProficiencies);
                    foreach (Proficiency proficiency in languages)
                    {
                        if (!backgroundProficiencies.Where(p => p.Name == proficiency.Name).Any())
                        {
                            backgroundProficiencies.Add(proficiency);
                        }
                    }
                }
            }
            if (backgroundProficiencies.Any())
            {
                BackgroundFeature proficiencies = new BackgroundFeature(background, "Background Proficiencies", "You gain proficiency in ");
                foreach (Proficiency proficiency in backgroundProficiencies)
                {
                    proficiencies.Description += $"{proficiency.Name}, ";
                }
                proficiencies.Description = proficiencies.Description.Substring(0, proficiencies.Description.Length - 2);
                background.Features.Add(proficiencies);
                proficiencies.Effects.Add(new GrantProficiencies(backgroundProficiencies, false));
            }
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
        public static Class ConvertClass(Class5ETools class5E, List<ClassFeature5ETools> classFeatures, List<Proficiency> allProficiencies)
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

            ClassFeature startingProficiencies = new ClassFeature(@class, "Starting Proficiencies", "", 1);
            List<Proficiency> classProficiencies = new List<Proficiency>();
            startingProficiencies.Description = "When gaining this class at first level, you gain proficiency in the following. ";
            if(class5E.startingProficiencies.armor != null)
            {
                startingProficiencies.Description += "Armor: ";
                foreach (object armor in class5E.startingProficiencies.armor)
                {
                    Proficiency? proficiency = FindProficiency(CleanText(armor.ToString()), allProficiencies);
                    if(proficiency != null && !classProficiencies.Where(p => p.Name == proficiency.Name).Any())
                    {
                        classProficiencies.Add(proficiency);
                        startingProficiencies.Description += $"{proficiency.Name}, ";
                    }
                }
                startingProficiencies.Description = startingProficiencies.Description
                        .Substring(0, startingProficiencies.Description.Length - 2) + ". ";
            }

            if (class5E.startingProficiencies.weapons != null)
            {
                startingProficiencies.Description += "Weapons: ";
                foreach (object weapon in class5E.startingProficiencies.weapons)
                {
                    Proficiency? proficiency = FindProficiency(CleanText(weapon.ToString()), allProficiencies);
                    if (proficiency != null && !classProficiencies.Where(p => p.Name == proficiency.Name).Any())
                    {
                        classProficiencies.Add(proficiency);
                        startingProficiencies.Description += $"{proficiency.Name}, ";
                    }
                }
                startingProficiencies.Description = startingProficiencies.Description
                            .Substring(0, startingProficiencies.Description.Length - 2) + ". ";
            }

            if (class5E.startingProficiencies.tools != null)
            {
                startingProficiencies.Description += "Tools: ";
                foreach (object tool in class5E.startingProficiencies.tools)
                {
                    Proficiency? proficiency = FindProficiency(CleanText(tool.ToString()), allProficiencies);
                    if (proficiency != null && !classProficiencies.Where(p => p.Name == proficiency.Name).Any())
                    {
                        classProficiencies.Add(proficiency);
                        startingProficiencies.Description += $"{proficiency.Name}, ";
                    }
                }
                startingProficiencies.Description = startingProficiencies.Description
                            .Substring(0, startingProficiencies.Description.Length - 2) + ". ";
            }

            startingProficiencies.Description += "Saving throws: ";
            foreach (string proficiencyname in class5E.proficiency)
            {
                Proficiency? proficiency = FindProficiency(proficiencyname, allProficiencies);
                if (proficiency != null && !classProficiencies.Where(p => p.Name == proficiency.Name).Any())
                {
                    classProficiencies.Add(proficiency);
                    startingProficiencies.Description += $"{proficiency.Name}, ";
                }
            }
            startingProficiencies.Description = startingProficiencies.Description
                .Substring(0, startingProficiencies.Description.Length - 2) + ". ";

            if (class5E.startingProficiencies.skills != null)
            {
                foreach (SkillProficiency skill in class5E.startingProficiencies.skills)
                {
                    if (skill.choose != null)
                    {
                        startingProficiencies.Description += $"Skills: Choose {skill.choose.count} from ";
                        List<Proficiency> skills = FindSkills(skill, allProficiencies);
                        for (int i = 0; i < skill.choose.count; i++)
                        {
                            EffectChoice choice = new EffectChoice(skills);
                            startingProficiencies.Choices.Add(choice);
                        }
                        
                        foreach (Proficiency proficiency in skills)
                        {
                            startingProficiencies.Description += $"{proficiency.Name}, ";
                        }
                        
                    }
                }
                startingProficiencies.Description = startingProficiencies.Description
                            .Substring(0, startingProficiencies.Description.Length - 2) + ". ";
            }

            startingProficiencies.Effects.Add(new GrantProficiencies(classProficiencies, false));
            @class.Features.Add(startingProficiencies);
            foreach (ClassFeature5ETools feature5E in classFeatures)
            {
                string description = "";
                foreach (object entry in feature5E.entries)
                {
                    description += entry.ToString() + " ";
                }
                description = CleanText(description);
                ClassFeature feature = new ClassFeature(@class, feature5E.name, description, feature5E.level);
                feature.Source = $"{feature5E.source}, Class, {@class.Name}";
                if (feature.Name == "Ability Score Improvement")
                {
                    feature.Choices.Add(new EffectChoice("ASI"));
                    feature.Choices.Add(new EffectChoice("ASI"));
                }
                @class.Features.Add(feature);
            }
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
                    description += entry.ToString() + " ";
                }
                description = CleanText(description);
                SubclassFeature feature = new SubclassFeature(subclass, feature5E.name, description, feature5E.level);
                subclass.Features.Add(feature);
                if(@class.SubclassLevel == 0 || feature.Level <= @class.SubclassLevel)
                {
                    @class.SubclassLevel = feature.Level;
                }
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
            string[] textList = text.Split(" ");
            string cleanText = "";
            foreach (string word in textList)
            {
                string mutableword = word;
                if (mutableword.IndexOf("\"") != mutableword.LastIndexOf("\""))
                {
                    mutableword = string.Concat(mutableword.AsSpan(0, mutableword.IndexOf("\"")), " ", mutableword.AsSpan(mutableword.LastIndexOf("\"") + 1));
                }
                if (mutableword.Contains("|"))
                {
                    mutableword = mutableword.Substring(0, mutableword.IndexOf("|"));
                }
                if (!mutableword.Contains("@"))
                {
                    cleanText += $"{mutableword} ";
                }
            }
            string[] bannedStrings = { "{", "}", "[", "]", " . ", " : ", "col-1", "col-2", "text-center", "\u2014", "\n", "\r", "\\" };
            foreach (string bannedString in bannedStrings)
            {
                cleanText = cleanText.Replace(bannedString, "");
            }
            return cleanText;
        }
        public static List<Proficiency> FindLanguages(LanguageProficiency language, List<Proficiency> proficiencies)
        {
            List<Proficiency> languages = new List<Proficiency>();
            if (language.auran.HasValue && language.auran.Value)
            {
                languages.Add(FindProficiency("primordial", proficiencies));
            }
            if(language.aquan.HasValue && language.aquan.Value)
            {
                languages.Add(FindProficiency("primordial", proficiencies));
            }
            if (language.common.HasValue && language.common.Value)
            {
                languages.Add(FindProficiency("common", proficiencies));
            }
            if (language.dwarvish.HasValue && language.dwarvish.Value)
            {
                languages.Add(FindProficiency("dwarvish", proficiencies));
            }
            if (language.elvish.HasValue && language.elvish.Value)
            {
                languages.Add(FindProficiency("elvish", proficiencies));
            }
            if (language.draconic.HasValue && language.draconic.Value)
            {
                languages.Add(FindProficiency("draconic", proficiencies));
            }
            if (language.celestial.HasValue && language.celestial.Value)
            {
                languages.Add(FindProficiency("celestial", proficiencies));
            }
            if (language.primordial.HasValue && language.primordial.Value)
            {
                languages.Add(FindProficiency("primordial", proficiencies));
            }
            if (language.thievescant.HasValue && language.thievescant.Value)
            {
                languages.Add(FindProficiency("thieves' cant", proficiencies));
            }
            if (language.undercommon.HasValue && language.undercommon.Value)
            {
                languages.Add(FindProficiency("undercommon", proficiencies));
            }
            if (language.giant.HasValue && language.giant.Value)
            {
                languages.Add(FindProficiency("giant", proficiencies));
            }
            if (language.goblin.HasValue && language.goblin.Value)
            {
                languages.Add(FindProficiency("goblin", proficiencies));
            }
            if (language.sylvan.HasValue && language.sylvan.Value)
            {
                languages.Add(FindProficiency("sylvan", proficiencies));
            }
            if (language.gnomish.HasValue && language.gnomish.Value)
            {
                languages.Add(FindProficiency("gnomish", proficiencies));
            }
            return languages;
        }
        public static List<Proficiency> FindSkills(SkillProficiency skill, List<Proficiency> proficiencies)
        {
            List<Proficiency> skills = new List<Proficiency>();

            if (skill.intimidation.HasValue && skill.intimidation.Value)
            {
                skills.Add(FindProficiency("Intimidation", proficiencies));
            }
            if (skill.insight.HasValue && skill.insight.Value)
            {
                skills.Add(FindProficiency("Insight", proficiencies));
            }
            if (skill.religion.HasValue && skill.religion.Value)
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
            if (skill.arcana.HasValue && skill.arcana.Value)
            {
                skills.Add(FindProficiency("Arcana", proficiencies));
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
            return skills;
        }
        public static List<Proficiency> FindTools(ToolProficiency tool, List<Proficiency> proficiencies)
        {
            List<Proficiency> tools = new List<Proficiency>();
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
            if (tool.herbalismkit.HasValue &&  tool.herbalismkit.Value)
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
            if (tool.alchemistssupplies.HasValue && tool.alchemistssupplies.Value)
            {
                tools.Add(FindProficiency("alchemist's supplies", proficiencies));
            }
            if (tool.cooksutensils.HasValue && tool.cooksutensils.Value)
            {
                tools.Add(FindProficiency("cook's utensils", proficiencies));
            }

            return tools;
        }
        public static Proficiency? FindProficiency(string proficiencyName, List<Proficiency> proficiencies)
        {
            return proficiencies.Where(p => p.Name.ToLower().Contains(proficiencyName.Trim().ToLower())).FirstOrDefault();
        }
        public static string ParseTraits(string traits)
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
                {"WI", "Wondrous Item" },
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
        public static string ParseDamageType(string damageType)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                {"A", "Acid" },
                {"B", "Bludgeoning" },
                {"C", "Cold" },
                {"F", "Fire" },
                {"O", "Force" },
                {"L", "Lightning" },
                {"N", "Necrotic" },
                {"P", "Piercing" },
                {"PS", "Psychic" },
                {"R", "Radiant" },
                {"S", "Slashing" },
                {"T", "Thunder" }
            };
            return dict[damageType];
        }
        #endregion
    }
}
