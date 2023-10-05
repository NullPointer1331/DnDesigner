using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents a character feature from various sources
    /// i.e. Class, Subclass, Background
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// The feature's Unique identifier
        /// </summary>
        [Key]
        public int FeatureId { get; private set; }

        /// <summary>
        /// The feature's name
        /// </summary>
        public string FeatureName { get; set; }

        /// <summary>
        /// The feature's source
        /// formatted as Sourcebook, Class/Subclass/Background/Race, Source name
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


        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The name of the feature</param>
        /// <param name="source">The source of the feature</param>
        /// <param name="description">The feature's description</param>
        /// <param name="level">The level the feature is available at</param>
        public Feature(string name, string source, string description, int level)
        {
            FeatureName = name;
            Source = source;
            Description = description;
            Level = level;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Feature() { } //Without this I get errors in the constructors of inheriting classes, no clue why
    }
    /// <summary>
    /// Represents a feature for a class
    /// </summary>
    public class ClassFeature : Feature
    {
        /// <summary>
        /// The subclass that has this feature
        /// </summary>
        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="sourceclass">The subclass that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        /// <param name="level">The level the feature is available at</param>
        public ClassFeature(Class sourceclass, string name, string description, int level)
        {
            Class = sourceclass;
            FeatureName = name;
            Source = $"{Class.Sourcebook}, Subclass, {Class.ClassName}";
            Description = description;
            Level = level;
        }
    }
    public class SubclassFeature : Feature
    {
        /// <summary>
        /// The subclass that has this feature
        /// </summary>
        [ForeignKey("SubclassId")]
        public Subclass Subclass { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="subclass">The subclass that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        /// <param name="level">The level the feature is available at</param>
        public SubclassFeature(Subclass subclass, string name, string description, int level)
        {
            Subclass = subclass;
            FeatureName = name;
            Source = $"{Subclass.Sourcebook}, Subclass, {Subclass.SubclassName}";
            Description = description;
            Level = level;
        }
    }
    public class RaceFeature : Feature
    {
        /// <summary>
        /// The race that has this feature
        /// </summary>
        [ForeignKey("RaceId")]
        public Race Race { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="race">The race that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        /// <param name="level">The level the feature is available at</param>
        public RaceFeature(Race race, string name, string description, int level)
        {
            Race = race;
            FeatureName = name;
            Source = $"{Race.Sourcebook}, Race, {Race.RaceName}";
            Description = description;
            Level = level;
        }
    }
    public class BackgroundFeature : Feature
    {
        /// <summary>
        /// The background that has this feature
        /// </summary>
        [ForeignKey("BackgroundId")]
        public Background Background { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="background">The background that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        public BackgroundFeature(Background background, string name, string description)
        {
            Background = background;
            FeatureName = name;
            Source = $"{Background.Sourcebook}, Background, {Background.BackgroundName}";
            Description = description;
            Level = 0;
        }
    }

}
