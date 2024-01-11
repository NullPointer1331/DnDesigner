using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
    /// <summary>
    /// The information of the character
    /// </summary>
    public class Character
    {
        #region properties
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int CharacterId { get; set; }

        /// The Characters name
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The characters current level
        /// </summary>
        public int Level { get
            {
                int level = 0;
                foreach (CharacterClass characterClass in Classes)
                {
                    level += characterClass.Level;
                }
                return level;
            }}

        /// <summary>
        /// The characters current proficiency bonus
        /// </summary>
        public int ProficiencyBonus { get
            {
                return (Level - 1) / 4 + 2;
            }}

        /// <summary>
        /// The characters maximum health points
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// The characters current health points
        /// </summary>
        public int CurrentHealth { get; set; }

        /// <summary>
        /// The characters temporary health points
        /// </summary>
        public int TempHealth { get; set; }

        /// <summary>
        /// The characters available hit dice
        /// </summary>
        public int AvailableHitDice { get; set; }

        /// <summary>
        /// An array containing the character's max hit dice for each size, 
        /// index 0 = d6, 1 = d8, 2 = d10, 3 = d12
        /// </summary>
        public int[] MaxHitDice { get {
                int[] hitDice = new int[4];
                foreach (CharacterClass characterClass in Classes)
                {
                    hitDice[characterClass.Class.HitDie / 2 - 3] += characterClass.Level;
                }
                return hitDice;
            }}

        /// <summary>
        /// The characters current walking speed
        /// </summary>
        public int WalkingSpeed { get; set; }

        /// <summary>
        /// The characters Strength stat.
        /// Typically can't be higher than 20, or lower than 1.
        /// But there are cases where it can be higher than 20.
        /// </summary>
        [Range(0, 30)]
        public int Strength { get; set; }

        /// <summary>
        /// The characters Dexterity stat.
        /// Typically can't be higher than 20, or lower than 1.
        /// But there are cases where it can be higher than 20.
        /// </summary>
        [Range(0, 30)]
        public int Dexterity { get; set; }

        /// <summary>
        /// The characters Constitution stat.
        /// Typically can't be higher than 20, or lower than 1.
        /// But there are cases where it can be higher than 20.
        /// </summary>
        [Range(0, 30)]
        public int Constitution { get; set; }

        /// <summary>
        /// The characters Intelligence stat.
        /// Typically can't be higher than 20, or lower than 1.
        /// But there are cases where it can be higher than 20.
        /// </summary>
        [Range(0, 30)]
        public int Intelligence { get; set; }

        /// <summary>
        /// The characters Wisdom stat.
        /// Typically can't be higher than 20, or lower than 1.
        /// But there are cases where it can be higher than 20.
        /// </summary>
        [Range(0, 30)]
        public int Wisdom { get; set; }

        /// <summary>
        /// The characters Charisma stat.
        /// Typically can't be higher than 20, or lower than 1.
        /// But there are cases where it can be higher than 20.
        /// </summary>
        [Range(0, 30)]
        public int Charisma { get; set; }

        /// <summary>
        /// The characters resistances
        /// </summary>
        public string Resistances { get; set; }

        /// <summary>
        /// The characters immunities
        /// </summary>
        public string Immunities { get; set; }

        /// <summary>
        /// The characters vulnerabilities
        /// </summary>
        public string Vulnerabilities { get; set; }

        /// <summary>
        /// The notes a player wants to keep with this character
        /// </summary>
        public string PlayerNotes { get; set; }

        /// <summary>
        /// The characters race
        /// </summary>
        public Race Race { get; set; }

        /// <summary>
        /// The background of the character
        /// </summary>
        public Background Background { get; set; }

        /// <summary>
        /// A list of skills, saving throws, languages, and equipment, and the character's proficiency in them
        /// </summary>
        public List<CharacterProficiency> Proficiencies { get; set; } = null!;

        /// <summary>
        /// A list of the character's classes and subclasses
        /// </summary>
        public List<CharacterClass> Classes { get; set; } = null!;

        /// <summary>
        /// A list of the character's spellcasting abilities
        /// </summary>
        public List<CharacterSpellcasting> Spellcasting { get; set; }

        /// <summary>
        /// A list of the character's features
        /// </summary>
        public List<CharacterFeature> Features { get; set; }

        /// <summary>
        /// A list of the effects applied to the character
        /// </summary>
        public List<CharacterEffect> CharacterEffects { get; set; }

        /// <summary>
        /// A list of the character's actions
        /// </summary>
        public List<CharacterAction> Actions { get; set; }

        /// <summary>
        /// Contains the character's inventory information
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Contains the Id of the user who created the character
        /// </summary>
        public string UserId { get; set; }
        #endregion

        public Character()
        {
            Proficiencies = new List<CharacterProficiency>();
            Classes = new List<CharacterClass>();
            Spellcasting = new List<CharacterSpellcasting>();
            Features = new List<CharacterFeature>();
            Actions = new List<CharacterAction>();
            CharacterEffects = new List<CharacterEffect>();
            Inventory = new Inventory(this);
            Name = "Unnamed Character";
            Resistances = "";
            Immunities = "";
            Vulnerabilities = "";
            PlayerNotes = "";
        }
        public Character(CreateCharacterViewModel character, 
            Race race, Background background, List<Proficiency> defaultProficiencies, string userId)
        {
            UserId = userId;
            Name = character.Name;
            Classes = new List<CharacterClass>();
            Proficiencies = new List<CharacterProficiency>();
            Spellcasting = new List<CharacterSpellcasting>();
            Features = new List<CharacterFeature>();
            CharacterEffects = new List<CharacterEffect>();
            Actions = new List<CharacterAction>();
            Inventory = new Inventory(this);
            Background = background;
            Race = race;
            MaxHealth = character.MaxHealth;
            CurrentHealth = MaxHealth;
            TempHealth = 0;
            Strength = character.Strength;
            Dexterity = character.Dexterity;
            Constitution = character.Constitution;
            Intelligence = character.Intelligence;
            Wisdom = character.Wisdom;
            Charisma = character.Charisma;
            WalkingSpeed = race.Speed;
            Resistances = "";
            Immunities = "";
            Vulnerabilities = "";
            PlayerNotes = "";

            foreach (Proficiency proficiency in defaultProficiencies)
            {
                Proficiencies.Add(new CharacterProficiency(this, proficiency));
            }
            
        }

        #region methods
        /// <summary>
        /// Calculates the average maximum health of the character
        /// </summary>
        /// <returns>The average maximum health of the character</returns>
        public int GetAverageHealth()
        {
            int averageHealth = MaxHitDice[0] * (4 + GetModifier("con")) 
                + MaxHitDice[1] * (5 + GetModifier("con")) 
                + MaxHitDice[2] * (6 + GetModifier("con")) 
                + MaxHitDice[3] * (7 + GetModifier("con"));
            foreach(CharacterClass characterClass in Classes)
            {
                if(characterClass.InitialClass)
                {
                    if(characterClass.Class.HitDie == 6)
                    {
                        averageHealth += 2;
                    }
                    else if (characterClass.Class.HitDie == 8)
                    {
                        averageHealth += 3;
                    }
                    else if (characterClass.Class.HitDie == 10)
                    {
                        averageHealth += 4;
                    }
                    else if (characterClass.Class.HitDie == 12)
                    {
                        averageHealth += 5;
                    }
                    break;
                }
            }
            return averageHealth;
        }

        /// <summary>
        /// Calculates a value based on a formula
        /// </summary>
        /// <param name="formula">The formula to use, 
        /// using numbers, operators, attributes,
        /// and attribute modifiers separated by spaces, 
        /// for example dex + 10 - strmod</param>
        /// <returns>The result of the formula, throws an exception if the formula is invalid</returns>
        public int Calculate(string formula)
        {
            string[] parts = formula.Split(' ');
            int value = ParseValue(parts[0]);
            for (int i = 1; i < parts.Length; i += 2)
            {
                if (parts[i] == "+")
                {
                    value += ParseValue(parts[i + 1]);
                }
                else if (parts[i] == "-")
                {
                    value -= ParseValue(parts[i + 1]);
                }
                else if (parts[i] == "*")
                {
                    value *= ParseValue(parts[i + 1]);
                }
                else if (parts[i] == "/")
                {
                    value /= ParseValue(parts[i + 1]);
                }
                else
                {
                    throw new ArgumentException("Invalid formula", formula);
                }
            }
            return value;
        } 

        /// <summary>
        /// Attempts to translate a string into a number, 
        /// either by getting the value of an attribute, attribute modifier or by parsing the string
        /// </summary>
        /// <param name="str">The string to parse</param>
        /// <returns>The translated value</returns>
        public int ParseValue(string str)
        {
            if(int.TryParse(str, out int value))
            {
                return value;
            }
            else
            {
                if (str.ToLower().Contains("mod"))
                {
                    return GetModifier(str.Substring(0, str.Length - 3));
                }
                else if (str.ToLower().Contains("prof"))
                {
                    return ProficiencyBonus;
                }
                else
                {
                    return GetAttribute(str);
                }
            }
        }

        /// <summary>
        /// Gets the score of the specified attribute
        /// </summary>
        /// <param name="name">The name of the attribute
        /// (strength, dexterity, constitution, intelligence, wisdom, or charisma)</param>
        /// <returns>Returns the score of the specified attribute, -1 if it can't be found</returns>
        public int GetAttribute(string name)
        {
            if(name.ToLower().Contains("str"))
            {
                return Strength;
            }
            else if (name.ToLower().Contains("dex"))
            {
                return Dexterity;
            }
            else if (name.ToLower().Contains("con"))
            {
                return Constitution;
            }
            else if (name.ToLower().Contains("int"))
            {
                return Intelligence;
            }
            else if (name.ToLower().Contains("wis"))
            {
                return Wisdom;
            }
            else if (name.ToLower().Contains("cha"))
            {
                return Charisma;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets the modifier of the specified attribute
        /// </summary>
        /// <param name="name">The name of the attribute
        /// (strength, dexterity, constitution, intelligence, wisdom, or charisma)</param>
        /// <returns>Returns the modifier of the specified attribute, 
        /// which is calculated with (score - 10) / 2</returns>
        public int GetModifier(string name)
        {
            return (GetAttribute(name) - 10) / 2;
        }

        /// <summary>
        /// Sets the score of the specified attribute
        /// </summary>
        /// <param name="name">The attribute to set</param>
        /// <param name="value">The number to set it to</param>
        public void SetAttribute(string name, int value)
        {
            if (name.ToLower().Contains("str"))
            {
                Strength = value;
            }
            else if (name.ToLower().Contains("dex"))
            {
                Dexterity = value;
            }
            else if (name.ToLower().Contains("con"))
            {
                Constitution = value;
            }
            else if (name.ToLower().Contains("int"))
            {
                Intelligence = value;
            }
            else if (name.ToLower().Contains("wis"))
            {
                Wisdom = value;
            }
            else if (name.ToLower().Contains("cha"))
            {
                Charisma = value;
            }
        }

        /// <summary>
        /// Changes the score of the specified attribute by the specified amount
        /// </summary>
        /// <param name="name">The attribute to change</param>
        /// <param name="value">The amount to change it by</param>
        public void ModifyAttribute(string name, int value)
        {
            SetAttribute(name, value + GetAttribute(name));
        }

        public CharacterAction? GetAction(string name)
        {
            return Actions.Where(a => a.Action.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Grants a proficiency to the character
        /// </summary>
        /// <param name="proficiency">The proficiency to grant</param>
        public void GrantProficiency(Proficiency proficiency)
        {
            GrantProficiency(proficiency, false);
        }

        /// <summary>
        /// Grants a proficiency to the character
        /// </summary>
        /// <param name="proficiency">The proficiency to grant</param>
        /// <param name="expertise">Whether the character should get normal proficiency or expertise</param>
        public void GrantProficiency(Proficiency proficiency, bool expertise)
        {
            CharacterProficiency? existingProficiency = GetProficiency(proficiency.Name);
            if (existingProficiency == null)
            {
                if(expertise)
                {
                    Proficiencies.Add(new CharacterProficiency(this, proficiency, 2, 0));
                }
                else
                {
                    Proficiencies.Add(new CharacterProficiency(this, proficiency, 1, 0));
                }
            }
            else
            {
                if(expertise)
                {
                    existingProficiency.ProficiencyLevel = 2;
                }
                else if (existingProficiency.ProficiencyLevel < 2)
                {
                    existingProficiency.ProficiencyLevel = 1;
                }
            }
        }

        /// <summary>
        /// Removes a proficiency from the character
        /// </summary>
        /// <param name="characterProficiency"></param>
        public void RemoveProficiency(Proficiency proficiency)
        {
            CharacterProficiency? characterProficiency = GetProficiency(proficiency.Name);
            if (characterProficiency != null)
            {
                characterProficiency.ProficiencyLevel = 0;
                if (characterProficiency.Proficiency.Type != "saving throw" && characterProficiency.Proficiency.Type != "skill")
                {
                    Proficiencies.Remove(characterProficiency);
                }
            }
        } 

        /// <summary>
        /// Gets a CharacterProficiency with a given name
        /// </summary>
        /// <param name="name">The name of the proficiency</param>
        /// <returns>The specified CharacterProficiency, null if none</returns>
        public CharacterProficiency? GetProficiency(string name)
        {
            return Proficiencies.Where(p => p.Proficiency.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of CharacterProficiencies by type
        /// </summary>
        /// <param name="type">The type of proficiency to get</param>
        /// <returns>A list of character proficiencies</returns>
        public List<CharacterProficiency> GetProficienciesByType(string type)
        {
            return Proficiencies.Where(t => t.Proficiency.Type == type).ToList();
        }

        /// <summary>
        /// Gets all CharacterProficiencies tagged as skills
        /// </summary>
        /// <returns>A list of CharacterProficiencies</returns>
        public List<CharacterProficiency> GetSkills()
        {
            List<CharacterProficiency> skills = Proficiencies.Where(p => p.Proficiency.Type == "skill").ToList();
            return skills;
        }

        /// <summary>
        /// Gets all CharacterProficiencies tagged as saving throws
        /// </summary>
        /// <returns>A list of CharacterProficiencies</returns>
        public List<CharacterProficiency> GetSaves()
        {
            List<CharacterProficiency> saves = Proficiencies.Where(p => p.Proficiency.Type == "saving throw").ToList();
            return saves;
        }


        public void SetActiveFeatures()
        {
            //Remove features that shouldn't be active
            foreach (CharacterFeature characterFeature in Features)
            {
                Feature feature = characterFeature.Feature;
                bool valid = true;
                if (feature.Level > Level)
                {
                    valid = false;
                }
                else if (feature is BackgroundFeature backgroundFeature && 
                    backgroundFeature.Background.BackgroundId != Background.BackgroundId)
                {
                    valid = false;
                }
                else if (feature is RaceFeature raceFeature &&
                    raceFeature.Race.RaceId != Race.RaceId)
                {
                    valid = false;
                }
                else if (feature is ClassFeature classFeature)
                {
                    CharacterClass? sourceClass = Classes
                        .Where(c => c.Class.ClassId == classFeature.Class.ClassId).FirstOrDefault();
                    if (sourceClass == null)
                    {
                        valid = false;
                    }
                    else if (classFeature.InitialClassOnly && !sourceClass.InitialClass)
                    {
                        valid = false;
                    }
                    else if (classFeature.MulticlassOnly && sourceClass.InitialClass)
                    {
                        valid = false;
                    }
                    else if (classFeature.Level > sourceClass.Level)
                    {
                        valid = false;
                    }
                }
                else if (feature is SubclassFeature subclassFeature)
                {
                    CharacterClass? sourceSublass = Classes
                        .Where(c => c.Subclass?.SubclassId == subclassFeature.Subclass?.SubclassId).FirstOrDefault();
                    if (sourceSublass == null)
                    {
                        valid = false;
                    }
                    else if (subclassFeature.Level > sourceSublass.Level)
                    {
                        valid = false;
                    }
                }
                if (!valid)
                {
                    characterFeature.RemoveEffect();
                    Features.Remove(characterFeature);
                }
            }

            List<Feature> features = new List<Feature>();
            foreach (CharacterClass @class in Classes)
            {
                features.AddRange(@class.Class.GetAvailableFeatures(@class.Level));
                if (@class.Subclass != null)
                {
                    features.AddRange(@class.Subclass.GetAvailableFeatures(@class.Level));
                }
            }
            features.AddRange(Background.Features.Where(f => f.Level <= Level));
            features.AddRange(Race.Features.Where(f => f.Level <= Level));
            
            //Add features that haven't been added yet
            foreach (Feature feature in features)
            {
                if (!Features.Where(f => f.Equals(feature)).Any())
                {
                    CharacterFeature characterFeature = new CharacterFeature(this, feature);
                    Features.Add(characterFeature);
                    characterFeature.ApplyEffect();
                }
            }
            ApplyEffects();
        }
        public void ApplyEffects()
        {
            RemoveEffects();
            for (int i = 0; i < CharacterEffects.Count; i++)
            {
                CharacterEffects[i].ApplyEffect();
            }
        }
        public void RemoveEffects()
        {
            for (int i = 0; i < CharacterEffects.Count; i++)
            {
                CharacterEffects[i].RemoveEffect();
            }
        }
        #endregion
    }

    public class CreateCharacterViewModel
    {
        /// <summary>
        /// The name of the character
        /// </summary>
        [DefaultValue("Unnamed Character")]
        public string Name { get; set; } = "Unnamed Character";

        /// <summary>
        /// A list containing arrays of level, class id, and subclass id
        /// </summary>
        public List<int[]> Classes { get; set; }

        /// <summary>
        /// The id of background of the character.
        /// </summary>
        public int BackgroundId { get; set; }

        /// <summary>
        /// The Id of characters race
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// The list of available classes
        /// </summary>
        public List<Class> AvailableClasses { get; set; }

		/// <summary>
		/// The list of available races
		/// </summary>
		public List<Race> AvailableRaces { get; set; }

		/// <summary>
		/// The list of available backgrounds
		/// </summary>
		public List<Background> AvailableBackgrounds { get; set; }

        /// <summary>
        /// The character's maximum health.
        /// </summary>
        public int MaxHealth { get; set; } = 7;

        /// <summary>
        /// The character's strength stat. 
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Strength { get; set; } = 8;

        /// <summary>
        /// The character's dexterity stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Dexterity { get; set; } = 8;

        /// <summary>
        /// The character's constitution stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Constitution { get; set; } = 8;

        /// <summary>
        /// The character's intelligence stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Intelligence { get; set; } = 8;

        /// <summary>
        /// The character's wisdom stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Wisdom { get; set; } = 8;

        /// <summary>
        /// The character's charisma stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Charisma { get; set; } = 8;
    }

    public class FeatureChoiceViewModel
    {
        public Character Character { get; set; }

        // Normally I would handle this inside the CharacterFeature class, 
        // but the view simply isn't passing it back to the controller and I don't know why
        // So this is a hopefully temporary workaround
        // As is, it won't work with choices in choices
        // We don't have any of those yet, but we will if we implement Feats
        public List<int?> ChoiceValues { get; set; }
    }
}
