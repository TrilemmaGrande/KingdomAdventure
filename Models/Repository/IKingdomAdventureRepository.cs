using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;

namespace KingdomAdventure.Models.Repository
{
    public interface IKingdomAdventureRepository
    {
        public IQueryable<Player> Players { get; }
        public IQueryable<Inventory> Inventories { get; }
        public IQueryable<InventoryItem> InventoryItems { get; }
        public IQueryable<Item> Items { get; }
        public IQueryable<UpgradeItem> UpgradeItems { get; }
        public IQueryable<EnemyNPC> EnemyNPCs { get; }
        public void AddPlayer(Player player);
        public void DeletePlayer(Player player);
        public void AddInventoryItem(Item item, Inventory inventory);
        public void DeleteInventoryItem(InventoryItem inventoryItem, Inventory inventory);
    }
}
