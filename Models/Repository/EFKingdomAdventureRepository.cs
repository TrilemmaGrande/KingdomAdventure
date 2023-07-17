using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var firstBuilding = Buildings.FirstOrDefault(n => n.BuildingName == "Tent");
            town.TownBuildings.Add(
                new TownBuilding()
                {
                    Town = town,
                    Building = firstBuilding,
                    Level = 1,
                    WorkersMax = firstBuilding.WorkersMaxTemplate
                });

            town.TownRessources.FirstOrDefault(r => r.Ressource.RessourceName == "PopulationMax").Amount = 2;
            town.TownRessources.FirstOrDefault(r => r.Ressource.RessourceName == "Storage").Amount = 20;
            town.TownRessources.FirstOrDefault(r => r.Ressource.RessourceName == "Wood").Amount = 15;
            town.TownRessources.FirstOrDefault(r => r.Ressource.RessourceName == "Food").Amount = 20;
            town.PopulationNotWorking = 2;

            ctx.SaveChanges();

        }
        public void UpdateRessources(Town town)
        {
            ConsumeRessources(town);
            ProduceRessources(town);
            WorkersConsumeFood(town);
            town.LastUpdated = DateTime.UtcNow;
            ctx.SaveChanges();
        }
        public void ConsumeRessources(Town town)
        {
            var consumingBuildings = town.TownBuildings;
            foreach (var consumingBuilding in consumingBuildings)
            {
                foreach (var consumedRessource in consumingBuilding.Building.ConsumingRessources)
                {
                    DateTime currentTime = DateTime.UtcNow;
                    TimeSpan timeElapsed = currentTime - town.LastUpdated;
                    double timeElapsedInMilSeconds = timeElapsed.TotalMilliseconds;
                    const int minuteToMilSeconds = 60000;

                    double consumedInMilSeconds = (double)consumedRessource.Amount / minuteToMilSeconds;
                    double restOfLastInterval = town.TownRessources.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).ProducedBetweenInterval;
                    double consumedInInterval = consumedInMilSeconds * timeElapsedInMilSeconds;

                    if (Math.Floor(consumedInInterval - restOfLastInterval) < 1)
                    {
                        town.TownRessources.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).ProducedBetweenInterval
                            -= consumedInInterval;
                    }
                    else
                    {
                        town.TownRessources.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).ProducedBetweenInterval
                            = consumedInInterval - restOfLastInterval - Math.Floor(consumedInInterval - restOfLastInterval);

                        int oldTownRessourceValue = town.TownRessources.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).Amount;
                        int storageValue = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Storage").Amount;
                        int newTownRessourceValue = oldTownRessourceValue - (int)Math.Floor(consumedInInterval - restOfLastInterval);

                        if (newTownRessourceValue > 0)
                        {
                            // decrease TownRessource
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).Amount = newTownRessourceValue;
                            // increase BuildingRessource
                            consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).Amount
                                = newTownRessourceValue - oldTownRessourceValue;
                        }
                        else
                        {
                            // consume Rest of TownRessource
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).Amount = 0;
                            // increase BuildingRessource with Rest of TownRessource
                            consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumedRessource.RessourceID).Amount
                                = oldTownRessourceValue;
                        }
                    }
                }
            }
        }
        public void ProduceRessources(Town town)
        {
            // Increase Ressources in Town every Millisecond for every producing Building

            var producingBuildings = town.TownBuildings;
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

                        double producedInMilSeconds = (double)producedRessource.Amount
                            / minuteToMilSeconds
                            * (double)producingBuilding.Workers;

                        double restOfLastInterval = town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).ProducedBetweenInterval;
                        double producedInInterval = producedInMilSeconds * timeElapsedInMilSeconds;

                        // test if Building consumed all ressources for producing
                        bool ressourcesAvailableInBuilding = false;
                        foreach (var consumingRessource in producingBuilding.Building.ConsumingRessources)
                        {
                            var consumedRessource = producingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID);
                            if (consumingRessource.Amount > consumedRessource.Amount)
                            {
                                ressourcesAvailableInBuilding = false;
                                break;
                            }
                            else
                            {
                                ressourcesAvailableInBuilding = true;
                            }
                        }
                        if (ressourcesAvailableInBuilding)
                        {
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
                                if (oldTownRessourceValue < storageValue)
                                {
                                    int newTownRessourceValue = oldTownRessourceValue + (int)Math.Floor(producedInInterval + restOfLastInterval);
                                    if (newTownRessourceValue < storageValue)
                                    {
                                        town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).Amount = newTownRessourceValue;
                                    }
                                    else
                                    {
                                        town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).Amount = storageValue;
                                    }
                                    foreach (var consumedRessource in producingBuilding.RessourcesConsumed)
                                    {
                                        consumedRessource.Amount = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void WorkersConsumeFood(Town town)
        {

            // Decrease Food in Town every Millisecond for every Person

            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeElapsed = currentTime - town.LastUpdated;
            double timeElapsedInMilSeconds = timeElapsed.TotalMilliseconds;
            const int minuteToMilSeconds = 60000;
            int workingPopulation = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "PopulationMax").Amount
                    - town.PopulationNotWorking;
            double restOfLastInterval = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").ProducedBetweenInterval;
            double consumedInMilSecond = (double)workingPopulation / minuteToMilSeconds;
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
                if (newFoodValue > 0)
                {
                    town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").Amount = newFoodValue;
                }
                else
                {
                    town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").Amount = 0;
                }
                // Make Workers to NotWorking if no Food
                int workingPopulationWithoutFood = workingPopulation - town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").Amount;
                if (workingPopulationWithoutFood > 0)
                {
                    foreach (var building in town.TownBuildings)
                    {
                        while (building.Workers > 0 && workingPopulationWithoutFood > 0)
                        {
                            town.PopulationNotWorking++;
                            workingPopulationWithoutFood--;
                            if (town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").Amount > 0)
                            {
                                town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "Food").Amount--;
                            }
                            building.Workers--;
                        }
                    }
                }
            }
            UpdatePopulationNotWorking(town);
        }

        public void AddBuilding(Town town, int id)
        {
            UpdateRessources(town);
            var building = Buildings.FirstOrDefault(n => n.BuildingID == id);
            town.TownBuildings.Add(
               new TownBuilding()
               {
                   Town = town,
                   Building = building,
                   Level = 1,
                   WorkersMax = building.WorkersMaxTemplate
               });
            foreach (var ressource in building.BuildingRessourcesCosts)
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).Amount -=
                    ressource.Amount;
            }
            foreach (var ressource in building.ProducingRessources)
            {
                if (ressource.ProduceOnce)
                {
                    town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).Amount +=
                        ressource.Amount;
                }
            }
            UpdatePopulationNotWorking(town);
            ctx.SaveChanges();
        }
        public void UpdatePopulationNotWorking(Town town)
        {
            int workingPopulation = 0;
            foreach (var building in town.TownBuildings)
            {
                workingPopulation += building.Workers;
            }
            town.PopulationNotWorking = town.TownRessources.FirstOrDefault(i => i.Ressource.RessourceName == "PopulationMax").Amount - workingPopulation;
        }
        public void AddWorkerToBuilding(Town town, int id)
        {
            town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == id).Workers++;
            UpdatePopulationNotWorking(town);
            ctx.SaveChanges();
        }
        public void SubWorkerFromBuilding(Town town, int id)
        {
            town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == id).Workers--;
            UpdatePopulationNotWorking(town);
            ctx.SaveChanges();
        }
    }
}
