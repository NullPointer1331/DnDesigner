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
		/// Any proficiencies provided by the race
		/// </summary>
		public List<RaceProficiency> Proficiencies { get; set; }

		/// <summary>
		/// Any features provided by the race
		/// </summary>
		public List<RaceFeature> Features { get; set; }


		public Race() {
			Proficiencies = new List<RaceProficiency>();
            Features = new List<RaceFeature>();
		}
	}
}
