using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;


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
                        Amount = 0
                    });
            }
            var tent = Buildings.FirstOrDefault(n => n.EBuildingName == EBuildingName.Tent);
            TownBuilding firstBuilding = new TownBuilding()
            {
                Town = town,
                Building = tent,
                Level = 1,
                WorkersMax = tent.WorkersMaxTemplate
            };
            town.TownBuildings.Add(firstBuilding);
            ctx.SaveChanges();
            foreach (var ressource in BuildingRessourceProducings.Where(i => i.Building.BuildingName == tent.BuildingName))
            {
                firstBuilding.RessourcesConsumed.Add(
                    new TownBuildingRessourceConsumed()
                    {
                        TownBuilding = town.TownBuildings.FirstOrDefault(i => i.Building.BuildingName == tent.BuildingName),
                        Ressource = ressource.Ressource,
                        Amount = 0
                    });
            }
            foreach (var ressource in BuildingRessourceProducings.Where(i => i.Building.BuildingName == tent.BuildingName))
            {
                firstBuilding.RessourcesProduced.Add(
                    new TownBuildingRessourceProduced()
                    {
                        TownBuilding = town.TownBuildings.FirstOrDefault(i => i.Building.BuildingName == tent.BuildingName),
                        Ressource = ressource.Ressource,
                        Amount = 0
                    });
            }
            town.TownRessources.FirstOrDefault(r => r.Ressource.ERessourceName == ERessourceName.PopulationMax).Amount = 2;
            town.TownRessources.FirstOrDefault(r => r.Ressource.ERessourceName == ERessourceName.Storage).Amount = 20;
            town.TownRessources.FirstOrDefault(r => r.Ressource.ERessourceName == ERessourceName.Wood).Amount = 15;
            town.TownRessources.FirstOrDefault(r => r.Ressource.ERessourceName == ERessourceName.Food).Amount = 20;
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
                    double restOfLastInterval = consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ConsumedBetweenInterval;
                    double consumedInInterval = consumedInMilSeconds * timeElapsedInMilSeconds;
                    // Amount of NewTownRessource is smaller than OldTownRessource! (Ressources got consumed)
                    int oldTownRessourceValue = town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount;
                    int newTownRessourceValue = oldTownRessourceValue - (int)Math.Floor(consumedInInterval + restOfLastInterval);
                    // if consuming ressources overflow town ressources: cant take more than in stock.
                    if (newTownRessourceValue <= 0)
                    {
                        newTownRessourceValue = 0;
                        restOfLastInterval = 0;
                        consumedInInterval = oldTownRessourceValue;
                    }
                    if (ressourcesConsumed < consumingRessource.Amount && oldTownRessourceValue > 0)
                    {
                        if (consumedInInterval + restOfLastInterval < 1)

                            consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ConsumedBetweenInterval
                            += consumedInInterval;
                    }
                    else
                    {
                        // Only take whole numbers for consuming. Put the Rest to ConsumedBetweenInterval.
                        consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ConsumedBetweenInterval
                            = consumedInInterval + restOfLastInterval - Math.Floor(consumedInInterval + restOfLastInterval);
                        // if consuming ressources overflow consumed ressources: take only what needed
                        if (oldTownRessourceValue - newTownRessourceValue + ressourcesConsumed > consumingRessource.Amount)
                        {
                            int newConsumingAmount = consumingRessource.Amount - ressourcesConsumed;
                            // decrease TownRessource
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount = oldTownRessourceValue - newConsumingAmount;
                            // increase ConsumedCounter
                            consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount
                                += newConsumingAmount;
                        }
                        else
                        {
                            // decrease TownRessource
                            town.TownRessources.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount = newTownRessourceValue;
                            // increase ConsumedCounter
                            consumingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount
                                += oldTownRessourceValue - newTownRessourceValue;
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
         * OK -- if produced Ressource.Amount == producing Ressource.Amount: Stop Producing
         * OK -- if producing.Amount == produced.Amount: every consumed Ressource = 0, every produced Ressource = 0
         * -- if produced Ressource < 1 put to Rest producing
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
                bool producedAllProducingRessources = false;
                foreach (var producingRessource in producingBuilding.Building.ProducingRessources)
                {
                    DateTime currentTime = DateTime.UtcNow;
                    TimeSpan timeElapsed = currentTime - town.LastUpdated;
                    double timeElapsedInMilSeconds = timeElapsed.TotalMilliseconds;
                    const int minuteToMilSeconds = 60000;
                    int ressourcesProduced = producingBuilding.RessourcesProduced.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount;
                    double producedInMilSeconds = (double)producingRessource.Amount / minuteToMilSeconds * (double)producingBuilding.Workers;
                    double restOfLastInterval = producingBuilding.RessourcesProduced.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProducedBetweenInterval;
                    double producedInInterval = producedInMilSeconds * timeElapsedInMilSeconds;
                    int oldTownRessourceValue = town.TownRessources.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount;
                    int newTownRessourceValue = oldTownRessourceValue + (int)Math.Floor(producedInInterval + restOfLastInterval);
                    int storageValue = town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Storage).Amount;
                    if (!producingRessource.ProduceOnce)
                    {
                        //test if Building consumed all ressources for producing
                        bool ressourcesAvailableInBuilding = true;
                        foreach (var consumingRessource in producingBuilding.Building.ConsumingRessources)
                        {
                            var consumedRessource = producingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID);
                            if (consumingRessource.Amount > consumedRessource.Amount)
                            {
                                ressourcesAvailableInBuilding = false;
                                break;
                            }
                        }
                        if (ressourcesAvailableInBuilding)
                        {
                            if (newTownRessourceValue >= storageValue)
                            {
                                newTownRessourceValue = storageValue;
                                restOfLastInterval = 0;
                                producedInInterval = storageValue - oldTownRessourceValue;
                            }
                            if (ressourcesProduced < producingRessource.Amount && oldTownRessourceValue < storageValue)
                            {

                                if (producedInInterval + restOfLastInterval < 1)
                                {
                                    producingBuilding.RessourcesProduced
                                        .FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProducedBetweenInterval
                                        += producedInInterval;
                                }
                                else
                                {
                                    // Only take whole numbers for producing. Put the Rest to ProducingBetweenInterval.
                                    producingBuilding.RessourcesProduced
                                        .FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProducedBetweenInterval
                                         = producedInInterval + restOfLastInterval - Math.Floor(producedInInterval + restOfLastInterval);
                                    // if producing ressources overflow produced ressources: produce only what needed
                                    if (newTownRessourceValue - oldTownRessourceValue + ressourcesProduced > producingRessource.Amount)
                                    {
                                        int newProducingAmount = producingRessource.Amount - ressourcesProduced;
                                        // increase TownRessource
                                        town.TownRessources.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount = oldTownRessourceValue + newProducingAmount;
                                        // increase ProducedCounter
                                        producingBuilding.RessourcesProduced.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount
                                            += newProducingAmount;
                                    }
                                    else
                                    {
                                        // increase TownRessource
                                        town.TownRessources.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount = newTownRessourceValue;
                                        // increase ProducedCounter
                                        producingBuilding.RessourcesProduced.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount
                                            += newTownRessourceValue - oldTownRessourceValue;
                                    }
                                }
                            }
                            // test if a cycle of producing is through (produced the whole Amount)
                            int ressourcesProducing = producingRessource.Amount;
                            ressourcesProduced = producingBuilding.RessourcesProduced.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount;
                            if (ressourcesProduced < ressourcesProducing)
                            {
                                producedAllProducingRessources = false;
                            }
                            else
                            {
                                producedAllProducingRessources = true;
                            }
                        }
                    }
                }
                // reset producing cycle
                if (producedAllProducingRessources)
                {
                    foreach (var consumingRessource in producingBuilding.Building.ConsumingRessources)
                    {
                        producingBuilding.RessourcesConsumed.FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).Amount = 0;
                    }
                    foreach (var producingRessource in producingBuilding.Building.ProducingRessources)
                    {
                        producingBuilding.RessourcesProduced.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).Amount = 0;
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
            int workingPopulation = town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.PopulationMax).Amount
                    - town.PopulationNotWorking;
            double restOfLastInterval = town.PopulationFoodConsumptionLastInterval;
            double consumedInMilSecond = (double)workingPopulation / minuteToMilSeconds;
            double consumedInInterval = consumedInMilSecond * timeElapsedInMilSeconds;
            int oldFoodValue = town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food).Amount;
            int newFoodValue = oldFoodValue - (int)Math.Floor(consumedInInterval + restOfLastInterval);

            if (newFoodValue <= 0)
            {
                newFoodValue = 0;
                restOfLastInterval = 0;
                consumedInInterval = oldFoodValue;
            }
            if (consumedInInterval + restOfLastInterval < 1 && oldFoodValue > 0)
            {
                town.PopulationFoodConsumptionLastInterval
                    += consumedInInterval;
            }
            else
            {
                town.PopulationFoodConsumptionLastInterval
                    = consumedInInterval + restOfLastInterval - Math.Floor(consumedInInterval + restOfLastInterval);

                town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food).Amount = newFoodValue;


                // Make Workers to NotWorking if no Food
                int workingPopulationWithoutFood = 
                    workingPopulation - town.TownRessources
                                            .FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food).Amount;
                if (workingPopulationWithoutFood > 0)
                {
                    foreach (var building in town.TownBuildings)
                    {
                        while (building.Workers > 0 && workingPopulationWithoutFood > 0)
                        {
                            town.PopulationNotWorking++;
                            workingPopulationWithoutFood--;
                            if (town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food).Amount > 0)
                            {
                                town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food).Amount--;
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

            TownBuilding townBuilding = new TownBuilding()
            {
                Town = town,
                Building = building,
                Level = 1,
                WorkersMax = building.WorkersMaxTemplate
            };
            town.TownBuildings.Add(townBuilding);
            ctx.SaveChanges();
            foreach (var ressource in BuildingRessourceProducings.Where(i => i.Building.BuildingID == id))
            {
                townBuilding.RessourcesConsumed.Add(
                    new TownBuildingRessourceConsumed()
                    {
                        TownBuilding = town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == townBuilding.TownBuildingID),
                        Ressource = ressource.Ressource,
                        Amount = 0
                    });
            }
            foreach (var ressource in BuildingRessourceProducings.Where(i => i.Building.BuildingID == id))
            {
                townBuilding.RessourcesProduced.Add(
                    new TownBuildingRessourceProduced()
                    {
                        TownBuilding = town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == townBuilding.TownBuildingID),
                        Ressource = ressource.Ressource,
                        Amount = 0
                    });
            }
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
            town.PopulationNotWorking = town.TownRessources
                .FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.PopulationMax).Amount - workingPopulation;
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
