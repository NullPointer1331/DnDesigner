using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents an expendable resource
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int ResourceId { get; set; }

        /// <summary>
        /// The name of the resource
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The formula for the max amount of the resource
        /// </summary>
        public string? MaxFormula { get; set; }

        /// <summary>
        /// The amount of the resource restored per long rest, -1 indicates half of the max, -2 indicates all
        /// </summary>
        public int RestoredPerLongRest { get; set; }

        /// <summary>
        /// The amount of the resource restored per short rest, -1 indicates half of the max, -2 indicates all
        /// </summary>
        public int RestoredPerShortRest { get; set; }

        public Resource(string name, string? maxFormula, int restoredPerShortRest = 0, int restoredPerLongRest = -2 )
        {
            Name = name;
            MaxFormula = maxFormula;
            RestoredPerLongRest = restoredPerLongRest;
            RestoredPerShortRest = restoredPerShortRest;
        }
    }

    /// <summary>
    /// Represents a spell slot
    /// </summary>
    public class SpellSlot : Resource
    {
        /// <summary>
        /// The level of the spell slot
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Whether the spell slot is a pact magic slot, only used for warlocks
        /// </summary>
        public bool PactMagic { get; set; }

        public SpellSlot(string name, int level, bool pactMagic, string? maxFormula, 
            int restoredPerShortRest = 0, int restoredPerLongRest = -2)
            : base(name, maxFormula, restoredPerShortRest, restoredPerLongRest)
        {
            Level = level;
            PactMagic = pactMagic;
        }
    }

    /// <summary>
    /// Represents the amount of a resource a character has
    /// </summary>
    [PrimaryKey("CharacterId", "ResourceId")]
    public class CharacterResource
    {
        /// <summary>
        /// The character the resource is for
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        /// <summary>
        /// The resource the character has
        /// </summary>
        [ForeignKey("ResourceId")]
        public Resource Resource { get; set; }

        /// <summary>
        /// The maximum amount of the resource the character can have
        /// </summary>
        public int MaxAmount { get; set; }

        /// <summary>
        /// The current amount of the resource the character has
        /// </summary>
        public int CurrentAmount { get; set; }

        public CharacterResource(Character character, Resource resource)
        {
            Character = character;
            Resource = resource;
            int maxAmount = 0;
            if (resource.MaxFormula != null)
            {
                maxAmount = character.Calculate(resource.MaxFormula);
            }
            MaxAmount = maxAmount;
            CurrentAmount = maxAmount;
        }

        public CharacterResource(Character character, Resource resource, int maxAmount)
        {
            Character = character;
            Resource = resource;
            MaxAmount = maxAmount;
            CurrentAmount = maxAmount;
        }

        private CharacterResource() { }
    }
}
