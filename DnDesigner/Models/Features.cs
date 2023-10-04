using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents a character feature from various sources
    /// i.e. Class, Subclass, Background
    /// </summary>
    public class Features
    {
        /// <summary>
        /// The feature's Unique identifier
        /// </summary>
        [Key]
        public int FeatureId { get; private set; }

        /// <summary>
        /// The feature's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The feature's source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The feature's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The level the feature is available at
        /// </summary>
        public int Level { get; set; }
    }
}
