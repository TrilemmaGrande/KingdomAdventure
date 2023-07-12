namespace KingdomAdventure.Models.WorldArea
{
    public class InventoryUpgrade
    {
        public int InventoryUpgradeID { get; set; }

        // Foreign key
        public int InventoryID { get; set; }

        // Navigation property
        public Inventory Inventory { get; set; }

        // Foreign key
        public int UpgradeID { get; set; }

        // Navigation property
        public Upgrade Upgrade { get; set; }
    }
}
