namespace DnDesigner.Models
{
	public class Subclass
	{
		/// <summary>
		/// The subclass identifier
		/// </summary>
		public int SubclassId { get; set; }

		/// <summary>
		/// The name of the subclass
		/// </summary>
		public string SubclassName { get; set; }

		/// <summary>
		/// The source book the subclass is from
		/// </summary>
		public string Sourcebook { get; set; }

		/// <summary>
		/// The features of the subclass
		/// </summary>
		public List<SubclassFeature> Features { get; set; }

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
