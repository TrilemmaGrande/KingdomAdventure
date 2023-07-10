namespace KingdomAdventure.Models.WorldArea
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public Account Account { get; set; }
        List<Item> Items = new List<Item>();
        List<UpgradeItem> UpgradeItems = new List<UpgradeItem>();
    }
}
