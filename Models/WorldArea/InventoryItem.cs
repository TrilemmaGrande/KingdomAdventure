namespace KingdomAdventure.Models.WorldArea
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }
        public string Name { get; set; }

        // Foreign key
        public int InventoryId { get; set; }

        // Navigation property
        public Inventory Inventory { get; set; }

        // Foreign key
        public int ItemId { get; set; }

        // Navigation property
        public Item Item { get; set; }
    }
}
