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
    }

    /// <summary>
    /// Represents a character's proficiency in a saving throw or skill
    /// </summary>
    public class CharacerProficiency
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
        /// The level of proficiency the character has. 0 = not proficient, 1 = proficient, 2 = expertise
        /// </summary>
        public int ProficiencyLevel { get; set; }

        /// <summary>
        /// Any other bonus the character gets to this proficiency
        /// </summary>
        public int CheckBonus { get; set; }
    }
}
