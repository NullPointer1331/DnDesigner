using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
    public class Background
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int BackgroundId { get; set; }

        /// <summary>
        /// The name of the background
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The skill proficiencies this background gives
        /// </summary>
        public List<Proficiency> SkillProficiencies { get; set; }

        /// <summary>
        /// The tool proficiencies this background gives
        /// </summary>
        public List<Proficiency>? ToolProficiencies { get; set; }

        /// <summary>
        /// The language proficiencies this background gives
        /// </summary>
        public List<Proficiency>? LanguageProficiencies { get; set; }

        /// <summary>
        /// The starting equipment this background gives
        /// Will be a list of Items when those are added
        /// </summary>
        public List<string> Equipment { get; set; }

        /// <summary>
        /// The main feature of this background
        /// </summary>
        public string BackgroundFeature { get; set; }

        /// <summary>
        /// Suggested personality traits for this background
        /// </summary>
        public string PersonalityTraits { get; set; }

        /// <summary>
        /// Suggested ideals for this background
        /// </summary>
        public string Ideals { get; set; }

        /// <summary>
        /// Suggested bonds for this background
        /// </summary>
        public string Bonds { get; set; }

        /// <summary>
        /// Suggested flaws for this background
        /// </summary>
        public string Flaws { get; set; }

        /// <summary>
        /// Other information about this background
        /// </summary>
        public string OtherInformation { get; set; }
    }
}
