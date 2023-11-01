using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

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
        public string Resistances { get; set; } = null!;

        /// <summary>
        /// The characters immunities
        /// </summary>
        public string Immunities { get; set; } = null!;

        /// <summary>
        /// The characters vulnerabilities
        /// </summary>
        public string Vulnerabilities { get; set; } = null!;

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
        /// Contains the character's inventory information
        /// </summary>
        public Inventory Inventory { get; set; }
        #endregion

        public Character()
        {
            Proficiencies = new List<CharacterProficiency>();
            Classes = new List<CharacterClass>();
            Spellcasting = new List<CharacterSpellcasting>();
            Inventory = new Inventory(this);
            Name = "Unnamed Character";
            Resistances = "";
            Immunities = "";
            Vulnerabilities = "";
        }
        public Character(CreateCharacterViewModel character, Class @class, 
            Race race, Background background, List<Proficiency> defaultProficiencies)
        {
            Name = character.Name;
            Classes = new List<CharacterClass>();
            Proficiencies = new List<CharacterProficiency>();
            Spellcasting = new List<CharacterSpellcasting>();
            Inventory = new Inventory(this);
            Background = background;
            Race = race;
            TempHealth = 0;
            Strength = character.Strength;
            Dexterity = character.Dexterity;
            Constitution = character.Constitution;
            Intelligence = character.Intelligence;
            Wisdom = character.Wisdom;
            Charisma = character.Charisma;
            WalkingSpeed = race.Speed;
            MaxHealth = character.MaxHealth;
            CurrentHealth = character.MaxHealth;
            Resistances = "";
            Immunities = "";
            Vulnerabilities = "";

            Classes.Add(new CharacterClass(this, @class, 1));
            if(@class.Spellcasting != null)
            {
                Spellcasting.Add(new CharacterSpellcasting(this, @class.Spellcasting));
            }
            SetProficiencies(defaultProficiencies);
        }

        #region methods
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

        /// <summary>
        /// Sets the character's proficiencies
        /// </summary>
        /// <param name="defaultProficiencies">A list of all skills and saving throws</param>
        public void SetProficiencies(List<Proficiency> defaultProficiencies)
        {
            foreach(Proficiency proficiency in defaultProficiencies){
                Proficiencies.Add(new CharacterProficiency(this, proficiency));
            }
            foreach (CharacterClass characterClass in Classes)
            {
                foreach (ClassProficiency classProficiency in characterClass.Class.Proficiencies)
                {
                    GrantProficiency(classProficiency.Proficiency);
                }
            }
            foreach (BackgroundProficiency backgroundProficiency in Background.Proficiencies)
            {
                GrantProficiency(backgroundProficiency.Proficiency);
            }
            foreach (RaceProficiency raceProficiency in Race.Proficiencies)
            {
                GrantProficiency(raceProficiency.Proficiency);
            }
        }

        /// <summary>
        /// Grants a proficiency to the character
        /// </summary>
        /// <param name="proficiency">The proficiency to grant</param>
        public void GrantProficiency(Proficiency proficiency)
        {
            CharacterProficiency? existingProficiency = GetProficiency(proficiency.Name);
            if (existingProficiency == null)
            {
                Proficiencies.Add(new CharacterProficiency(this, proficiency, 1, 0));
            }
            else
            {
                if(existingProficiency.ProficiencyLevel < 2)
                {
                    existingProficiency.ProficiencyLevel = 1;
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
        /// Gets all CharacterProficiencies tagged as skills
        /// </summary>
        /// <returns>A list of CharacterProficiencies</returns>
        public List<CharacterProficiency> GetSkills()
        {
            return Proficiencies.Where(p => p.Proficiency.Type == "skill").ToList();
        }

        /// <summary>
        /// Gets all CharacterProficiencies tagged as saving throws
        /// </summary>
        /// <returns>A list of CharacterProficiencies</returns>
        public List<CharacterProficiency> GetSaves()
        {
            return Proficiencies.Where(p => p.Proficiency.Type == "saving throw").ToList();
        }

        /// <summary>
        /// Gets all features from classes, subclasses, race and background 
        /// where the character meets the required level
        /// </summary>
        /// <returns>All features the character meets the required level for</returns>
        public List<Feature> GetActiveFeatures()
        {
            List<Feature> features = new List<Feature>();
            foreach(CharacterClass @class in Classes)
            {
                features.AddRange(@class.Class.GetAvailableFeatures(@class.Level));
                if(@class.Subclass != null)
                {
                    features.AddRange(@class.Subclass.GetAvailableFeatures(@class.Level));
                }
            }
            features.AddRange(Background.Features.Where(f => f.Level <= Level));
            features.AddRange(Race.Features.Where(f => f.Level <= Level));
            return features;
        }
        #endregion
    }

    public class CreateCharacterViewModel
    {
        /// <summary>
        /// The name of the character
        /// </summary>
        [DefaultValue("Unnamed Character")]
        public string Name { get; set; }

        /*
        /// <summary>
        /// The level of the character.
        /// Cannot be more than 20, or less than 1.
        /// </summary>
        [Range(1, 20)]
        public int Level { get; set; }*/

        /// <summary>
        /// The classes the character has.
        /// </summary>
        //public List<CharacterClass> Classes { get; set; }

        /// <summary>
        /// The id of selected class
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// The id of background of the character.
        /// </summary>
        public int BackgroundId { get; set; }

        /*
        /// <summary>
        /// The proficiencies the character has.
        /// </summary>
        public List<CharacterProficiency> Proficiencies { get; set; }*/

        /*
        /// <summary>
        /// The spellcasting ability of the character, if they have one.
        /// </summary>
        public List<CharacterSpellcasting> Spellcasting { get; set; }*/

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
		/// The character's maximum health points.
		/// </summary>
		public int MaxHealth { get; set; }

        /// <summary>
        /// The character's strength stat. 
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Strength { get; set; }

        /// <summary>
        /// The character's dexterity stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Dexterity { get; set; }

        /// <summary>
        /// The character's constitution stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Constitution { get; set; }

        /// <summary>
        /// The character's intelligence stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Intelligence { get; set; }

        /// <summary>
        /// The character's wisdom stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Wisdom { get; set; }

        /// <summary>
        /// The character's charisma stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Charisma { get; set;}
    }
}
