using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
	public class Subclass
	{
		/// <summary>
		/// The subclass identifier
		/// </summary>
		[Key]
		public int SubclassId { get; set; }

		/// <summary>
		/// The name of the subclass
		/// </summary>
		public string Name { get; set; }

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
		[ForeignKey("ClassId")]
		public Class Class { get; set; }

		/// <summary>
		/// The spellcasting abilities of the subclass, null if none
		/// </summary>
		public Spellcasting? Spellcasting { get; set; }

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
        }
	}
}
