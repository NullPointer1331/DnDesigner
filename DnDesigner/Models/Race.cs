using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
	public class Race
	{
		/// <summary>
		/// The race identifier
		/// </summary>
		[Key]
		public int RaceId { get; set; }

		/// <summary>
		/// The race name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The source book the race is from
		/// </summary>
		public string Sourcebook { get; set; }

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
		/// Any proficiencies provided by the race
		/// </summary>
		public List<Proficiency> Proficiencies { get; set; }

		/// <summary>
		/// Any features provided by the race
		/// </summary>
		public List<RaceFeature> Features { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The race name</param>
        /// <param name="stats">The racial stat bonuses</param>
        /// <param name="size">The size of the creature</param>
        /// <param name="speed">The speed of the creature</param>
        /// <param name="proficiencies">Any proficiencies provided by the race</param>
        /// <param name="features">Any features provided by the race</param>
        public Race(string name, string stats, string size, int speed, 
			List<Proficiency> proficiencies, List<RaceFeature> features) {
			Name = name;
			StatBonuses = stats;
			Size = size;
			Speed = speed;
			Proficiencies = proficiencies;
			Features = features;
		}
	}
}
