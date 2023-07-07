namespace KingdomAdventure.Models.WorldArea
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        List<Item> Items = new List<Item>();
    }
}
