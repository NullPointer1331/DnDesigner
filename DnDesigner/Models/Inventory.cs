using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    public class Inventory
    {
        #region properties
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
        public List<InventoryItem> AttunedItems { get; set; }

        /// <summary>
        /// The maximum number of items that can be attuned to the character
        /// </summary>
        public int MaxAttunedItems { get; set; }

        /// <summary>
        /// The item currently equipped in the main hand,
        /// null if no item is equipped
        /// </summary>
        [NotMapped]
        public InventoryItem? MainHand { get; set; }

        /// <summary>
        /// The item currently equipped in the off hand,
        /// null if no item is equipped
        /// </summary>
        [NotMapped]
        public InventoryItem? OffHand { get; set; }

        /// <summary>
        /// The item currently equipped as armor,
        /// null if no item is equipped
        /// </summary>
        [NotMapped]
        public InventoryItem? Armor { get; set; }

        /// <summary>
        /// Items equipped in other slots
        /// </summary>
        [NotMapped]
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
        #endregion

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
        private Inventory() { }

        #region methods
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
                    Unequip(Armor);
                    item.EquippedIn = 1;
                    Armor = item;
                }
                else if (item.Item.Equipable == 2) // Main hand
                {
                    Unequip(MainHand);
                    item.EquippedIn = 2;
                    MainHand = item;
                }
                else if (item.Item.Equipable == 3) // Offhand
                {
                    Unequip(OffHand);
                    if(MainHand != null && MainHand.Item.Equipable == 5) // If there's a 2 handed weapon equipped
                    {
                        Unequip(MainHand);
                    }
                    item.EquippedIn = 3;
                    OffHand = item;
                }
                else if(item.Item.Equipable == 4) // Either hand
                {
                    if(MainHand == null)
                    {
                        item.EquippedIn = 2;
                        MainHand = item;
                    }
                    else if (OffHand == null)
                    {
                        if (MainHand != null && MainHand.Item.Equipable == 5)
                        {
                            Unequip(MainHand);
                        }
                        item.EquippedIn = 3;
                        OffHand = item;
                    }
                    else //If neither hand is free
                    {
                        if (MainHand.Item.Equipable == 5)
                        {
                            Unequip(MainHand);
                            item.EquippedIn = 2;
                            MainHand = item;
                        }
                        else
                        {
                            Unequip(OffHand);
                            item.EquippedIn = 3;
                            OffHand = item;
                        }
                    }
                }
                else if (item.Item.Equipable == 5) // Both hands
                {
                    Unequip(MainHand);
                    Unequip(OffHand);
                    item.EquippedIn = 2;
                    MainHand = item;
                }
                else
                {
                    OtherEquippedItems.Add(item);
                    item.EquippedIn = 4;
                }
                Attune(item);
            }
            item.ApplyEffect();
        }

        /// <summary>
        /// Unequips an InventoryItem
        /// </summary>
        /// <param name="item">The InventoryItem to unequip</param>
        public void Unequip(InventoryItem? item)
        {
            if(item != null && item.Equipped)
            {
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
                    OtherEquippedItems.Remove(item);
                }
                item.EquippedIn = 0;
                item.RemoveEffect();
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
                AttunedItems.Add(item);
                item.ApplyEffect();
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
                AttunedItems.Remove(item);
                item.RemoveEffect();
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
        /// Simplifies coin values to the smallest number of coins possible ignoring electrum
        /// </summary>
        public void SimplifyCoins()
        {
            SimplifyCoins(TotalCoinValue);
        }

        /// <summary>
        /// Simplifies coin values to the smallest number of coins possible ignoring electrum
        /// while given a total number of copper coins
        /// </summary>
        /// <param name="copper">The total copper value</param>
        public void SimplifyCoins(int copper)
        {
            Platinum = copper / 1000;
            copper -= Platinum * 1000;
            Gold = copper / 100;
            copper -= Gold * 100;
            Silver = copper / 10;
            copper -= Silver * 10;
            Copper = copper;
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
        #endregion
    }
}
