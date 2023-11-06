using Microsoft.EntityFrameworkCore;
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
        public string Name { get; private set; }

        /// <summary>
        /// The attribute associated with the skill or saving throw
        /// </summary>
        public string? MainAttribute { get; private set; }

        /// <summary>
        /// The proficiency type
        /// Can be a skill, saving throw, language, tool, armor, weapon, or instrument
        /// </summary>
        public string Type { get; private set; }


        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The name of the proficiency</param>
        /// <param name="mainAttribute">The attribute associated with this proficiency</param>
        /// <param name="type">The type of proficiency i.e. Tool, Skill, Saving throw</param>
        public Proficiency (string name, string? mainAttribute, string type)
        {
            Name = name;
            MainAttribute = mainAttribute;
            Type = type;
        }
    }

    /// <summary>
    /// Represents a character's proficiency in a saving, throw skill, or other proficiency
    /// </summary>
    [PrimaryKey(nameof(CharacterId), nameof(ProficiencyId))]
    public class CharacterProficiency
    {
        /// <summary>
        /// The character this proficiency belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CharacterId { get; set; }

        /// <summary>
        /// The proficiency this is referencing
        /// </summary>
        [ForeignKey("ProficiencyId")]
        public Proficiency Proficiency { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProficiencyId { get; set; }

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

        public CharacterProficiency(int characterId, int proficiencyId) {
            CharacterId = characterId;
            ProficiencyId = proficiencyId;
        }

        /// <summary>
        /// Calculates the character's bonus to rolls with this proficiency 
        /// </summary>
        /// <returns>The bonus to rolls with this proficiency, 
        /// null if the proficiency doesn't have a main attribute</returns>
        public int? GetRollBonus()
        {
            if (Proficiency.MainAttribute != null)
            {
                return Character.GetModifier(Proficiency.MainAttribute) +
                    (Character.ProficiencyBonus * ProficiencyLevel) + CheckBonus;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Represents a proficiency granted by a background
    /// </summary>
    [PrimaryKey(nameof(BackgroundId), nameof(ProficiencyId))]
    public class BackgroundProficiency
    {
        /// <summary>
        /// The background this proficiency belongs to
        /// </summary>
        [ForeignKey("BackgroundId")]
        public Background Background { get; set; }

        [Key]
        [Column(Order = 1)]
        public int BackgroundId { get; set; }

        /// <summary>
        /// The proficiency this is referencing
        /// </summary>
        [ForeignKey("ProficiencyId")]
        public Proficiency Proficiency { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProficiencyId { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="background">The background this proficiency belongs to</param>
        /// <param name="proficiency">The proficiency this is referencing</param>
        public BackgroundProficiency(Background background, Proficiency proficiency)
        {
            Background = background;
            Proficiency = proficiency;
            ProficiencyId = proficiency.ProficiencyId;
            BackgroundId = background.BackgroundId;
        }
        public BackgroundProficiency(int backgroundId, int proficiencyId) {
            BackgroundId = backgroundId;
            ProficiencyId = proficiencyId;
        }
    }

    /// <summary>
    /// Represents a proficiency granted by a race
    /// </summary>
    [PrimaryKey(nameof(RaceId), nameof(ProficiencyId))]
    public class RaceProficiency
    {
        /// <summary>
        /// The Race associated with this proficiency.
        /// </summary>
        [ForeignKey("RaceId")]
        public Race Race { get; set; }

        public int RaceId { get; set; }

        /// <summary>
        /// The proficiency this is referencing
        /// </summary>
        [ForeignKey("ProficiencyId")]
        public Proficiency Proficiency { get; set; }

        public int ProficiencyId { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="race">The race that has this proficiency</param>
        /// <param name="proficiency">The proficiency that this race has</param>
        public RaceProficiency(Race race, Proficiency proficiency)
        {
            Race = race;
            Proficiency = proficiency;
        }
        public RaceProficiency(int raceId, int proficiencyId)
        {
            RaceId = raceId;
            ProficiencyId = proficiencyId;
        }
    }

    /// <summary>
    /// Represents a proficiency granted by a class.
    /// </summary>
    [PrimaryKey(nameof(ClassId), nameof(ProficiencyId))]
    public class ClassProficiency
    {
        /// <summary>
        /// The class associated with this proficiency.
        /// </summary>
        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        public int ClassId { get; set; }

        /// <summary>
        /// The proficiency this is referencing
        /// </summary>
        [ForeignKey("ProficiencyId")]
        public Proficiency Proficiency { get; set; }

        public int ProficiencyId { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceclass">The class that has this proficiency</param>
        /// <param name="proficiency">The proficiency that this class has</param>
        public ClassProficiency(Class sourceclass, Proficiency proficiency)
        {
            Class = sourceclass;
            Proficiency = proficiency;
        }
        public ClassProficiency(int classId, int proficiencyId)
        {
            ClassId = classId;
            ProficiencyId = proficiencyId;
        }
    }
}
