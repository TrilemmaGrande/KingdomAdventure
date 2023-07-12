namespace KingdomAdventure.Models.WorldArea
{
    public class InventoryItemUpgrade
    {
        public int InventoryItemUpgradeID { get; set; }

        // Foreign key
        public int InventoryItemID { get; set; }

        // Navigation property
        public InventoryItem InventoryItem { get; set; }

        // Foreign key
        public int UpgradeID { get; set; }

        // Navigation property
        public Upgrade Upgrade { get; set; }
    }
}
