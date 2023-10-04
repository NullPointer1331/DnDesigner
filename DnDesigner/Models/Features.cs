﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

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


        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The name of the feature</param>
        /// <param name="source">The source of the feature</param>
        /// <param name="description">The feature's description</param>
        /// <param name="level">The level the feature is available at</param>
        public Features(string name, string source, string description, int level)
        {
            Name = name;
            Source = source;
            Description = description;
            Level = level;
        }
    }
}
