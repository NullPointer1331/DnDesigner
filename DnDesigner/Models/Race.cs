namespace DnDesigner.Models
{
	public class Race
	{
		/// <summary>
		/// The race identifier
		/// </summary>
		public int RaceId { get; set; }

		/// <summary>
		/// The race name
		/// </summary>
		public string RaceName { get; set; }

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
	}
}
