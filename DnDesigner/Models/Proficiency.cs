﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    [PrimaryKey("CharacterId", "ProficiencyId")]
    public class CharacterProficiency
    {
        /// <summary>
        /// The character this proficiency belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        [JsonIgnore]
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

        private CharacterProficiency() { }

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
}
