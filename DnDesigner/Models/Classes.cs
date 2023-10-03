namespace DnDesigner.Models
{
	public class Classes
	{
		/// <summary>
		/// The name of the class
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The features of the class
		/// </summary>
		public List<String> Features { get; set; }

		/// <summary>
		/// The proficiencies that can be learned 
		/// through the class
		/// </summary>
		public List<String> Proficiencies { get; set; }

		/// <summary>
		/// The hit die type of the class
		/// </summary>
		public int HitDie { get; set; }

		/// <summary>
		/// The spellcasting type of the class. 
		/// Can be full, half or none
		/// </summary>
		public string SpellcastingType { get; set; }

		/// <summary>
		/// The spellcasting ability of the class.
		/// (i.e. Wisdom for Clerics)
		/// </summary>
		public string SpellcastingAbility { get; set; }

		/// <summary>
		/// The list of subclasses for the class.
		/// </summary>
		// TODO: Make the Subclass class
		public List<Subclass> Subclasses { get; set; }
	}
}
