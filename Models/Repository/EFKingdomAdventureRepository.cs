using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;

namespace KingdomAdventure.Models.Repository
{
    public class EFKingdomAdventureRepository : IKingdomAdventureRepository
    {
        private KingdomAdventureDBContext ctx;

        public EFKingdomAdventureRepository(KingdomAdventureDBContext ctx)
        {
            this.ctx = ctx;
        }
        public IQueryable<Player> Players => ctx.Player;
        public IQueryable<Inventory> Inventories => ctx.Inventory;
        public IQueryable<InventoryItem> InventoryItems => ctx.InventoryItems;
        public IQueryable<Item> Items => ctx.Items;
        public IQueryable<UpgradeItem> UpgradeItems => ctx.UpgradeItems;
        public IQueryable<EnemyNPC> EnemyNPCs => ctx.EnemyNPCs;

        public void AddPlayer(Player player)
        {
            ctx.Player.Add(player);
            ctx.SaveChanges();
        }
        public void DeletePlayer(Player player)
        {
            ctx.Player.Remove(player);
            ctx.SaveChanges();
        }
        public void AddInventoryItem(Item item, Inventory inventory)
        {
            var inventoryItem = new InventoryItem
            {
                Item = item,
                Inventory = inventory,
                InventoryItemName = item.ItemName
            };
            ctx.Inventory.FirstOrDefault(i => i.InventoryID == inventory.InventoryID).Items.Add(inventoryItem);
            ctx.SaveChanges();
        }
        public void DeleteInventoryItem(InventoryItem inventoryItem, Inventory inventory)
        {            
            ctx.Inventory.FirstOrDefault(i => i.InventoryID == inventory.InventoryID).Items.Remove(inventoryItem);
            ctx.SaveChanges();
        }
    }
}
