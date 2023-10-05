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
        public int ProficiencyId { get; private set; }

        /// <summary>
        /// The name of the skill or saving throw
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The attribute associated with the skill or saving throw
        /// </summary>
        public string? MainAttribute { get; private set; }

        /// <summary>
        /// The proficiency type
        /// Can be a skill, saving throw, language, tool, or type of equipment
        /// </summary>
        public string ProficiencyType { get; private set; }


        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The name of the proficiency</param>
        /// <param name="attribute">The attribute associated with this proficiency</param>
        /// <param name="proficiencyType">The type of proficiency i.e. Tool, Skill, Saving throw</param>
        public Proficiency (string name, string? attribute, string proficiencyType)
        {
            Name = name;
            MainAttribute = attribute;
            ProficiencyType = proficiencyType;
        }
    }

    /// <summary>
    /// Represents a character's proficiency in a saving throw or skill
    /// </summary>
    public class CharacterProficiency
    {
        /// <summary>
        /// The character this proficiency belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        /// <summary>
        /// The proficiency this is referencing
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
        /// Minimal constructor, sets character and proficiency, 
        /// sets proficiency level and check bonus to 0
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

        /// <summary>
        /// Constructor that takes a background proficiency and sets the proficiency level to 1
        /// </summary>
        /// <param name="character">The character who has this proficiency</param>
        /// <param name="backgroundProficiency">A proficiency from a background</param>
        public CharacterProficiency(Character character, BackgroundProficiency backgroundProficiency)
        {
            Character = character;
            Proficiency = backgroundProficiency.Proficiency;
            ProficiencyLevel = 1;
            CheckBonus = 0;
        }
    }

    public class BackgroundProficiency
    {
        /// <summary>
        /// The background this proficiency belongs to
        /// </summary>
        [ForeignKey("BackgroundId")]
        public Background Background { get; set; }

        /// <summary>
        /// The proficiency this is referencing
        /// </summary>
        [ForeignKey("ProficiencyId")]
        public Proficiency Proficiency { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="background">The background this proficiency belongs to</param>
        /// <param name="proficiency">The proficiency this is referencing</param>
        public BackgroundProficiency(Background background, Proficiency proficiency)
        {
            Background = background;
            Proficiency = proficiency;
        }
    }
}
