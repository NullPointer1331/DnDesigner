namespace DnDesigner.Models
{
	public class Class
	{
		/// <summary>
		/// The class identifier
		/// </summary>
		public int ClassId { get; set; }

		/// <summary>
		/// The name of the class
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// The source book the class is from
		/// </summary>
		public string Sourcebook { get; set; }

		/// <summary>
		/// The features of the class
		/// </summary>
		public List<string> Features { get; set; }

		/// <summary>
		/// The proficiencies that can be learned 
		/// through the class
		/// </summary>
		public List<string> Proficiencies { get; set; }

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
	public class CharacterClass {
		/// <summary>
		/// A class the character has
		/// </summary>
		public Class Class { get; set; }

		/// <summary>
		/// The chosen subclass of the class
		/// </summary>
		public Subclass? Subclass { get; set; }

		/// <summary>
		/// The character the class belongs to
		/// </summary>
		public Character Character { get; set; }

		/// <summary>
		/// How many levels the character has in this class
		/// </summary>
		public int Level { get; set; }
	}
}
