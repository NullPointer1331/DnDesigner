using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    public class Spellcasting
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int SpellcastingId { get; set; }

        /// <summary>
        /// The name of the spellcasting class
        /// </summary>
        public string SpellcastingName { get; set; }

        /// <summary>
        /// Which attribute is used for spellcasting
        /// </summary>
        public string SpellcastingAttribute { get; set; }

        /// <summary>
        /// The type of spellcasting
        /// Full, half, third, pact, or innate
        /// </summary>
        public string SpellcastingType { get; set; }

        /// <summary>
        /// Whether the spellcasting is prepared or not
        /// </summary>
        public bool PreparedCasting { get; set; }

        /// <summary>
        /// Whether or not this gives ritual casting
        /// </summary>
        public bool RitualCasting { get; set; }

        /// <summary>
        /// The spells that can be learned from this spellcasting
        /// </summary>
        public List<LearnableSpell> LearnableSpells { get; set; }
    }
    public class CharacterSpellcasting
    {
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        [ForeignKey("SpellcastingId")]
        public Spellcasting Spellcasting { get; set; }
    }
}
