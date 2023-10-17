using System.ComponentModel;
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
        /// The characters Strength stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Strength { get; set; }

        /// <summary>
        /// The characters Dexterity stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Dexterity { get; set; }

        /// <summary>
        /// The characters Constitution stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Constitution { get; set; }

        /// <summary>
        /// The characters Intelligence stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Intelligence { get; set; }

        /// <summary>
        /// The characters Wisdom stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
        public int Wisdom { get; set; }

        /// <summary>
        /// The characters Charisma stat.
        /// Cannot be higher than 20, or lower than 1.
        /// </summary>
        [Range(1, 20)]
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
    }

    public class CreateCharacterViewModel
    {
        /// <summary>
        /// The name of the character
        /// </summary>
        [DefaultValue("Unnamed Character")]
        public string Name { get; set; }

        /// <summary>
        /// The level of the character.
        /// Cannot be more than 20, or less than 1.
        /// </summary>
        [Range(1, 20)]
        public int Level { get; set; }

        /// <summary>
        /// The classes the character has.
        /// </summary>
        public List<CharacterClass> Classes { get; set; }

        /// <summary>
        /// The background of the character.
        /// </summary>
        public Background Background { get; set; }

        /// <summary>
        /// The proficiencies the character has.
        /// </summary>
        public List<CharacterProficiency> Proficiencies { get; set; }

        /// <summary>
        /// The spellcasting ability of the character, if they have one.
        /// </summary>
        public List<CharacterSpellcasting> Spellcasting { get; set; }

        // race
        public Race Race { get; set; }

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
