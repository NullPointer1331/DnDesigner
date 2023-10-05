using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    public class Inventory
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        int InventoryId { get; set; }

        /// <summary>
        /// The character this inventory belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        /// <summary>
        /// All the items in the inventory
        /// </summary>
        public List<InventoryItem> Items { get; set; }

        /// <summary>
        /// The items currently attuned to the character
        /// </summary>
        public List<InventoryItem> AttunedItems { get; set; }

        /// <summary>
        /// The maximum number of items that can be attuned to the character
        /// </summary>
        public int MaxAttunedItems { get; set; }

        /// <summary>
        /// The item currently equipped in the main hand,
        /// null if no item is equipped
        /// </summary>
        public InventoryItem? MainHand { get; set; }

        /// <summary>
        /// The item currently equipped in the off hand,
        /// null if no item is equipped
        /// </summary>
        public InventoryItem? OffHand { get; set; }

        /// <summary>
        /// The item currently equipped as armor,
        /// null if no item is equipped
        /// </summary>
        public InventoryItem? Armor { get; set; }

        /// <summary>
        /// Items equipped in other slots
        /// </summary>
        public List<InventoryItem> OtherEquippedItems { get; set; }

        /// <summary>
        /// The number of platinum coins in the inventory
        /// </summary>
        public int Platinum { get; set; }

        /// <summary>
        /// The number of gold coins in the inventory
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// The number of electrum coins in the inventory
        /// </summary>
        public int Electrum { get; set; }

        /// <summary>
        /// The number of silver coins in the inventory
        /// </summary>
        public int Silver { get; set; }

        /// <summary>
        /// The number of copper coins in the inventory
        /// </summary>
        public int Copper { get; set; }

        /// <summary>
        /// Minimal constructor, sets Character and initializes empty lists
        /// Sets MaxAttunedItems to 3 and leaves other properties null or 0
        /// </summary>
        /// <param name="character">The character the inventory belongs to</param>
        public Inventory(Character character)
        {
            Character = character;
            Items = new List<InventoryItem>();
            AttunedItems = new List<InventoryItem>();
            MaxAttunedItems = 3;
            OtherEquippedItems = new List<InventoryItem>();
        }
    }
}
