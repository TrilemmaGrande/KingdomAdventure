namespace KingdomAdventure.Models.WorldArea
{
    public class Inventory
    {
        public int InventoryID { get; set; }

        // Foreign key
        public int PlayerId { get; set; }

        // Navigation property
        public Player Player { get; set; }
        public List<InventoryItem> Items { get; set; } = new List<InventoryItem>();
        List<UpgradeItem> UpgradeItems = new List<UpgradeItem>();
    }
}
