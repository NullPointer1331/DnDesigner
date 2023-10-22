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
        public int InventoryId { get; set; }

        /// <summary>
        /// The character this inventory belongs to
        /// </summary>
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        /// <summary>
        /// All the items in the inventory
        /// </summary>
        public List<InventoryItem> Items { get; set; } 
        //Also holds equipped and attuned items, as such those don't need to go in the database

        /// <summary>
        /// The items currently attuned to the character
        /// </summary>
        [NotMapped]
        public List<Item> AttunedItems { get; set; }

        /// <summary>
        /// The maximum number of items that can be attuned to the character
        /// </summary>
        public int MaxAttunedItems { get; set; }

        /// <summary>
        /// The item currently equipped in the main hand,
        /// null if no item is equipped
        /// </summary>
        [NotMapped]
        public Item? MainHand { get; set; }

        /// <summary>
        /// The item currently equipped in the off hand,
        /// null if no item is equipped
        /// </summary>
        [NotMapped]
        public Item? OffHand { get; set; }

        /// <summary>
        /// The item currently equipped as armor,
        /// null if no item is equipped
        /// </summary>
        [NotMapped]
        public Item? Armor { get; set; }

        /// <summary>
        /// Items equipped in other slots
        /// </summary>
        [NotMapped]
        public List<Item> OtherEquippedItems { get; set; }

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

        public int TotalCoinValue
        {
            get
            {
                int total = 0;
                total += Platinum * 1000;
                total += Gold * 100;
                total += Electrum * 50;
                total += Silver * 10;
                total += Copper;
                return total;
            }
        }

        /// <summary>
        /// Minimal constructor, sets Character and initializes empty lists
        /// Sets MaxAttunedItems to 3 and leaves other properties null or 0
        /// </summary>
        /// <param name="character">The character the inventory belongs to</param>
        public Inventory(Character character)
        {
            Character = character;
            Items = new List<InventoryItem>();
            AttunedItems = new List<Item>();
            MaxAttunedItems = 3;
            OtherEquippedItems = new List<Item>();
        }
        private Inventory() { }

        /// <summary>
        /// Populates the equipment slots with the items that are equipped or attuned
        /// </summary>
        public void PopulateEquipmentSlots()
        {
            foreach(InventoryItem item in Items)
            {
                if(item.Equipped)
                {
                    Equip(item);
                }
                else if(item.Attuned)
                {
                    Attune(item);
                }
            }
        }

        /// <summary>
        /// Equips an item to the appropriate slot
        /// </summary>
        /// <param name="item">The InventoryItem to equip</param>
        public void Equip(InventoryItem item)
        {
            if(item.Item.Equipable != 0)
            {
                if(item.Item.Equipable == 1) // Armor
                {
                    Unequip(FindItem(Armor));
                    item.EquippedIn = 1;
                    Armor = item.Item;
                }
                else if (item.Item.Equipable == 2) // Main hand
                {
                    Unequip(FindItem(MainHand));
                    item.EquippedIn = 2;
                    MainHand = item.Item;
                }
                else if (item.Item.Equipable == 3) // Offhand
                {
                    Unequip(FindItem(OffHand));
                    if(MainHand != null && MainHand.Equipable == 5) // If there's a 2 handed weapon equipped
                    {
                        Unequip(FindItem(MainHand));
                    }
                    item.EquippedIn = 3;
                    OffHand = item.Item;
                }
                else if(item.Item.Equipable == 4) // Either hand
                {
                    if(MainHand == null)
                    {
                        item.EquippedIn = 2;
                        MainHand = item.Item;
                    }
                    else if (OffHand == null)
                    {
                        if (MainHand != null && MainHand.Equipable == 5)
                        {
                            Unequip(FindItem(MainHand));
                        }
                        item.EquippedIn = 3;
                        OffHand = item.Item;
                    }
                    else //If neither hand is free
                    {
                        if (MainHand.Equipable == 5)
                        {
                            Unequip(FindItem(MainHand));
                            item.EquippedIn = 2;
                            MainHand = item.Item;
                        }
                        else
                        {
                            Unequip(FindItem(OffHand));
                            item.EquippedIn = 3;
                            OffHand = item.Item;
                        }
                    }
                }
                else if (item.Item.Equipable == 5) // Both hands
                {
                    Unequip(FindItem(MainHand));
                    Unequip(FindItem(OffHand));
                    item.EquippedIn = 2;
                    MainHand = item.Item;
                }
                else
                {
                    OtherEquippedItems.Add(item.Item);
                    item.EquippedIn = 4;
                }
                item.Equipped = true;
                Attune(item);
            }
        }

        /// <summary>
        /// Unequips an InventoryItem
        /// </summary>
        /// <param name="item">The InventoryItem to unequip</param>
        public void Unequip(InventoryItem? item)
        {
            if(item.Equipped && item != null)
            {
                item.Equipped = false;
                Unattune(item);
                if (item.EquippedIn == 1)
                {
                    Armor = null;
                }
                else if (item.EquippedIn == 2)
                {
                    MainHand = null;
                }
                else if (item.EquippedIn == 3)
                {
                    OffHand = null;
                }
                else
                {
                    OtherEquippedItems.Remove(item.Item);
                }
                item.EquippedIn = 0;
            }
        }

        /// <summary>
        /// Attunes an item to a character
        /// </summary>
        /// <param name="item">The InventoryItem to attune</param>
        public void Attune(InventoryItem item) 
        {
            if(item.Item.Attuneable && AttunedItems.Count < MaxAttunedItems)
            {
                item.Attuned = true;
                AttunedItems.Add(item.Item);
            }
        }

        /// <summary>
        /// Unattunes an item to a character
        /// </summary>
        /// <param name="item">The InventoryItem to unattune</param>
        public void Unattune(InventoryItem item)
        {
            if(item.Attuned)
            {
                item.Attuned = false;
                AttunedItems.Remove(item.Item);
            }
        }

        /// <summary>
        /// Finds an InventoryItem in this inventory with the specified Item
        /// </summary>
        /// <param name="item">The item to find in the inventory</param>
        /// <returns>The InventoryItem containting the item, null if none</returns>
        public InventoryItem? FindItem(Item? item)
        {
            return Items.Where(i => i.Item.Equals(item)).FirstOrDefault();
        }

        /// <summary>
        /// Simplifies coin values to the smallest number of coins possible 
        /// ignoring electrum
        /// </summary>
        public void SimplifyCoins()
        {
            int total = TotalCoinValue;
            Platinum = total / 1000;
            total -= Platinum * 1000;
            Gold = total / 100;
            total -= Gold * 100;
            Silver = total / 10;
            total -= Silver * 10;
            Copper = total;
        }

        /// <summary>
        /// Adds a specified quantity of an item to the inventory
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="quantity">How many of the item</param>
        public void AddItem(Item item, int quantity)
        {
            InventoryItem? inventoryItem = FindItem(item);
            if(inventoryItem != null)
            {
                inventoryItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new InventoryItem(item, this, quantity));
            }
        }

        /// <summary>
        /// Adds one of an item to the inventory
        /// </summary>
        /// <param name="item">The item to add</param>
        public void AddItem(Item item)
        {
            AddItem(item, 1);
        }

        /// <summary>
        /// Removes a specified quantity of an item from the inventory
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <param name="quantity">How many of that item to remove</param>
        public void RemoveItem(Item item, int quantity)
        {
            InventoryItem? inventoryItem = FindItem(item);
            if (inventoryItem != null)
            {
                inventoryItem.Quantity -= quantity;
                if (inventoryItem.Quantity <= 0)
                {
                    Unequip(inventoryItem);
                    Unattune(inventoryItem);
                    Items.Remove(inventoryItem);
                }
            }
        }

        /// <summary>
        /// Removes one of an item from the inventory
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void RemoveItem(Item item)
        {
            RemoveItem(item, 1);
        }
    }
}
