using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;

namespace KingdomAdventure.Models.Repository
{
    public interface IKingdomAdventureRepository
    {
        public IQueryable<Building> Buildings { get; }
        public IQueryable<BuildingRessourceConsuming> BuildingRessourceConsumings { get; }
        public IQueryable<BuildingRessourceConsuming> BuildingRessourceConsumed { get; }
        public IQueryable<BuildingRessourceCost> BuildingRessourceCosts { get; }
        public IQueryable<BuildingRessourceProducing> BuildingRessourceProducings { get; }
        public IQueryable<BuildingRessourceProducing> BuildingRessourceProduced { get; }
        public IQueryable<BuildingSoldierProducing> BuildingSoldierProducings { get; }
        public IQueryable<Ressource> Ressources { get; }
        public IQueryable<Soldier> Soldiers { get; }
        public IQueryable<Town> Towns { get; }
        public IQueryable<TownBuilding> TownBuildings { get; }
        public IQueryable<TownRessource> TownRessources { get; }
        public IQueryable<TownSoldier> TownSoldiers { get; }
        public IQueryable<EnemyNPC> EnemyNPCs { get; }
        public IQueryable<Inventory> Inventories { get; }
        public IQueryable<InventoryItem> InventoryItems { get; }
        public IQueryable<InventoryItemUpgrade> InventoryItemUpgrades { get; }
        public IQueryable<InventoryUpgrade> InventoryUpgrades { get; }
        public IQueryable<Item> Items { get; }
        public IQueryable<Player> Players { get; }
        public IQueryable<PlayerEnemyNPC> PlayerEnemyNPCs { get; }
        public IQueryable<Upgrade> Upgrades { get; }

        public void AddPlayer(Player player);
        public void DeletePlayer(Player player);
        public void AddInventoryItem(Item item, Inventory inventory);
        public void DeleteInventoryItem(InventoryItem inventoryItem, Inventory inventory);
        public void CreateTownValues(Town town);
        public void UpdateRessources(Town town);
        public void ConsumeRessources(Town town);
        public void ProduceRessources(Town town);
        public void WorkersConsumeFood(Town town);
        public void AddBuilding(Town town, int id);
        public void AddWorkerToBuilding(Town town, int id);
        public void SubWorkerFromBuilding(Town town, int id);
        public void UpdatePopulationNotWorking(Town town);
        public void LevelUpBuilding(Town town, int id);
    }
}
