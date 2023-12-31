﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DnDesigner.Models
{
	public class Subclass
	{
        #region properties
        /// <summary>
        /// The subclass identifier
        /// </summary>
        [Key]
		public int SubclassId { get; set; }

		/// <summary>
		/// The name of the subclass
		/// </summary>
		public string Name { get; set; } = null!;

		/// <summary>
		/// The source book the subclass is from
		/// </summary>
		public string Sourcebook { get; set; } = null!;

		/// <summary>
		/// The features of the subclass
		/// </summary>
		public List<SubclassFeature> Features { get; set; }

		/// <summary>
		/// The class the subclass is a subclass of
		/// </summary>
		[ForeignKey("ClassId")]
		[JsonIgnore]
		public Class Class { get; set; } = null!;

		/// <summary>
		/// The spellcasting abilities of the subclass, null if none
		/// </summary>
		[ForeignKey("SpellcastingId")]
		public Spellcasting? Spellcasting { get; set; }
        #endregion

        /// <summary>
        /// Basic constructor, sets class and name
        /// </summary>
        /// <param name="sourceclass"></param>
        /// <param name="name"></param>
        public Subclass(Class sourceclass, string name)
		{
            Class = sourceclass;
            Name = name;
			Features = new List<SubclassFeature>();
			Spellcasting = null;
			Sourcebook = "";
        }
		private Subclass() // For EF
        {
			Features = new List<SubclassFeature>();
		} 

        /// <summary>
        /// Gets the features available to the subclass at a given level
        /// </summary>
        /// <param name="level">The level </param>
        /// <returns>a list of the features available at that level</returns>
        public List<SubclassFeature> GetAvailableFeatures(int level)
        {
            return Features.Where(Features => Features.Level <= level).ToList();
        }
    }
}
