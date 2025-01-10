using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents a single item
    /// </summary>
    public class Item
    {
        #region properties
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int ItemId { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The source book the item is from
        /// </summary>
        public string Sourcebook { get; set; } = null!;

        /// <summary>
        /// The items description
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// The items cost in gold
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The items weight in pounds
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Can the item be equipped, if so, where
        /// Range guide: 0 = not equipable, 1 = armor, 2 = main hand only
        /// 3 = off hand only, 4 = either hand, 5 = both hands, 6 = other
        /// </summary>
        [Range(0, 6)]
        public int Equipable { get; set; }

        /// <summary>
        /// Does the item require attunement
        /// </summary>
        public bool Attuneable { get; set; }

        /// <summary>
        /// The rarity of the item
        /// Range guide: 0 = nonmagical, 1 = common, 2 = uncommon, 3 = rare
        /// 4 = very rare, 5 = legendary, 6 = artifact, -1 = unknown
        /// </summary>
        [Range(-1, 6)]
        public int Rarity { get; set; }

        /// <summary>
        /// The items traits
        /// </summary>
        public string Traits { get; set; } = null!;

        /// <summary>
        /// The effects the item has on the character
        /// </summary>
        public List<Effect> Effects { get; set; } = null!;
        #endregion

        public Item() {
            Effects = new List<Effect>();
            Name = "";
            Sourcebook = "";
            Description = "";
            Traits = "";
        }
    }

    /// <summary>
    /// Represents a single item in an inventory
    /// </summary>
    [PrimaryKey("ItemId", "InventoryId")]
    public class InventoryItem
    {
        /// <summary>
        /// The item in an inventory
        /// </summary>
        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        /// <summary>
        /// The inventory the item is in
        /// </summary>
        [ForeignKey("InventoryId")]
        public Inventory Inventory { get; set; }

        /// <summary>
        /// How many of the item are in the inventory
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Is the item equipped
        /// </summary>
        public bool Equipped { get { 
                return EquippedIn != 0;
            }}

        /// <summary>
        /// Which slot the item is equipped in,
        /// 0 = not equipped, 1 = armor, 2 = main hand
        /// 3 = offhand, 4 = other
        /// </summary>
        [Range(0, 4)]
        public int EquippedIn {  get; set; }

        /// <summary>
        /// Is the item attuned
        /// </summary>
        public bool Attuned { get; set; }

        public InventoryItem(Item item, Inventory inventory, int quantity)
        {
            Item = item;
            Inventory = inventory;
            Quantity = quantity;
            Attuned = false;
        }
        private InventoryItem() { }

        public void ApplyEffect()
        {
            foreach (Effect effect in Item.Effects)
            {
                CharacterEffect characterEffect = new CharacterEffect(Inventory.Character, effect);
                Inventory.Character.CharacterEffects.Add(characterEffect);
            }
        }
        public void RemoveEffect()
        {
            foreach (Effect effect in Item.Effects)
            {
                CharacterEffect? existingEffect = Inventory.Character.CharacterEffects.Find(e => e.Effect.EffectId == effect.EffectId);
                if (existingEffect != null)
                {
                    existingEffect.RemoveEffect();
                    Inventory.Character.CharacterEffects.Remove(existingEffect);
                }
            }
        }
    }
}
