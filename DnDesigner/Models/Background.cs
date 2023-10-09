using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents a background
    /// </summary>
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
        /// The source book the background is from
        /// </summary>
        public string Sourcebook { get; set; }

        /// <summary>
        /// The proficiencies this background gives
        /// </summary>
        public List<BackgroundProficiency> Proficiencies { get; set; }


        /// <summary>
        /// The starting equipment this background gives
        /// </summary>
        public List<Item> StarterEquipment { get; set; }

        /// <summary>
        /// All features of this background
        /// </summary>
        public List<BackgroundFeature> BackgroundFeature { get; set; }

        /// <summary>
        /// The gold this background gives
        /// </summary>
        int StarterGold { get; set; }

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

        /// <summary>
        /// Minimal constructor, Initializes lists and leaves everything else blank
        /// </summary>
        public Background()
        {
            Proficiencies = new List<BackgroundProficiency>();
            StarterEquipment = new List<Item>();
        }
    }
}
