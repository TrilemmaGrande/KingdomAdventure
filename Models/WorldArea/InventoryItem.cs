namespace KingdomAdventure.Models.WorldArea
{
    public class InventoryItem
    {
        public int InventoryItemID { get; set; }

        // Foreign key
        public int InventoryID { get; set; }

        // Navigation property
        public Inventory Inventory { get; set; }

        // Foreign key
        public int ItemID { get; set; }

        // Navigation property
        public Item Item { get; set; }
    }
}
