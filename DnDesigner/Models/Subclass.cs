namespace DnDesigner.Models
{
	public class Subclass
	{
		/// <summary>
		/// The subclass identifier
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The name of the subclass
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The fearures of the subclass
		/// </summary>
		public List<string> Features { get; set; }

		/// <summary>
		/// The class the subclass is a subclass of
		/// </summary>
		public Class Class { get; set; }

		/// <summary>
		/// A boolean indicating whether the subclass is a spellcasting subclass
		/// </summary>
		public bool MagicSubclass { get; set; }
	}
}
