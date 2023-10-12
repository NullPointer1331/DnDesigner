using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
	public class Class
	{
		/// <summary>
		/// The class identifier
		/// </summary>
		[Key]
		public int ClassId { get; set; }

		/// <summary>
		/// The name of the class
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The source book the class is from
		/// </summary>
		public string Sourcebook { get; set; }

        /// <summary>
        /// The hit die type of the class
        /// </summary>
        public int HitDie { get; set; }

        /// <summary>
        /// The features of the class
        /// </summary>
        public List<ClassFeature> Features { get; set; }

		/// <summary>
		/// The proficiencies that can be learned 
		/// through the class
		/// </summary>
		public List<ClassProficiency> Proficiencies { get; set; }

		/// <summary>
		/// The spellcasting abilities of the class, null if none
		/// </summary>
		[ForeignKey("SpellcastingId")]
		public Spellcasting? Spellcasting { get; set; }

		/// <summary>
		/// The list of subclasses for the class.
		/// </summary>
		public List<Subclass> Subclasses { get; set; }

		public Class() {
			Features = new List<ClassFeature>();
            Proficiencies = new List<ClassProficiency>();
            Subclasses = new List<Subclass>();
		}
	}

	[PrimaryKey(nameof(CharacterId), nameof(ClassId))]
	public class CharacterClass {
		/// <summary>
		/// A class the character has
		/// </summary>
		[ForeignKey("ClassId")]
		public Class Class { get; set; }

		int ClassId { get; set; }

		/// <summary>
		/// The chosen subclass of the class
		/// </summary>
		[ForeignKey("SubclassId")]
		public Subclass? Subclass { get; set; }

		int? SubclassId { get; set; }

		/// <summary>
		/// The character the class belongs to
		/// </summary>
		[ForeignKey("CharacterId")]
		public Character Character { get; set; }

		int CharacterId { get; set; }

		/// <summary>
		/// How many levels the character has in this class
		/// </summary>
		public int Level { get; set; }

        /// <summary>
        /// Basic constructor, sets class, character, and level
        /// For use when there is no subclass
        /// </summary>
        /// <param name="sourceclass">A class the character has</param>
        /// <param name="character">The character the class belongs to</param>
        /// <param name="level">How many levels the character has in this class</param>
        public CharacterClass(Class sourceclass, Character character, int level)
		{
            Class = sourceclass;
            Character = character;
            Level = level;
        }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="sourceclass">A class the character has</param>
        /// <param name="subclass">The chosen subclass of the class</param>
        /// <param name="character">The character the class belongs to</param>
        /// <param name="level">How many levels the character has in this class</param>
        public CharacterClass(Class sourceclass, Subclass subclass, Character character, int level)
        {
            Class = sourceclass;
			Subclass = subclass;
            Character = character;
            Level = level;
        }

		private CharacterClass() { }
    }
}
