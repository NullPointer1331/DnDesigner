using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    /// <summary>
    /// The information of the character
    /// </summary>
    public class Character
    {
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
        public int Level { get; set; }

        /// <summary>
        /// The characters current proficiency bonus
        /// </summary>
        public int ProficiencyBonus { get; set; }

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
        /// The characters hit die type
        /// </summary>
        public string HitDieType { get; set; }

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
        /// The background of the character
        /// </summary>
        public Background Background { get; set; }

        /// <summary>
        /// The weapon and armor types the character is proficient with
        /// </summary>
        public List<CharacterProficiency> EquipmentProficiencies { get; set; } = null!;

        /// <summary>
        /// The tool and instrument types the character is proficient with
        /// </summary>
        public List<CharacterProficiency> ToolProficiencies { get; set; } = null!;

        /// <summary>
        /// A list of the languages the character is proficient in
        /// </summary>
        public List<CharacterProficiency> LanguageProficiencies { get; set; } = null!;

        /// <summary>
        /// A list of saving throws and the character's proficiency in them
        /// </summary>
        public List<CharacterProficiency> SavingThrowProficiencies { get; set; } = null!;

        /// <summary>
        /// A list of skills and the character's proficiency in them
        /// </summary>
        public List<CharacterProficiency> SkillProficiencies { get; set; } = null!;

        /// <summary>
        /// A list of the character's classes
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
    }
}
