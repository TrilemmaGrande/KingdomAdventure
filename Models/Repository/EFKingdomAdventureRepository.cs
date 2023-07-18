using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IQueryable<BuildingRessourceConsuming> BuildingRessourceConsumed => ctx.BuildingRessourceConsumed;
        public IQueryable<BuildingRessourceCost> BuildingRessourceCosts => ctx.BuildingRessourceCost;
        public IQueryable<BuildingRessourceProducing> BuildingRessourceProducings => ctx.BuildingRessourceProducing;
        public IQueryable<BuildingRessourceProducing> BuildingRessourceProduced => ctx.BuildingRessourceProduced;
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

        /* 
         * Consume:
         * For every Building in Town
         * - For every consuming Ressource:
         * OK -- if consumed Ressource.Amount == consuming Ressource.Amount: Stop Consuming
         * OK -- if town Ressource < 1: Stop Consuming
         * OK -- if consuming Ressource < 1 put to Rest consuming
         * OK -- if consuming Ressource + Rest < 1 add consuming Ressource to Rest
         * OK -- if consuming Ressource + Rest >= 1 add to consumed Ressource and put overflow to Rest
         * 
         */
        public void ConsumeRessources(Town town)
        {
            var consumingBuildings = town.TownBuildings;
            foreach (var consumingBuilding in consumingBuildings)
            {
                foreach (var consumingRessource in consumingBuilding.Building.ConsumingRessources)
                {
                    DateTime currentTime = DateTime.UtcNow;
                    TimeSpan timeElapsed = currentTime - town.LastUpdated;
                    double timeElapsedInMilSeconds = timeElapsed.TotalMilliseconds;
                    const int minuteToMilSeconds = 60000;
                    int ressourcesConsumed = consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount;
                    double consumedInMilSeconds = (double)consumingRessource.Amount / minuteToMilSeconds * (double)consumingBuilding.Workers;
                    double restOfLastInterval = town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ConsumedBetweenInterval;
                    double consumedInInterval = consumedInMilSeconds * timeElapsedInMilSeconds;
                    // Amount of NewTownRessource is smaller than OldTownRessource! (Ressources got consumed)
                    int oldTownRessourceValue = town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount;
                    int newTownRessourceValue = oldTownRessourceValue - (int)Math.Floor(consumedInInterval - restOfLastInterval);
                    // if consuming ressources overflow town ressources: cant take more than in stock.
                    if (newTownRessourceValue <= 0)
                    {
                        newTownRessourceValue = 0;
                        consumedInInterval = oldTownRessourceValue;
                    }
                    if (ressourcesConsumed < consumingRessource.Amount && oldTownRessourceValue > 0)
                    {
                        if (consumedInInterval + restOfLastInterval < 1)
                        {
                            if (oldTownRessourceValue > 0)
                            {
                                town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ConsumedBetweenInterval
                                    += consumedInInterval;
                            }
                        }
                        else
                        {
                            // Only take whole numbers for consuming. Put the Rest to ConsumedBetweenInterval.
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ConsumedBetweenInterval
                                = consumedInInterval + restOfLastInterval - Math.Floor(consumedInInterval + restOfLastInterval);
                            // if consuming ressources overflow consumed ressources: take only what needed
                            if (oldTownRessourceValue - newTownRessourceValue + ressourcesConsumed > consumingRessource.Amount)
                            {
                                int newConsumingAmount = consumingRessource.Amount - ressourcesConsumed;
                                // decrease TownRessource
                                town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount = oldTownRessourceValue - newConsumingAmount;
                                // increase Buildingressource
                                consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount
                                    += newConsumingAmount;
                            }
                            else
                            {
                                // decrease TownRessource
                                town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount = newTownRessourceValue;
                                // increase Buildingressource
                                consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount
                                    += oldTownRessourceValue - newTownRessourceValue;
                            }

                        }
                    }
                }
            }
        }
        /* 
         * Produce:         
         * For every Building in Town
         * - For every consuming Ressource:
         * OK -- if every consumed Ressource.Amount == every consuming Ressource.Amount: start Producing
         * - For every producing Ressource:
         * -- if produced Ressource.Amount == producing Ressource.Amount: Stop Producing
         * -- if produced Ressource < 1 put to Rest producing
         * -- if every producing.Amount == produced.Amount: every consumed Ressource = 0, every produced Ressource = 0
         * -- if producing Ressource + Rest< 1 add producing Ressource to Rest
         * -- if producing Ressource + Rest >= 1 add to produced Ressource and put overflow to Rest
         * 
         */
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
                    double producedInMilSeconds = (double)producedRessource.Amount / minuteToMilSeconds * (double)producingBuilding.Workers;
                    double restOfLastInterval = town.TownRessources.FirstOrDefault(i => i.RessourceID == producedRessource.RessourceID).ProducedBetweenInterval;
                    double producedInInterval = producedInMilSeconds * timeElapsedInMilSeconds;
                    if (!producedRessource.ProduceOnce)
                    {
                        //test if Building consumed all ressources for producing
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
                            if (producedInInterval + restOfLastInterval < 1)
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
