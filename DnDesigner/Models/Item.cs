﻿using Microsoft.EntityFrameworkCore;
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
        /// The items traits
        /// </summary>
        public string Traits { get; set; } = null!;


    }

    /// <summary>
    /// Represents a single item in an inventory
    /// </summary>
    [PrimaryKey(nameof(ItemId), nameof(InventoryId))]
    public class InventoryItem
    {
        /// <summary>
        /// The item in an inventory
        /// </summary>
        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public int ItemId { get; set; }

        /// <summary>
        /// The inventory the item is in
        /// </summary>
        [ForeignKey("InventoryId")]
        public Inventory Inventory { get; set; }

        public int InventoryId { get; set; }

        /// <summary>
        /// How many of the item are in the inventory
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Is the item equipped
        /// </summary>
        public bool Equipped { get; set; }

        /// <summary>
        /// Which slot the item is equipped in,
        /// 0 = not equipped, 1 = armor, 2 = main hand
        /// 3 = offhand, 5 = other
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
            Equipped = false;
            Attuned = false;
        }
        private InventoryItem() { }
    }
}
