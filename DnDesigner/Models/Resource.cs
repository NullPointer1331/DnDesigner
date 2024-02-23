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
        /// The amount of the resource restored per long rest, -1 indicates all, -2 indicates half of the max
        /// </summary>
        public int RestoredPerLongRest { get; set; }

        /// <summary>
        /// The amount of the resource restored per short rest, -1 indicates all, -2 indicates half of the max
        /// </summary>
        public int RestoredPerShortRest { get; set; }

        public Resource(string name, int restoredPerShortRest = 0, int restoredPerLongRest = -1 )
        {
            Name = name;
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

        public SpellSlot(string name, int level, bool pactMagic)
            : base(name, pactMagic ? -1 : 0, -1)
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

        public CharacterResource(Character character, Resource resource, int maxAmount)
        {
            Character = character;
            Resource = resource;
            MaxAmount = maxAmount;
            CurrentAmount = maxAmount;
        }

        private CharacterResource() { }

        /// <summary>
        /// Sets the current amount of the resource, clamping it to the max amount
        /// </summary>
        /// <param name="amount">The value to set it to</param>
        public void Set(int amount)
        {
            CurrentAmount = Math.Clamp(amount, 0, MaxAmount);
        }

        /// <summary>
        /// Restores the resource as if the character took a short rest
        /// </summary>
        public void ShortRest()
        {
            if (Resource.RestoredPerShortRest == -1)
            {
                CurrentAmount = MaxAmount;
            }
            else if (Resource.RestoredPerShortRest == -2)
            {
                CurrentAmount += MaxAmount / 2;
            }
            else
            {
                CurrentAmount += Resource.RestoredPerShortRest;
            }
            CurrentAmount = Math.Clamp(CurrentAmount, 0, MaxAmount);
        }

        /// <summary>
        /// Restores the resource as if the character took a long rest
        /// </summary>
        public void LongRest()
        {
            if (Resource.RestoredPerLongRest == -1)
            {
                CurrentAmount = MaxAmount;
            }
            else if (Resource.RestoredPerLongRest == -2)
            {
                CurrentAmount += MaxAmount / 2;
            }
            else
            {
                CurrentAmount += Resource.RestoredPerLongRest;
            }
            CurrentAmount = Math.Clamp(CurrentAmount, 0, MaxAmount);
        }
    }
}
