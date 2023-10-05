using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents a single item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int ItemId { get; private set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The source book the item is from
        /// </summary>
        public string Sourcebook { get; set; }

        /// <summary>
        /// The items description
        /// </summary>
        public string Description { get; set; }

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
        /// 3 = off hand only, 4 = either hand, 5 = other
        /// </summary>
        [Range(0, 5)]
        public int Equipable { get; set; }

        /// <summary>
        /// Does the item require attunement
        /// </summary>
        public bool Attuneable { get; set; }

        /// <summary>
        /// The items traits
        /// </summary>
        public string Traits { get; set; }


        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="description">The items description</param>
        /// <param name="price">The items cost in gold</param>
        /// <param name="weight">The items weight in pounds</param>
        /// <param name="equipable">Can the item be equipped, if so, where</param>
        /// <param name="attuneable">Can the item be attuned to</param>
        /// <param name="traits">The items traits</param>
        public Item(string name, string description, double price, double weight,
                    int equipable, bool attuneable, string traits)
        {
            Name = name;
            Description = description;
            Price = price;
            Weight = weight;
            Equipable = equipable;
            Attuneable = attuneable;
            Traits = traits;
        }
    }

    /// <summary>
    /// Represents a single item in an inventory
    /// </summary>
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
        bool Equipped { get; set; }

        /// <summary>
        /// Is the item attuned
        /// </summary>
        bool Attuned { get; set; }
        public InventoryItem(Item item, Inventory inventory, int quantity)
        {
            Item = item;
            Inventory = inventory;
            Quantity = quantity;
            Equipped = false;
            Attuned = false;
        }
    }
}
