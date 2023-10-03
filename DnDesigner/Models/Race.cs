namespace DnDesigner.Models
{
	public class Race
	{
		/// <summary>
		/// The race identifier
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The race name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The racial stat bonuses
		/// </summary>
		public List<string> StatBonuses { get; set; }

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
		public List<string> Proficiencies { get; set; }

		/// <summary>
		/// Any features provided by the race
		/// </summary>
		public List<string> Features { get; set; }
	}
}
