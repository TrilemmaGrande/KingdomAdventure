using KingdomAdventure.Models.TownArea;
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
        public IQueryable<Building> Buildings => ctx.Building;
        public IQueryable<Ressource> Ressources => ctx.Ressource;
        public IQueryable<Soldier> Soldiers => ctx.Soldier;
        public IQueryable<Town> Towns => ctx.Town;
        public IQueryable<TownBuilding> TownBuildings => ctx.TownBuilding;
        public IQueryable<TownRessource> TownRessources => ctx.TownRessource;
        public IQueryable<TownSoldierAttacking> TownSoldiersAttacking => ctx.TownSoldierAttacking;
        public IQueryable<TownSoldierDefending> TownSoldiersDefending => ctx.TownSoldierDefending;
        public IQueryable<EnemyNPC> EnemyNPCs => ctx.EnemyNPC;
        public IQueryable<Inventory> Inventories => ctx.Inventory;
        public IQueryable<InventoryItem> InventoryItems => ctx.InventoryItem;
        public IQueryable<InventoryItemUpgrade> InventoryItemUpgrades => ctx.InventoryItemUpgrade;
        public IQueryable<InventoryUpgrade> InventoryUpgrades => ctx.InventoryUpgrade;
        public IQueryable<Item> Items => ctx.Item;
        public IQueryable<Player> Players => ctx.Player;
        public IQueryable<Upgrade> Upgrades => ctx.Upgrade;

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
                Inventory = inventory
            };
            ctx.Inventory.FirstOrDefault(i => i.InventoryID == inventory.InventoryID).InventoryItems.Add(inventoryItem);
            ctx.SaveChanges();
        }
        public void DeleteInventoryItem(InventoryItem inventoryItem, Inventory inventory)
        {            
            ctx.Inventory.FirstOrDefault(i => i.InventoryID == inventory.InventoryID).InventoryItems.Remove(inventoryItem);
            ctx.SaveChanges();
        }
        public void SaveRepo()
        {
            ctx.SaveChanges();
        }
    }
}
