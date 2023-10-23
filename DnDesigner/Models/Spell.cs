using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Spell
    {
        #region properties
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int SpellId { get; set; }

        /// <summary>
        /// The name of the spell
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The source book the spell is from
        /// </summary>
        public string Sourcebook { get; set; }

        /// <summary>
        /// The base level of the spell,
        /// level 0 spells are cantrips
        /// </summary>
        [Range(0, 9)]
        public int SpellLevel { get; set; }

        /// <summary>
        /// The school of magic the spell belongs to
        /// </summary>
        public string SpellSchool { get; set; }

        /// <summary>
        /// The time it takes to cast the spell
        /// Can be an action, bonus action, reaction, minutes, or hours
        /// </summary>
        public string CastingTime { get; set; }

        /// <summary>
        /// The range of the spell
        /// </summary>
        public string Range { get; set; }

        /// <summary>
        /// The components required to cast the spell
        /// Can be verbal, somatic, material (with materials specified), or a combination of the three
        /// </summary>
        public string Components { get; set; }

        /// <summary>
        /// How long the spell lasts
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// The description of the spell
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Can the spell be cast as a ritual
        /// </summary>
        public bool IsRitual { get; set; }

        /// <summary>
        /// Does the spell require concentration
        /// </summary>
        public bool RequiresConcentration { get; set; }

        /// <summary>
        /// Who can learn the spell
        /// </summary>
        public List<LearnableSpell> LearnedBy { get; set; }
        #endregion

        /// <summary>
        /// Minimal constructor, Initializes lists and leaves everything else blank
        /// </summary>
        public Spell()
        {
            LearnedBy = new List<LearnableSpell>();
            Name = "";
            Sourcebook = "";
            SpellSchool = "";
            CastingTime = "";
            Range = "";
            Components = "";
            Duration = "";
            Description = "";
        }
    }

    /// <summary>
    /// Shows which classes can learn which spells
    /// </summary>
    [PrimaryKey(nameof(SpellId), nameof(SpellcastingId))]
    public class LearnableSpell
    {
        /// <summary>
        /// A spell that can be learned by a class
        /// </summary>
        [ForeignKey("SpellId")]
        public Spell Spell { get; set; }

        int SpellId { get; set; }

        /// <summary>
        /// A class that can learn a spell
        /// </summary>
        [ForeignKey("SpellcastingId")]
        public Spellcasting Spellcasting { get; set; }

        int SpellcastingId { get; set; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="spell">A spell that can be learned by a class</param>
        /// <param name="spellcasting">A class that can learn a spell</param>
        public LearnableSpell(Spell spell, Spellcasting spellcasting)
        {
            Spell = spell;
            Spellcasting = spellcasting;
        }
        private LearnableSpell() { }
    }
    [PrimaryKey(nameof(CharacterId), nameof(SpellId))]
    public class KnownSpell
    {
        /// <summary>
        /// The character who knows this spell
        /// </summary>
        [ForeignKey("CharacterId, SpellcastingId")]
        public CharacterSpellcasting Character { get; set; }

        int CharacterId { get; set; }

        int SpellcastingId { get; set; }

        /// <summary>
        /// The spell the character knows
        /// </summary>
        [ForeignKey("SpellId")]
        public Spell Spell { get; set; }

        int SpellId { get; set; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="Character">The character who knows this spell</param>
        /// <param name="spell">The spell the character knows</param>
        public KnownSpell(CharacterSpellcasting Character, Spell spell)
        {
            this.Character = Character;
            Spell = spell;
        }
        private KnownSpell() { }
    }
}
