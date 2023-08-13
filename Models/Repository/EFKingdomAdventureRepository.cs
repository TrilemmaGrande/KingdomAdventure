﻿using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
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
        public IQueryable<BuildingRessourceCost> BuildingRessourceCosts => ctx.BuildingRessourceCost;
        public IQueryable<BuildingRessourceProducing> BuildingRessourceProducings => ctx.BuildingRessourceProducing;
        public IQueryable<BuildingSoldierProducing> BuildingSoldierProducings => ctx.BuildingSoldierProducing;
        public IQueryable<TownBuildingDeactivatedRessourceProduction> TownBuildingDeactivatedRessourceProductions => ctx.TownBuildingDeactivatedRessourceProduction;
        public IQueryable<Ressource> Ressources => ctx.Ressource;
        public IQueryable<Soldier> Soldiers => ctx.Soldier;
        public IQueryable<PlayerTown> PlayerTowns => ctx.PlayerTown;
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

        public void CreateTownValues(PlayerTown town)
        {
            foreach (var item in Ressources)
            {
                town.TownRessources.Add(

                    new TownRessource()
                    {
                        PlayerTown = town,
                        Ressource = item,
                        Amount = 0
                    });
            }

            var tent = Buildings.FirstOrDefault(n => n.EBuildingName == EBuildingName.Tent);
            for (int i = 0; i < 2; i++)
            {
                TownBuilding firstBuilding = new TownBuilding()
                {
                    PlayerTown = town,
                    Building = tent,
                    Level = 1,
                    WorkersMax = tent.WorkersMaxTemplate,
                    Population = tent.PopulationMaxTemplate,
                    Storage = tent.StorageMaxTemplate
                };
                town.TownBuildings.Add(firstBuilding);

            }
            town.LastUpdated = DateTime.UtcNow;
            town.Population = 2;
            town.PopulationNotWorking = 2;
            town.Storage = 20;
            town.TownRessources.FirstOrDefault(r => r.Ressource.ERessourceName == ERessourceName.Wood).Amount = 15;
            town.TownRessources.FirstOrDefault(r => r.Ressource.ERessourceName == ERessourceName.Food).Amount = 20;

            ctx.SaveChanges();

        }
        public void UpdateRessources(PlayerTown town)
        {
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeElapsed = currentTime - town.LastUpdated;
            double timeElapsedInMilSeconds = Math.Floor(timeElapsed.TotalMilliseconds / 100);

            for (int tempTimeStep = 0; tempTimeStep <= timeElapsedInMilSeconds; tempTimeStep++)
            {

                ReactivateBuildingProduction(town);

                foreach (var townRessource in town.TownRessources)
                {
                    double tempTownRessourceAmount = townRessource.Amount;
                    double producingInMilSeconds = townRessource.ProduceInTimestep;

                    if (tempTownRessourceAmount + producingInMilSeconds <= 0)
                    {
                        townRessource.Amount = 0;
                        tempTownRessourceAmount = 0;
                    }
                    if (tempTownRessourceAmount + producingInMilSeconds >= town.Storage)
                    {
                        tempTownRessourceAmount = town.Storage;
                        townRessource.Amount = town.Storage;
                    }
                    // Storage empty = stop consuming + stop producing
                    if (tempTownRessourceAmount == 0 && townRessource.ProduceInTimestep < 0)
                    {
                        if (townRessource.Ressource.ERessourceName == ERessourceName.Food)
                        {
                            MakeAllWorkersUnemployed(town);
                        }
                        foreach (var townBuilding in town.TownBuildings.Where(i => i.Building.ConsumingRessources.Any(ii => ii.RessourceID == townRessource.RessourceID)))
                        {
                            foreach (var producingRessource in townBuilding.Building.ProducingRessources)
                            {
                                DeactivateBuildingRessourceProduction(town, townBuilding, producingRessource.RessourceID);
                            }
                            foreach (var consumingRessource in townBuilding.Building.ConsumingRessources)
                            {
                                DeactivateBuildingRessourceConsumption(town, townBuilding);
                            }
                        }
                        continue;
                    }
                    // Storage full = stop producing
                    if (tempTownRessourceAmount == town.Storage && townRessource.ProduceInTimestep > 0)
                    {
                        foreach (var townBuilding in town.TownBuildings.Where(i => i.Building.ProducingRessources.Any(ii => ii.RessourceID == townRessource.RessourceID)))
                        {
                            if (!AllBuildingRessourceProductionDeactivated(townBuilding))
                            {
                                DeactivateBuildingRessourceProduction(town, townBuilding, townRessource.RessourceID);
                                if (AllBuildingRessourceProductionDeactivated(townBuilding))
                                {
                                    DeactivateBuildingRessourceConsumption(town, townBuilding);
                                }
                            }
                        }
                        continue;
                    }
                }
                ProduceRessources(town);
            }
            town.LastUpdated = DateTime.UtcNow;
            ctx.SaveChanges();
        }

        private bool AllBuildingRessourceProductionDeactivated(TownBuilding townBuilding)
        {
            return townBuilding.DeactivatedRessourceProductions.Count() == townBuilding.Building.ProducingRessources.Count();
        }

        private void ReactivateBuildingProduction(PlayerTown town)
        {
            foreach (var townBuilding in town.TownBuildings.Where(i => i.DeactivatedRessourceProductions.Any()))
            {
                bool canConsume = true;
                int workers = townBuilding.Workers;
                if (AllBuildingRessourceProductionDeactivated(townBuilding))
                {
                    foreach (var consumingRessource in townBuilding.Building.ConsumingRessources)
                    {
                        if (town.TownRessources.FirstOrDefault(i => i.Ressource == consumingRessource.Ressource).Amount <= 0)
                        {
                            canConsume = false;
                            break;
                        }
                    }
                    if (canConsume)
                    {
                        bool consumptionTurnedOn = false;
                        foreach (var deactivatedRessourceProduction in townBuilding.DeactivatedRessourceProductions.ToList())
                        {
                            int deactivatedRessourceID = deactivatedRessourceProduction.ProducingRessource.RessourceID;
                            if (town.TownRessources.FirstOrDefault(i => i.RessourceID == deactivatedRessourceID).Amount < town.Storage)
                            {
                                ActivateBuildingRessourceProduction(town, townBuilding, deactivatedRessourceID);
                                if (!consumptionTurnedOn)
                                {
                                    ActivateBuildingRessourceConsumption(town, townBuilding);
                                    consumptionTurnedOn = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var deactivatedRessourceProduction in townBuilding.DeactivatedRessourceProductions.ToList())
                    {
                        int deactivatedRessourceID = deactivatedRessourceProduction.ProducingRessource.RessourceID;
                        if (town.TownRessources.FirstOrDefault(i => i.RessourceID == deactivatedRessourceID).Amount < town.Storage)
                        {
                            ActivateBuildingRessourceProduction(town, townBuilding, deactivatedRessourceID);
                        }
                    }
                }
            }
        }

        private void ActivateBuildingRessourceConsumption(PlayerTown town, TownBuilding townBuilding)
        {
            const double minuteToTenMilSeconds = 600.00;
            foreach (var consumptionRessource in townBuilding.Building.ConsumingRessources)
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == consumptionRessource.RessourceID).ProduceInTimestep
                    -= consumptionRessource.ConsumeInMinute * townBuilding.Workers / minuteToTenMilSeconds;
            }
            for (int i = 0; i < townBuilding.Workers; i++)
            {
                ActivateWorkerFoodConsumption(town);
            }
        }

        private void ActivateBuildingRessourceProduction(PlayerTown town, TownBuilding townBuilding, int ressourceID)
        {
            const double minuteToTenMilSeconds = 600.00; 
            int workers = townBuilding.Workers;
            var producingRessource = townBuilding.Building.ProducingRessources.FirstOrDefault(i => i.RessourceID == ressourceID);
            town.TownRessources
                .FirstOrDefault(i => i.RessourceID == ressourceID).ProduceInTimestep
                    += producingRessource.ProduceInMinute * workers / minuteToTenMilSeconds;

            var drp = townBuilding.DeactivatedRessourceProductions
                    .FirstOrDefault(i => i.BuildingRessourceProducingID == producingRessource.BuildingRessourceProducingID);

            townBuilding.DeactivatedRessourceProductions.Remove(drp);
        }
        private void DeactivateBuildingRessourceProduction(PlayerTown town, TownBuilding townBuilding, int ressourceID)
        {
            const double minuteToTenMilSeconds = 600.00; 
            var producingRessource = townBuilding.Building.ProducingRessources.FirstOrDefault(i => i.RessourceID == ressourceID);

            if (!townBuilding.DeactivatedRessourceProductions.Any(i => i.ProducingRessource == producingRessource))
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProduceInTimestep
                        -= producingRessource.ProduceInMinute * townBuilding.Workers / minuteToTenMilSeconds;
                TownBuildingDeactivatedRessourceProduction drp = new TownBuildingDeactivatedRessourceProduction()
                {
                    TownBuilding = townBuilding,
                    ProducingRessource = producingRessource
                };
                townBuilding.DeactivatedRessourceProductions.Add(drp);
            }
        }
        private void DeactivateBuildingRessourceConsumption(PlayerTown town, TownBuilding townBuilding)
        {
            const double minuteToTenMilSeconds = 600.00;
            foreach (var consumptionRessource in townBuilding.Building.ConsumingRessources)
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == consumptionRessource.RessourceID).ProduceInTimestep
                    += consumptionRessource.ConsumeInMinute * townBuilding.Workers / minuteToTenMilSeconds;
            }
            for (int i = 0; i < townBuilding.Workers; i++)
            {
                DeactivateWorkerFoodConsumption(town);
            }
        }
        public void ProduceRessources(PlayerTown town)
        {
            foreach (var townRessource in town.TownRessources)
            {
                townRessource.Amount += (double)townRessource.ProduceInTimestep;
            }
        }
        public void MakeAllWorkersUnemployed(PlayerTown town)
        {
            var townFoodRessource = town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food);

            if (townFoodRessource.Amount <= 0)
            {
                foreach (var townBuilding in town.TownBuildings)
                {
                    SubWorkerFromBuilding(town, townBuilding.TownBuildingID);
                }
                townFoodRessource.Amount = 0;
                townFoodRessource.ProduceInTimestep = 0;
            }
        }
        public void AddWorkerToBuilding(PlayerTown town, int townBuildingID)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(n => n.TownBuildingID == townBuildingID);
            DecreaseTownProduction(town, townBuilding);
            if (townBuilding.WorkersMax > townBuilding.Workers && town.PopulationNotWorking > 0)
            {
                townBuilding.Workers++;
                town.PopulationNotWorking--;
                if (!AllBuildingRessourceProductionDeactivated(townBuilding))
                {
                    ActivateWorkerFoodConsumption(town);
                }
            }
            IncreaseTownProduction(town, townBuilding);
            ctx.SaveChanges();

        }
        public void SubWorkerFromBuilding(PlayerTown town, int townBuildingID)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(n => n.TownBuildingID == townBuildingID);
            DecreaseTownProduction(town, townBuilding);
            if (townBuilding.Workers > 0 && town.PopulationNotWorking < town.Population)
            {
                townBuilding.Workers--;
                town.PopulationNotWorking++;
                if (!AllBuildingRessourceProductionDeactivated(townBuilding))
                {
                    DeactivateWorkerFoodConsumption(town);
                }
            }
            IncreaseTownProduction(town, townBuilding);
            ctx.SaveChanges();

        }
        private void ActivateWorkerFoodConsumption(PlayerTown town)
        {
            const double minuteToTenMilSeconds = 600.00;
            town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food).ProduceInTimestep -= 1 / minuteToTenMilSeconds;
        }
        private void DeactivateWorkerFoodConsumption(PlayerTown town)
        {
            const double minuteToTenMilSeconds = 600.00;
            town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food).ProduceInTimestep += 1 / minuteToTenMilSeconds;

        }

        private void IncreaseTownProduction(PlayerTown town, TownBuilding townBuilding)
        {
            const double minuteToTenMilSeconds = 600.00;
            int workers = townBuilding.Workers;
            foreach (var producingRessource in townBuilding.Building.ProducingRessources)
            {
                if (!townBuilding.DeactivatedRessourceProductions.Any(i => i.ProducingRessource == producingRessource))
                {
                    town.TownRessources
                    .FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProduceInTimestep
                        += producingRessource.ProduceInMinute * workers / minuteToTenMilSeconds;
                }
            }
            ctx.SaveChanges();
        }
        private void DecreaseTownProduction(PlayerTown town, TownBuilding townBuilding)
        {
            const double minuteToTenMilSeconds = 600.00;
            int workers = townBuilding.Workers;
            foreach (var ressource in Buildings.FirstOrDefault(i => i.BuildingID == townBuilding.BuildingID).ProducingRessources)
            {
                if (!townBuilding.DeactivatedRessourceProductions.Any(i => i.ProducingRessource.RessourceID == ressource.RessourceID))
                {
                    town.TownRessources
                        .FirstOrDefault(i => i.RessourceID == ressource.RessourceID).ProduceInTimestep
                            -= ressource.ProduceInMinute * workers / minuteToTenMilSeconds;
                }
            }
            ctx.SaveChanges();
        }
        public void AddBuilding(PlayerTown town, int buildingID)
        {
            UpdateRessources(town);
            var building = Buildings.FirstOrDefault(n => n.BuildingID == buildingID);

            TownBuilding townBuilding = new TownBuilding()
            {
                PlayerTown = town,
                Building = building,
                Level = 1,
                WorkersMax = building.WorkersMaxTemplate,
                Population = building.PopulationMaxTemplate,
                Storage = building.StorageMaxTemplate
            };
            town.TownBuildings.Add(townBuilding);
            foreach (var ressource in BuildingRessourceCosts.Where(i => i.BuildingID == buildingID))
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).Amount -=
                    ressource.Amount;
            }
            town.Population += townBuilding.Population;
            town.PopulationNotWorking += townBuilding.Population;
            town.Storage += townBuilding.Storage;
            ctx.SaveChanges();
        }
        public void RemoveBuilding(PlayerTown town, int id)
        {
            UpdateRessources(town);
            const double minuteToTenMilSeconds = 600.00;
            var townBuilding = town.TownBuildings.FirstOrDefault(n => n.TownBuildingID == id);
            foreach (var ressource in Buildings.FirstOrDefault(i => i.BuildingID == townBuilding.BuildingID).RessourceCost)
            {
                var townRessource = town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID);
                int restoringRessources = (int)Math.Floor(ressource.Amount * 0.25);
                int storage = town.Storage;
                if (townRessource.Amount + restoringRessources <= storage)
                {
                    townRessource.Amount += restoringRessources;
                }
                else
                {
                    townRessource.Amount = storage;
                }
            }
            for (int i = 0; i < townBuilding.Workers; i++)
            {
                SubWorkerFromBuilding(town, townBuilding.TownBuildingID);
            }
            if (townBuilding.Storage > 0)
            {
                town.Storage -= townBuilding.Storage;
                foreach (var ressource in town.TownRessources)
                {
                    if (ressource.Amount > town.Storage)
                    {
                        ressource.Amount = town.Storage;
                    }
                }
            }
            if (townBuilding.Population > 0)
            {
                town.Population -= townBuilding.Population;
                Random rnd = new Random();
                for (int i = 0; i < townBuilding.Population; i++)
                {
                    if (town.PopulationNotWorking > 0)
                    {
                        town.PopulationNotWorking--;

                    }
                    else
                    {
                        var randomTownBuildingID = town.TownBuildings
                            .Where(i => i.Workers > 0)
                            .OrderBy(r => rnd.Next())
                            .FirstOrDefault().TownBuildingID;
                        SubWorkerFromBuilding(town, randomTownBuildingID);
                    }
                }
            }
            town.TownBuildings.Remove(townBuilding);
            ctx.SaveChanges();
        }


        public void LevelUpBuilding(PlayerTown town, int id)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(i => i.BuildingID == id);
            townBuilding.WorkersMax++;
            townBuilding.Level++;
            townBuilding.Population += townBuilding.Building.PopulationMaxTemplate;
            town.Population += townBuilding.Building.PopulationMaxTemplate;
            town.PopulationNotWorking += townBuilding.Building.PopulationMaxTemplate;
            townBuilding.Storage += townBuilding.Building.StorageMaxTemplate;
            town.Storage += townBuilding.Building.StorageMaxTemplate;
            foreach (var ressource in townBuilding.Building.RessourceCost)
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).Amount -=
                    (int)Math.Floor(ressource.Amount * 0.75);
            }
            ctx.SaveChanges();

        }
        public void IncreaseTownStage(PlayerTown town)
        {
            UpdateRessources(town);

            int newStage = town.Stage + 1;
            town.Stage++;
            IEnumerable<TownBuilding> townBuildings = town.TownBuildings.Where(i => i.Building.LockedInStage == true && i.Building.AvailableInStage < newStage);
            foreach (var townBuilding in townBuildings)
            {
                EBuildingName? newBuildingEBuildingName;
                switch (townBuilding.Building.EBuildingName)
                {
                    case EBuildingName.Tent:
                        newBuildingEBuildingName = EBuildingName.House;
                        break;
                    case EBuildingName.House:
                        newBuildingEBuildingName = EBuildingName.Mansion;
                        break;
                    default:
                        continue;
                }
                if (newBuildingEBuildingName is not null)
                {
                    town.TownBuildings.Remove(townBuilding);
                    Building newBuilding = Buildings.FirstOrDefault(i => i.EBuildingName == newBuildingEBuildingName);
                    TownBuilding newTownBuilding = new TownBuilding()
                    {
                        Building = newBuilding,
                        Level = 1,
                        Storage = newBuilding.StorageMaxTemplate,
                        Population = newBuilding.PopulationMaxTemplate,
                        WorkersMax = newBuilding.WorkersMaxTemplate
                    };
                    town.TownBuildings.Add(newTownBuilding);
                }
            }
            ctx.SaveChanges();
        }
    }
}
