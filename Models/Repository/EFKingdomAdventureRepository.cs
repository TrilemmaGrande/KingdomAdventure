using KingdomAdventure.Models.TownArea;
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
            double timeElapsedInMilSeconds = timeElapsed.TotalMilliseconds;
            const double minuteToMilSeconds = 60000;

            for (int tempTimeStep = 0; tempTimeStep < timeElapsedInMilSeconds; tempTimeStep++)
            {

                ResetBuildingProduction(town);

                foreach (var townRessource in town.TownRessources)
                {
                    double tempTownRessourceAmount = townRessource.Amount;
                    double producingInMilSeconds = townRessource.ProducingInMinute / minuteToMilSeconds;

                    if (tempTownRessourceAmount + producingInMilSeconds < 0)
                    {
                        tempTownRessourceAmount = 0;
                    }
                    if (tempTownRessourceAmount + producingInMilSeconds > town.Storage)
                    {
                        tempTownRessourceAmount = town.Storage;
                        townRessource.Amount = town.Storage;
                    }

                    if (tempTownRessourceAmount == 0)
                    {
                        foreach (var townBuilding in TownBuildings.Where(i => i.Building.ConsumingRessources.Any(ii => ii.RessourceID == townRessource.RessourceID)))
                        {
                            DeactivateBuildingRessourceProduction(townBuilding);
                        }
                        continue;
                    }
                    if (tempTownRessourceAmount == town.Storage)
                    {
                        foreach (var townBuilding in TownBuildings.Where(i => i.Building.ProducingRessources.Any(ii => ii.RessourceID == townRessource.RessourceID)))
                        {
                            DeactivateBuildingRessourceProduction(townBuilding, townRessource.RessourceID);
                        }
                        continue;
                    }
                }
       
                ProduceRessources(town);

                if (timeElapsedInMilSeconds + tempTimeStep % minuteToMilSeconds == 0)
                {
    
                    WorkersConsumeFood(town);
                }
            }
            town.LastUpdated = DateTime.UtcNow;
            ctx.SaveChanges();
        }
        private void ResetBuildingProduction(PlayerTown town)
        {
            const double minuteToMilSeconds = 60000;
            foreach (var townBuilding in town.TownBuildings.Where(i => i.DeactivatedRessourceProductions.Any()))
            {
                int workers = townBuilding.Workers;
                bool canProduce = true;
                bool canConsume = true;

                foreach (var consumingRessource in townBuilding.Building.ConsumingRessources)
                {
                    double buildingConsumingInMilSeconds = consumingRessource.ConsumeInMinute * workers / minuteToMilSeconds;
                    if (buildingConsumingInMilSeconds < 0)
                    {
                        canConsume = false;
                        break;
                    }
                }
                foreach (var deactivatedProducingRessource in townBuilding.DeactivatedRessourceProductions)
                {
                    double buildingProducingInMilSeconds = deactivatedProducingRessource.ProducingRessource.ProduceInMinute * workers / minuteToMilSeconds;
                    if (buildingProducingInMilSeconds < town.Storage)
                    {
                        ActivateBuildingRessourceProduction(townBuilding, deactivatedProducingRessource.ProducingRessource.RessourceID);
                    }
                }
            }
        }
        private void DeactivateBuildingRessourceProduction(TownBuilding townBuilding)
        {
            PlayerTown town = townBuilding.PlayerTown;
            int workers = townBuilding.Workers;

            foreach (var producingRessource in townBuilding.Building.ProducingRessources)
            {
                town.TownRessources
                     .FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProducingInMinute
                         -= producingRessource.ProduceInMinute * workers;
                TownBuildingDeactivatedRessourceProduction drp = new TownBuildingDeactivatedRessourceProduction()
                {
                    TownBuilding = townBuilding,
                    ProducingRessource = producingRessource
                };
                townBuilding.DeactivatedRessourceProductions.Add(drp);
            }
            foreach (var consumingRessource in townBuilding.Building.ConsumingRessources)
            {
                town.TownRessources
                      .FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ProducingInMinute
                          += consumingRessource.ConsumeInMinute * workers;
            }

        }
        private void DeactivateBuildingRessourceProduction(TownBuilding townBuilding, int ressourceID)
        {
            PlayerTown town = townBuilding.PlayerTown;
            int workers = townBuilding.Workers;

            foreach (var producingRessource in townBuilding.Building.ProducingRessources.Where(i => i.RessourceID == ressourceID))
            {
                town.TownRessources
                      .FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProducingInMinute
                          -= producingRessource.ProduceInMinute * workers;
                TownBuildingDeactivatedRessourceProduction drp = new TownBuildingDeactivatedRessourceProduction()
                {
                    TownBuilding = townBuilding,
                    ProducingRessource = producingRessource
                };
                townBuilding.DeactivatedRessourceProductions.Add(drp);
            }
       
        }
        private void ActivateBuildingRessourceProduction(TownBuilding townBuilding)
        {
            PlayerTown town = townBuilding.PlayerTown;
            int workers = townBuilding.Workers;

            foreach (var producingRessource in townBuilding.Building.ProducingRessources)
            {
                town.TownRessources
                    .FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProducingInMinute
                        += producingRessource.ProduceInMinute * workers;

                var dpr = townBuilding.DeactivatedRessourceProductions
                    .FirstOrDefault(i => i.BuildingRessourceProducingID == producingRessource.BuildingRessourceProducingID);
                townBuilding.DeactivatedRessourceProductions.Remove(dpr);
            }
            foreach (var consumingRessource in townBuilding.Building.ConsumingRessources)
            {
                town.TownRessources
                    .FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ProducingInMinute
                        -= consumingRessource.ConsumeInMinute * workers;
            }
        
        }
        private void ActivateBuildingRessourceProduction(TownBuilding townBuilding, int ressourceID)
        {
            PlayerTown town = townBuilding.PlayerTown;
            int workers = townBuilding.Workers;

            town.TownRessources
                .FirstOrDefault(i => i.RessourceID == ressourceID).ProducingInMinute
                    += townBuilding.Building.ProducingRessources
                    .FirstOrDefault(i => i.RessourceID == ressourceID).ProduceInMinute * workers;

            var dpr = townBuilding.DeactivatedRessourceProductions
                    .FirstOrDefault(i => i.BuildingRessourceProducingID
                     == townBuilding.Building.ProducingRessources
                    .FirstOrDefault(ii => ii.RessourceID == ressourceID).BuildingRessourceProducingID);

            townBuilding.DeactivatedRessourceProductions.Remove(dpr);
            foreach (var consumingRessource in townBuilding.Building.ConsumingRessources)
            {
                town.TownRessources
                    .FirstOrDefault(i => i.RessourceID == consumingRessource.RessourceID).ProducingInMinute
                        -= consumingRessource.ConsumeInMinute * workers;
            }
      
        }

        public void ProduceRessources(PlayerTown town)
        {
            const double minuteToMilSeconds = 60000;

            foreach (var townRessource in town.TownRessources)
            {
                townRessource.Amount += (double)townRessource.ProducingInMinute / (double)minuteToMilSeconds;
            }
       
        }
        public void WorkersConsumeFood(PlayerTown town)
        {
            double populationInBuildingNotWorking = town.TownBuildings
                .Where(i => i.DeactivatedRessourceProductions.Count() == i.Building.ProducingRessources.Count())
                .Sum(i => i.Workers);
            double workingPopulation = town.Population - town.PopulationNotWorking - populationInBuildingNotWorking;
            var townFoodRessource = town.TownRessources.FirstOrDefault(i => i.Ressource.ERessourceName == ERessourceName.Food);

            if (townFoodRessource.Amount - workingPopulation <= 0)
            {
                townFoodRessource.Amount = 0;
                town.TownBuildings.ForEach(i => i.Workers = 0);
                town.PopulationNotWorking = town.Population;
            }
            else
            {
                townFoodRessource.Amount -= workingPopulation;
            }
       
        }

        public void AddBuilding(PlayerTown town, int id)
        {
            UpdateRessources(town);
            var building = Buildings.FirstOrDefault(n => n.BuildingID == id);

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
            foreach (var ressource in BuildingRessourceCosts.Where(i => i.BuildingID == id))
            {
                town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).Amount -=
                    ressource.Amount;
            }
            town.Population += townBuilding.Population;
            town.PopulationNotWorking += townBuilding.Building.PopulationMaxTemplate;
            town.Storage += townBuilding.Storage;
            ctx.SaveChanges();
        }
        public void RemoveBuilding(PlayerTown town, int id)
        {
            UpdateRessources(town);
            var townBuilding = town.TownBuildings.FirstOrDefault(n => n.TownBuildingID == id);
            town.TownBuildings.Remove(townBuilding);

            if (townBuilding.Storage > 0)
            {
                town.Storage -= townBuilding.Storage;
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
                        int randomBuildingIDWithWorker = town.TownBuildings
                            .Where(i => i.Workers > 0)
                            .OrderBy(r => rnd.Next())
                            .FirstOrDefault().TownBuildingID;
                        SubWorkerFromBuilding(town, randomBuildingIDWithWorker);
                    }
                }
            }
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
            foreach (var ressource in Buildings.FirstOrDefault(i => i.BuildingID == townBuilding.BuildingID).ProducingRessources)
            {
                if (!townBuilding.DeactivatedRessourceProductions.Any(i => i.ProducingRessource.RessourceID == ressource.RessourceID))
                {
                    town.TownRessources.FirstOrDefault(i => i.RessourceID == ressource.RessourceID).ProducingInMinute -=
                        ressource.ProduceInMinute * townBuilding.Workers;
                }
            }

            if (townBuilding.Storage > 0)
            {
                foreach (var ressource in town.TownRessources)
                {
                    if (ressource.Amount > town.Storage)
                    {
                        ressource.Amount = town.Storage;
                    }
                }
            }
            ctx.SaveChanges();
        }
        public void AddWorkerToBuilding(PlayerTown town, int id)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == id);
            DecreaseTownProduction(town, id);
            if (townBuilding.WorkersMax > townBuilding.Workers && town.PopulationNotWorking > 0)
            {
                townBuilding.Workers++;
                town.PopulationNotWorking--;
            }
            IncreaseTownProduction(town, id);
            ctx.SaveChanges();

        }
        public void SubWorkerFromBuilding(PlayerTown town, int id)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == id);
            DecreaseTownProduction(town, id);
            if (townBuilding.Workers > 0 && town.PopulationNotWorking < town.Population)
            {
                townBuilding.Workers--;
                town.PopulationNotWorking++;
            }
            IncreaseTownProduction(town, id);
            ctx.SaveChanges();
       
        }
        private void IncreaseTownProduction(PlayerTown town, int id)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == id);
            int workers = townBuilding.Workers;
            foreach (var producingRessource in townBuilding.Building.ProducingRessources)
            {
                town.TownRessources
                    .FirstOrDefault(i => i.RessourceID == producingRessource.RessourceID).ProducingInMinute
                        += producingRessource.ProduceInMinute * workers;
            }
            ctx.SaveChanges();
        }
        private void DecreaseTownProduction(PlayerTown town, int id)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == id);
            int workers = townBuilding.Workers;
            foreach (var ressource in Buildings.FirstOrDefault(i => i.BuildingID == townBuilding.BuildingID).ProducingRessources)
            {
                if (!townBuilding.DeactivatedRessourceProductions.Any(i => i.ProducingRessource.RessourceID == ressource.RessourceID))
                {
                    town.TownRessources
                        .FirstOrDefault(i => i.RessourceID == ressource.RessourceID).ProducingInMinute -=
                        ressource.ProduceInMinute * workers;
                }
            }
            ctx.SaveChanges();

        }

        public void LevelUpBuilding(PlayerTown town, int id)
        {
            var townBuilding = town.TownBuildings.FirstOrDefault(i => i.TownBuildingID == id);
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
            town.Stage = newStage;
            IEnumerable<TownBuilding> townBuildings = TownBuildings.Where(i => i.Building.LockedInStage == true && i.Building.AvailableInStage < newStage);
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
