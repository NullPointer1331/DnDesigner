using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents a background
    /// </summary>
    public class Background
    {
        #region properties
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
        /// The starting equipment this background gives
        /// </summary>
        [NotMapped]
        public List<Item> StarterEquipment { get; set; }

        /// <summary>
        /// All features of this background
        /// </summary>
        public List<BackgroundFeature> Features { get; set; }

        /// <summary>
        /// The gold this background gives
        /// </summary>
        public int StarterGold { get; set; }

        /// <summary>
        /// The description of this background
        /// </summary>
        public string Description { get; set; } = null!;
        #endregion

        /// <summary>
        /// Minimal constructor, Initializes lists and leaves everything else blank
        /// </summary>
        public Background()
        {
            StarterEquipment = new List<Item>();
            Features = new List<BackgroundFeature>();
            Name = "";
            Sourcebook = "";
            Description = "";
        }
    }
}
