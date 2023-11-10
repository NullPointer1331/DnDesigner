using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    public class Spellcasting
    {
        #region properties
        /// <summary>
        /// The standard spell progression for full spellcasters
        /// </summary>
        public static int[,] StandardSpellProgression = new int[20, 9]
        {
            {2, 0, 0, 0, 0, 0, 0, 0, 0},
            {3, 0, 0, 0, 0, 0, 0, 0, 0},
            {4, 2, 0, 0, 0, 0, 0, 0, 0},
            {4, 3, 0, 0, 0, 0, 0, 0, 0},
            {4, 3, 2, 0, 0, 0, 0, 0, 0},
            {4, 3, 3, 0, 0, 0, 0, 0, 0},
            {4, 3, 3, 1, 0, 0, 0, 0, 0},
            {4, 3, 3, 2, 0, 0, 0, 0, 0},
            {4, 3, 3, 3, 1, 0, 0, 0, 0},
            {4, 3, 3, 3, 2, 0, 0, 0, 0},
            {4, 3, 3, 3, 2, 1, 0, 0, 0},
            {4, 3, 3, 3, 2, 1, 0, 0, 0},
            {4, 3, 3, 3, 2, 1, 1, 0, 0},
            {4, 3, 3, 3, 2, 1, 1, 0, 0},
            {4, 3, 3, 3, 2, 1, 1, 1, 0},
            {4, 3, 3, 3, 2, 1, 1, 1, 0},
            {4, 3, 3, 3, 2, 1, 1, 1, 1},
            {4, 3, 3, 3, 3, 1, 1, 1, 1},
            {4, 3, 3, 3, 3, 2, 1, 1, 1},
            {4, 3, 3, 3, 3, 2, 2, 1, 1}
        };

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
        /// full, half, third, pact, or innate
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
        #endregion

    }

    [PrimaryKey("CharacterId", "SpellcastingId")]
    public class CharacterSpellcasting
    {
        #region properties
        /// <summary>
        /// The character this spellcasting belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; } = null!;

        /// <summary>
        /// The spellcasting this is referencing
        /// </summary>
        [ForeignKey("SpellcastingId")]
        public Spellcasting Spellcasting { get; set; } = null!;

        /// <summary>
        /// The total spellcasting level of this character 
        /// for the purposes of calculating spell slots
        /// </summary>
        public int TotalSpellcastingLevel { get
            {
                int level = 0;
                foreach (CharacterClass characterClass in Character.Classes)
                {
                    Spellcasting? spellcasting = null;
                    if(characterClass.Class.Spellcasting != null)
                    {
                        spellcasting = characterClass.Class.Spellcasting;
                        
                    }
                    else if(characterClass.Subclass != null && characterClass.Subclass.Spellcasting != null)
                    {
                        spellcasting = characterClass.Subclass.Spellcasting;
                    }
                    if(spellcasting != null)
                    {
                        if (spellcasting.SpellcastingType == "full")
                        {
                            level += characterClass.Level;
                        }
                        else if (spellcasting.SpellcastingType == "half")
                        {
                            level += characterClass.Level / 2;
                        }
                        else if (spellcasting.SpellcastingType == "third")
                        {
                            level += characterClass.Level / 3;
                        }
                    }
                }
                return level;
            }}

        /// <summary>
        /// The bonus to spell attack rolls
        /// </summary>
        public int SpellAttackBonus { get { 
                return Character.GetModifier(Spellcasting.SpellcastingAttribute) + Character.ProficiencyBonus;
            }}

        /// <summary>
        /// The difficulty class for spells
        /// </summary>
        public int SpellSaveDC { get
            {
                return 8 + SpellAttackBonus;
            }}

        /// <summary>
        /// The spells this character currently has prepared
        /// or if they aren't a prepared caster, the spells they know
        /// </summary>
        public List<KnownSpell> PreparedSpells { get; set; }
        #endregion

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

        private CharacterSpellcasting() // For EF
        {
            PreparedSpells = new List<KnownSpell>();
        }

    }
}
