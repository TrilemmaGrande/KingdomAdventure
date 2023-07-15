﻿using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
                        Amount = 0,
                        ProducedBetweenInterval = 0
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
            town.TownRessources.FirstOrDefault(r => r.Ressource.RessourceName == "Wood").Amount = 10;

            ctx.SaveChanges();

        }
        public void IncrementRessources(Town town)
        {
       
            var producingBuildings = town.TownBuildings.Where(i => i.Amount > 0);
            //increase Ressources in Town for every producing Building
            foreach (var producingBuilding in producingBuildings)
            {
                foreach (var producedRessource in producingBuilding.Building.ProducingRessources)
                {
                    DateTime currentTime = DateTime.UtcNow;
                    TimeSpan timeElapsed = currentTime - town.LastUpdated;
                    double timeElapsedInMilSeconds = timeElapsed.TotalMilliseconds;
                    const int minuteToMilSeconds = 60000;
                    if (!producedRessource.ProduceOnce)
                    {
                        double producedInMilSeconds = (double)producedRessource.Amount / minuteToMilSeconds * (double)producingBuilding.Amount;
                        double restOfLastInterval = town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).ProducedBetweenInterval;
                        double producedInInterval = producedInMilSeconds * timeElapsedInMilSeconds;
                      
                        if (Math.Floor(producedInInterval + restOfLastInterval) < 1)
                        {
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).ProducedBetweenInterval
                                += producedInInterval;
                        }
                        else
                        {
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).ProducedBetweenInterval
                                = producedInInterval + restOfLastInterval - Math.Floor(producedInInterval + restOfLastInterval);

                            int oldTownRessourceValue = town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).Amount;
                            int storageValue = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Storage").Amount;
                            int newTownRessourceValue = oldTownRessourceValue + (int)Math.Floor(producedInInterval + restOfLastInterval);
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
            }
            DecrementRessources(town);
            town.LastUpdated = DateTime.UtcNow;
            ctx.SaveChanges();
        }
        public void DecrementRessources(Town town)
        {
            //decrease Food in Town for every Person
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeElapsed = currentTime - town.LastUpdated;
            double timeElapsedInMilSeconds = timeElapsed.TotalMilliseconds;
            const int minuteToMilSeconds = 60000;
            int peopleInTown = town.PopulationUsed;
            double restOfLastInterval = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").ProducedBetweenInterval;
            double consumedInMilSecond = (double)peopleInTown / minuteToMilSeconds;
            double consumedInInterval = consumedInMilSecond * timeElapsedInMilSeconds;

            if (Math.Floor(consumedInInterval - restOfLastInterval) < 1)
            {
                town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").ProducedBetweenInterval
                    -= consumedInInterval;
            }
            else
            {
                town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").ProducedBetweenInterval
                    = consumedInInterval - restOfLastInterval - Math.Floor(consumedInInterval - restOfLastInterval);

                int oldFoodValue = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").Amount;
                int newFoodValue = oldFoodValue - (int)Math.Floor(consumedInInterval - restOfLastInterval);
                town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").Amount = newFoodValue;
            }
        }
        public void AddBuilding(Town town, int id)
        {
            IncrementRessources(town);
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
