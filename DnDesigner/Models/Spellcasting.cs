using Microsoft.EntityFrameworkCore;
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
        /// The name of the class or subclass that gives this spellcasting
        /// </summary>
        public string Name { get; set; }

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
        /// Whether this spellcasting uses a spellbook
        /// </summary>
        public bool Spellbook { get; set; }

        /// <summary>
        /// The spells that can be learned from this spellcasting
        /// </summary>
        public List<LearnableSpell> LearnableSpells { get; set; }
    }
    [PrimaryKey(nameof(CharacterId), nameof(SpellcastingId))]
    public class CharacterSpellcasting
    {
        /// <summary>
        /// The character this spellcasting belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        int CharacterId { get; set; }

        /// <summary>
        /// The spellcasting this is referencing
        /// </summary>
        [ForeignKey("SpellcastingId")]
        public Spellcasting Spellcasting { get; set; }

        int SpellcastingId { get; set; }

        /// <summary>
        /// The spells this character currently has prepared
        /// or if they aren't a prepared caster, the spells they know
        /// </summary>
        public List<KnownSpell> PreparedSpells { get; set; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="character">The character this spellcasting belongs to</param>
        /// <param name="spellcasting">The spellcasting this is referencing</param>
        public CharacterSpellcasting(Character character, Spellcasting spellcasting)
        {
            Character = character;
            Spellcasting = spellcasting;
            PreparedSpells = new List<KnownSpell>();
        }

        private CharacterSpellcasting() { }

        //Spellcasting level, spell attack bonus, and spell save DC will need to be calculated but don't need to be stored
        //So we will add methods to calculate them later
    }
}
