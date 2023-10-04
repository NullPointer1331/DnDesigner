using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    
    /// <summary>
    /// Represents a single saving throw or skill proficiency
    /// </summary>
    public class Proficiency
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int ProficiencyId { get; set; }

        /// <summary>
        /// The name of the skill or saving throw
        /// </summary>
        public string ProficiencyName { get; set; }

        /// <summary>
        /// The attribue associated with the skill or saving throw
        /// </summary>
        public string MainAttribute { get; set; }

        /// <summary>
        /// Whether the proficiency is a saving throw or skill
        /// </summary>
        public bool IsSavingThrow { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The name of the proficiency</param>
        /// <param name="attribute">The attribute associated with this proficiency</param>
        /// <param name="isSavingThrow">If true, this is a saving throw, if false this is a skill</param>
        public Proficiency (string name, string attribute, bool isSavingThrow)
        {
            ProficiencyName = name;
            MainAttribute = attribute;
            IsSavingThrow = isSavingThrow;
        }
    }

    /// <summary>
    /// Represents a character's proficiency in a saving throw or skill
    /// </summary>
    public class CharacterProficiency
    {
        /// <summary>
        /// The id of the character this proficiency belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        /// <summary>
        /// The id of the proficiency this is referencing
        /// </summary>
        [ForeignKey("ProficiencyId")]
        public Proficiency Proficiency { get; set; }

        /// <summary>
        /// A representation of how proficient the character is in this skill or saving throw.
        /// 0 = not proficient, 1 = proficient, 2 = expertise
        /// </summary>
        [Range(0, 2)]
        public int ProficiencyLevel { get; set; }

        /// <summary>
        /// Any other bonus the character gets to this proficiency
        /// </summary>
        public int CheckBonus { get; set; }

        /// <summary>
        /// Minimal constructor, sets character and proficiency, sets proficiency level and check bonus to 0
        /// </summary>
        /// <param name="character">The character who has this proficiency</param>
        /// <param name="proficiency">The saving throw or skill this is referencing</param>
        public CharacterProficiency(Character character, Proficiency proficiency)
        {
            Character = character;
            Proficiency = proficiency;
            ProficiencyLevel = 0;
            CheckBonus = 0;
        }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="character">The character who has this proficiency</param>
        /// <param name="proficiency">The saving throw or skill this is referencing</param>
        /// <param name="proficiencyLevel">How proficient the character is in this skill or saving throw</param>
        /// <param name="checkBonus">Any bonus the character gets to this proficiency</param>
        public CharacterProficiency (Character character, Proficiency proficiency, int proficiencyLevel, int checkBonus)
        {
            Character = character;
            Proficiency = proficiency;
            ProficiencyLevel = proficiencyLevel;
            CheckBonus = checkBonus;
        }
    }
}
