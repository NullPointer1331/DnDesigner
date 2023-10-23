using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int[] HitDieType { get {
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
        /// The characters Strength stat
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// The characters Dexterity stat
        /// </summary>
        public int Dexterity { get; set; }

        /// <summary>
        /// The characters Constitution stat
        /// </summary>
        public int Constitution { get; set; }

        /// <summary>
        /// The characters Intelligence stat
        /// </summary>
        public int Intelligence { get; set; }

        /// <summary>
        /// The characters Wisdom stat
        /// </summary>
        public int Wisdom { get; set; }

        /// <summary>
        /// The characters Charisma stat
        /// </summary>
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
}
