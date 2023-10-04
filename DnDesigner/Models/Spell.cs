using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Spell
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int SpellId { get; set; }

        /// <summary>
        /// The name of the spell
        /// </summary>
        public string SpellName { get; set; }

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
        public string SpellDescription { get; set; }

        /// <summary>
        /// Who can learn the spell
        /// </summary>
        public List<LearnableSpell> LearnedBy { get; set; }

        /// <summary>
        /// Minimal constructor, Initializes lists and leaves everything else blank
        /// </summary>
        public Spell()
        {
            LearnedBy = new List<LearnableSpell>();
        }
    }

    /// <summary>
    /// Shows which classes can learn which spells
    /// </summary>
    public class LearnableSpell
    {
        /// <summary>
        /// A spell that can be learned by a class
        /// </summary>
        [ForeignKey("SpellId")]
        public Spell Spell { get; set; }

        /// <summary>
        /// A class that can learn a spell
        /// </summary>
        [ForeignKey("SpellcastingId")]
        public Spellcasting Spellcasting { get; set; }

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
    }
}
