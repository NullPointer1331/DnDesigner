using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
	public class Race
	{
        #region properties
        /// <summary>
        /// The race identifier
        /// </summary>
        [Key]
		public int RaceId { get; set; }

		/// <summary>
		/// The race name
		/// </summary>
		public string Name { get; set; }

		public Source SourceBook { get; set; }

		public string Description { get; set; }

		/// <summary>
		/// The racial stat bonuses
		/// </summary>
		public string StatBonuses { get; set; }

		/// <summary>
		/// The size of the creature
		/// </summary>
		public string Size { get; set; }

		/// <summary>
		/// The speed of the creature
		/// </summary>
		public int Speed { get; set; }

		/// <summary>
		/// Any features provided by the race
		/// </summary>
		public List<RaceFeature> Features { get; set; }
        #endregion

        public Race() {
            Features = new List<RaceFeature>();
			Name = "";
			Description = "";
			StatBonuses = "";
			Size = "";
		}
	}
}
