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
        public IQueryable<BuildingSoldierProducing> BuildingSoldierProduced { get; }
        public IQueryable<Ressource> Ressources { get; }
        public IQueryable<Soldier> Soldiers { get; }
        public IQueryable<PlayerTown> PlayerTowns { get; }
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
        public void CreateTownValues(PlayerTown town);
        public void UpdateRessources(PlayerTown town);
        public void ConsumeRessources(PlayerTown town);
        public void ProduceRessources(PlayerTown town);
        public void ProduceSoldiers(PlayerTown town);
        public void WorkersConsumeFood(PlayerTown town);
        public void AddBuilding(PlayerTown town, int id);
        public void RemoveBuilding(PlayerTown town, int id);
        public void AddWorkerToBuilding(PlayerTown town, int id);
        public void SubWorkerFromBuilding(PlayerTown town, int id);
        public void UpdatePopulationNotWorking(PlayerTown town);
        public void LevelUpBuilding(PlayerTown town, int id);
        public void IncreaseTownStage(PlayerTown town);
    }
}
