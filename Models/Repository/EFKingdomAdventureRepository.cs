using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IQueryable<BuildingRessourceConsuming> BuildingRessourceConsumings => ctx.BuildingRessourceConsuming;
        public IQueryable<BuildingRessourceCost> BuildingRessourceCosts => ctx.BuildingRessourceCost;
        public IQueryable<BuildingRessourceProducing> BuildingRessourceProducings => ctx.BuildingRessourceProducing;
        public IQueryable<BuildingSoldierProducing> BuildingSoldierProducings => ctx.BuildingSoldierProducing;
        public IQueryable<Ressource> Ressources => ctx.Ressource;
        public IQueryable<Soldier> Soldiers => ctx.Soldier;
        public IQueryable<Town> Towns => ctx.Town;
        public IQueryable<TownBuilding> TownBuildings => ctx.TownBuilding;
        public IQueryable<TownRessource> TownRessources => ctx.TownRessource;
        public IQueryable<TownSoldier> TownSoldiers => ctx.TownSoldier;
        public IQueryable<EnemyNPC> EnemyNPCs => ctx.EnemyNPC;
        public IQueryable<Inventory> Inventories => ctx.Inventory;
        public IQueryable<InventoryItem> InventoryItems => ctx.InventoryItem;
        public IQueryable<InventoryItemUpgrade> InventoryItemUpgrades => ctx.InventoryItemUpgrade;
        public IQueryable<InventoryUpgrade> InventoryUpgrades => ctx.InventoryUpgrade;
        public IQueryable<Item> Items => ctx.Item;
        public IQueryable<Player> Players => ctx.Player;
        public IQueryable<PlayerEnemyNPC> PlayerEnemyNPCs => ctx.PlayerEnemyNPC;
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

        public void CreateTownValues(Town town)
        {
            foreach (var item in Ressources)
            {
                town.TownRessources.Add(

                    new TownRessource()
                    {
                        Town = town,
                        Ressource = item,
                        Amount = item.RessourceName == "Wood" ? 5 : 0
                    });
            }
            foreach (var building in Buildings)
            {
                town.TownBuildings.Add(
                    new TownBuilding()
                    {
                        Town = town,
                        Building = building,
                        Level = 1,
                        Amount = 0
                    });
            }
            town.TownBuildings.FirstOrDefault(n => n.Building.BuildingName == "Tent").Amount = 1;
            town.TownRessources.FirstOrDefault(r => r.Ressource.RessourceName == "PopulationMax").Amount = 1;
            town.TownRessources.FirstOrDefault(r => r.Ressource.RessourceName == "Storage").Amount = 10;

            ctx.SaveChanges();
            
        }
        public void IncrementRessources(Town town)
        {
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeElapsed = currentTime - town.LastUpdated;
            int incrementAmount = (int)timeElapsed.TotalSeconds;
            var producingBuildings = town.TownBuildings.Where(i => i.Amount > 0);
            foreach (var producingBuilding in producingBuildings)
            {
                foreach (var producedRessource in producingBuilding.Building.ProducingRessources)
                {
                    double producedInSeconds = (double)producedRessource.Amount / 60 * (double)producingBuilding.Amount;
                    Math.Floor(producedInSeconds);
                    if (!producedRessource.ProduceOnce)
                    {
                        int oldTownRessourceValue = town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).Amount;
                        int storageValue = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Storage").Amount;
                        int newTownRessourceValue = oldTownRessourceValue + (int)(producedInSeconds * incrementAmount);
                        if (newTownRessourceValue < storageValue)
                        {
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).Amount = newTownRessourceValue;
                        }
                        else
                        {
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).Amount = storageValue;
                        }
                    }
                }                
            }
            town.LastUpdated = DateTime.UtcNow;
            ctx.SaveChanges();
        }
        public void AddBuilding(Town town, int id)
        {
            var building = town.TownBuildings.FirstOrDefault(n => n.BuildingID == id);
            building.Amount += 1;
            foreach (var ressource in building.Building.BuildingRessourcesCosts)
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).Amount -=
                    ressource.Amount;
            }
            foreach (var ressource in building.Building.ProducingRessources)
            {
                if (ressource.ProduceOnce)
                {
                    town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).Amount +=
                        ressource.Amount;
                }
            }
            town.PopulationUsed += building.Building.Population;
            ctx.SaveChanges();
        }
    }
}
