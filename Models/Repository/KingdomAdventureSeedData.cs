using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace KingdomAdventure.Models.Repository
{
    public static class KingdomAdventureSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KingdomAdventureDBContext dbContext = app
              .ApplicationServices
              .CreateScope()
              .ServiceProvider
              .GetRequiredService<KingdomAdventureDBContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
            if (!dbContext.Upgrade.Any())
            {
                GenerateUpgradeItems(dbContext);
            }
            if (!dbContext.Item.Any())
            {
                GenerateMainHandWeapons(dbContext);
                GenerateOffHandWeapons(dbContext);
                GenerateArmor(dbContext);
            }
            if (!dbContext.Soldier.Any())
            {
                GenerateSoldiers(dbContext);
            }
            if (!dbContext.Building.Any())
            {
                GenerateBuildings(dbContext);
            }
            if (!dbContext.Ressource.Any())
            {
                GenerateRessources(dbContext);
                GenerateBuildingRessources(dbContext);
                MapBuildingLists(dbContext);
            }
        }
        private static void GenerateBuildings(KingdomAdventureDBContext dbContext)
        {
            dbContext.Building.AddRange(

                new Building()
                {
                    BuildingName = "Storage",
                    EBuildingName = EBuildingName.Storage,
                    StorageMaxTemplate = 10,
                    AvailableInStage = 1,
                    WorkersMaxTemplate = 0
                },
                new Building()
                {
                    BuildingName = "Lumber",
                    EBuildingName = EBuildingName.Lumber,
                    AvailableInStage = 1,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Hunting Lodge",
                    EBuildingName = EBuildingName.Hunter,
                    AvailableInStage = 1,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Farm",
                    EBuildingName = EBuildingName.Farm,
                    AvailableInStage = 3,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Quarry",
                    EBuildingName = EBuildingName.Quarry,
                    AvailableInStage = 2,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Mine",
                    EBuildingName = EBuildingName.Mine,
                    AvailableInStage = 2,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Smith",
                    EBuildingName = EBuildingName.Smith,
                    AvailableInStage = 2,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Fletcher",
                    EBuildingName = EBuildingName.Fletcher,
                    AvailableInStage = 1,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Tanner",
                    EBuildingName = EBuildingName.Tanner,
                    AvailableInStage = 1,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Weaver",
                    EBuildingName = EBuildingName.Weaver,
                    AvailableInStage = 3,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Ritual Fountain",
                    EBuildingName = EBuildingName.RitualFountain,
                    AvailableInStage = 3,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Mage Guild",
                    EBuildingName = EBuildingName.MageGuild,
                    AvailableInStage = 3,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Barracks",
                    EBuildingName = EBuildingName.Barracks,
                    AvailableInStage = 2,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Archery",
                    EBuildingName = EBuildingName.Archery,
                    AvailableInStage = 1,
                    WorkersMaxTemplate = 1
                },
                new Building()
                {
                    BuildingName = "Mansion",
                    EBuildingName = EBuildingName.Mansion,
                    PopulationMaxTemplate = 15,
                    AvailableInStage = 3,
                    LockedInStage = true,
                    WorkersMaxTemplate = 0
                },
                new Building()
                {
                    BuildingName = "House",
                    EBuildingName = EBuildingName.House,
                    PopulationMaxTemplate = 4,
                    AvailableInStage = 2,
                    LockedInStage = true,
                    WorkersMaxTemplate = 0
                },
                new Building()
                {
                    BuildingName = "Tent",
                    EBuildingName = EBuildingName.Tent,
                    PopulationMaxTemplate = 1,
                    AvailableInStage = 1,
                    LockedInStage = true,
                    WorkersMaxTemplate = 0
                }) ;

            dbContext.SaveChanges();
        }
        private static void GenerateBuildingRessources(KingdomAdventureDBContext dbContext)
        {
            // RESSOURCES NEEDED TO BUILD A BUILDING:

            dbContext.BuildingRessourceCost.AddRange(

            // Tent
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Tent),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 20
                },

            // House
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.House),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 15
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.House),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 25
                },

            // Mansion
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mansion),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 50
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mansion),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 50
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mansion),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Silver),
                    Amount = 20
                },

            // Storage
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Storage),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 20
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Storage),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 20
                },

            // Lumber
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Lumber),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 15
                },

            // Hunter
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Hunter),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 20
                },

            // Mine
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mine),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 10
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mine),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 25
                },

            // Quarry
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Quarry),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 20
                },

            // Farm
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Farm),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 50
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Farm),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 20
                },

            // Smith
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Smith),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 40
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Smith),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 40
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Smith),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Iron),
                    Amount = 10
                },

            // Tanner
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Tanner),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 20
                },

            // Fletcher
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Fletcher),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 20
                },

            // Weaver
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Weaver),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 30
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Weaver),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 10
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Weaver),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Silver),
                    Amount = 10
                },

            // Ritual Fountain
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.RitualFountain),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 100
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.RitualFountain),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Gold),
                    Amount = 70
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.RitualFountain),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Cloth),
                    Amount = 20
                },

            // Mage Guild
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 100
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 70
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Cloth),
                    Amount = 20
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Gold),
                    Amount = 50
                },

            // Barracks
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Barracks),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 50
                },
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Barracks),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    Amount = 70
                },

                // Archery
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    Amount = 20
                });

            dbContext.SaveChanges();

            // RESSOURCES PRODUCED BY BUILDINGS:

            dbContext.BuildingRessourceProducing.AddRange(

            // Lumber
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Lumber),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    ProduceInMinute = 6
                },

            // Hunter
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Hunter),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Leather),
                    ProduceInMinute = 3
                },
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Hunter),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Food),
                    ProduceInMinute = 5
                },

            // Mine
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mine),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Iron),
                    ProduceInMinute = 5
                },
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mine),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Silver),
                    ProduceInMinute = 2
                },
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Mine),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Gold),
                    ProduceInMinute = 1
                },

            // Quarry
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Quarry),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Stone),
                    ProduceInMinute = 2
                },

            // Farm
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Farm),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Cloth),
                    ProduceInMinute = 4
                },
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Farm),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Food),
                    ProduceInMinute = 10
                },

            // Smith
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Smith),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Sword),
                    ProduceInMinute = 1
                },
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Smith),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.IronArmor),
                    ProduceInMinute = 1
                },

            // Tanner
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Tanner),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.LeatherArmor),
                    ProduceInMinute = 1
                },

            // Fletcher
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Fletcher),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Bow),
                    ProduceInMinute = 1
                },

            // Weaver
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Weaver),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Robe),
                    ProduceInMinute = 1
                },

            // Ritual Fountain
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.RitualFountain),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wand),
                    ProduceInMinute = 1
                });

            dbContext.SaveChanges();

            // RESSOURCES CONSUMED BY BUILDINGS

            dbContext.BuildingRessourceConsuming.AddRange(

            // Tanner
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Tanner),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Leather),
                    ConsumeInMinute = 4
                },

            // Fletcher
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Fletcher),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    ConsumeInMinute = 2
                },

            // Weaver
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Weaver),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Silver),
                    ConsumeInMinute = 2
                },
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Weaver),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Cloth),
                    ConsumeInMinute = 4
                },

            // RitualFountain
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.RitualFountain),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Gold),
                    ConsumeInMinute = 4
                },
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.RitualFountain),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wood),
                    ConsumeInMinute = 2
                },

            // MageGuild
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Robe),
                    ConsumeInMinute = 1
                }, new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Wand),
                    ConsumeInMinute = 1
                },

            // Barracks
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Barracks),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.IronArmor),
                    ConsumeInMinute = 1
                },
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Barracks),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Sword),
                    ConsumeInMinute = 1
                },

            // Archery
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Archery),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Bow),
                    ConsumeInMinute = 1
                },
                new BuildingRessourceConsuming()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Archery),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.ERessourceName == ERessourceName.Robe),
                    ConsumeInMinute = 1
                });

            dbContext.SaveChanges();

            // SOLDIOERS PRODUCED BY BUILDINGS

            dbContext.BuildingSoldierProducing.AddRange(

            // Archery
                new BuildingSoldierProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Archery),
                    Soldier = dbContext.Soldier.FirstOrDefault(r => r.ESoldierName == ESoldierName.Archer),
                },

            // Barracks
                new BuildingSoldierProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.Barracks),
                    Soldier = dbContext.Soldier.FirstOrDefault(r => r.ESoldierName == ESoldierName.Warrior),
                },

            // Mage Guild
                new BuildingSoldierProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.EBuildingName == EBuildingName.MageGuild),
                    Soldier = dbContext.Soldier.FirstOrDefault(r => r.ESoldierName == ESoldierName.Mage),
                });

            dbContext.SaveChanges();
        }
        private static void MapBuildingLists(KingdomAdventureDBContext dbContext)
        {
            foreach (var producingRessource in dbContext.BuildingRessourceProducing)
            {
                var building = dbContext.Building
                    .FirstOrDefault(i => i.BuildingID == producingRessource.BuildingID);
                if (building is not null)
                {
                    building.ProducingRessources.Add(producingRessource);
                }
            }
            foreach (var consumingRessource in dbContext.BuildingRessourceConsuming)
            {
                var building = dbContext.Building
                    .FirstOrDefault(i => i.BuildingID == consumingRessource.BuildingID);
                if (building is not null)
                {
                    building.ConsumingRessources.Add(consumingRessource);
                }
            }
            foreach (var ressourceCost in dbContext.BuildingRessourceCost)
            {
                var building = dbContext.Building
                    .FirstOrDefault(i => i.BuildingID == ressourceCost.BuildingID);
                if (building is not null)
                {
                    building.RessourceCost.Add(ressourceCost);
                }
            }
            foreach (var producingSoldier in dbContext.BuildingSoldierProducing)
            {
                var building = dbContext.Building
                    .FirstOrDefault(i => i.BuildingID == producingSoldier.BuildingID);
                if (building is not null)
                {
                    building.ProducingSoldiers.Add(producingSoldier);
                }
            }
            dbContext.SaveChanges();
        }
        private static void GenerateSoldiers(KingdomAdventureDBContext dbContext)
        {
            dbContext.Soldier.AddRange(
                new Soldier()
                {
                    SoldierName = "Warrior",
                    ESoldierName = ESoldierName.Warrior,
                    AtkMelee = 10,
                    DefMelee = 5,
                    FullLP = 25
                },
                new Soldier()
                {
                    SoldierName = "Archer",
                    ESoldierName = ESoldierName.Archer,
                    AtkPierce = 10,
                    DefMelee = 5,
                    DefPierce = 5,
                    FullLP = 15
                },
                new Soldier()
                {
                    SoldierName = "Mage",
                    ESoldierName = ESoldierName.Mage,
                    AtkMagic = 10,
                    DefMelee = 5,
                    DefMagic = 5,
                    FullLP = 10
                });
            dbContext.SaveChanges();
        }
        private static void GenerateRessources(KingdomAdventureDBContext dbContext)
        {
            dbContext.Ressource.AddRange(

                new Ressource()
                {
                    RessourceName = "Food",
                    ERessourceName = ERessourceName.Food,
                    RessourceValue = 0.2
                },
                new Ressource()
                {
                    RessourceName = "Wood",
                    ERessourceName = ERessourceName.Wood,
                    RessourceValue = 0.1
                },
                new Ressource()
                {
                    RessourceName = "Stone",
                    ERessourceName = ERessourceName.Stone,
                    RessourceValue = 0.1
                },
                new Ressource()
                {
                    RessourceName = "Cloth",
                    ERessourceName = ERessourceName.Cloth,
                    RessourceValue = 1.5
                },
                new Ressource()
                {
                    RessourceName = "Leather",
                    ERessourceName = ERessourceName.Leather,
                    RessourceValue = 1.5
                },
                new Ressource()
                {
                    RessourceName = "Iron",
                    ERessourceName = ERessourceName.Iron,
                    RessourceValue = 1.0
                },
                new Ressource()
                {
                    RessourceName = "Silver",
                    ERessourceName = ERessourceName.Silver,
                    RessourceValue = 2.5
                },
                new Ressource()
                {
                    RessourceName = "Gold",
                    ERessourceName = ERessourceName.Gold,
                    RessourceValue = 4.0
                },
                new Ressource()
                {
                    RessourceName = "IronArmor",
                    ERessourceName = ERessourceName.IronArmor,
                    RessourceValue = 5.0
                },
                new Ressource()
                {
                    RessourceName = "Sword",
                    ERessourceName = ERessourceName.Sword,
                    RessourceValue = 5.0
                },
                new Ressource()
                {
                    RessourceName = "LeatherArmor",
                    ERessourceName = ERessourceName.LeatherArmor,
                    RessourceValue = 3.5
                },
                new Ressource()
                {
                    RessourceName = "Bow",
                    ERessourceName = ERessourceName.Bow,
                    RessourceValue = 2.5
                },
                new Ressource()
                {
                    RessourceName = "Robe",
                    ERessourceName = ERessourceName.Robe,
                    RessourceValue = 5.0
                },
                new Ressource()
                {
                    RessourceName = "Wand",
                    ERessourceName = ERessourceName.Wand,
                    RessourceValue = 7.0
                });

            dbContext.SaveChanges();
        }
        private static void GenerateMainHandWeapons(KingdomAdventureDBContext dbContext)
        {
            dbContext.Item.AddRange(
              new Item()
              {
                  ItemName = "Rusty Sword",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Mainhand,
                  ItemValue = 2.5,
                  AtkMelee = 2
              },
              new Item()
              {
                  ItemName = "Steel Sword",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Mainhand,
                  ItemValue = 5,
                  AtkMelee = 4,
                  Str = 1,
                  UpgradeItemSlots = 1
              },
              new Item()
              {
                  ItemName = "Silver Sword",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Mainhand,
                  ItemValue = 7,
                  AtkMelee = 6,
                  Str = 2,
                  UpgradeItemSlots = 2
              },
              new Item()
              {
                  ItemName = "Gold Sword",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Mainhand,
                  ItemValue = 10,
                  AtkMelee = 8,
                  Str = 4,
                  UpgradeItemSlots = 3
              },
              new Item()
              {
                  ItemName = "Unholy Staff",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Mainhand,
                  ItemValue = 10,
                  AtkMagic = 8,
                  Int = 4,
                  UpgradeItemSlots = 3
              },
              new Item()
              {
                  ItemName = "Maple Bow",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Mainhand,
                  ItemValue = 4,
                  AtkPierce = 6,
                  Dex = 4,
                  UpgradeItemSlots = 1
              });
            dbContext.SaveChanges();
        }
        private static void GenerateOffHandWeapons(KingdomAdventureDBContext dbContext)
        {
            dbContext.Item.AddRange(
              new Item()
              {
                  ItemName = "Book of Intelligence",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Offhand,
                  ItemValue = 15,
                  Int = 10,
              },
              new Item()
              {
                  ItemName = "Rusty Shield",
                  ItemType = ItemType.Weapon,
                  WeaponType = WeaponType.Offhand,
                  ItemValue = 2.5,
                  DefMelee = 3,
                  DefPierce = 5
              });
            dbContext.SaveChanges();
        }
        private static void GenerateArmor(KingdomAdventureDBContext dbContext)
        {
            dbContext.Item.AddRange(
              new Item()
              {
                  ItemName = "Linen Helmet",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Head,
                  ItemValue = 2.5,
                  DefMelee = 3
              },
              new Item()
              {
                  ItemName = "Linen Shoulders",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Shoulders,
                  ItemValue = 5,
                  DefMelee = 2
              },
              new Item()
              {
                  ItemName = "Linen Chest",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Chest,
                  ItemValue = 7,
                  DefMelee = 5,
                  UpgradeItemSlots = 1
              },
              new Item()
              {
                  ItemName = "Linen Legs",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Legs,
                  ItemValue = 10,
                  DefMelee = 2
              },
              new Item()
              {
                  ItemName = "Linen Feet",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Feet,
                  ItemValue = 4,
                  DefMelee = 1
              },
              new Item()
              {
                  ItemName = "Leather Helmet",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Head,
                  ItemValue = 2.5,
                  DefMelee = 2,
                  DefPierce = 1,
                  UpgradeItemSlots = 1
              },
              new Item()
              {
                  ItemName = "Leather Shoulders",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Shoulders,
                  ItemValue = 5,
                  DefMelee = 2,
                  DefPierce = 1,
                  UpgradeItemSlots = 1
              },
              new Item()
              {
                  ItemName = "Leather Chest",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Chest,
                  ItemValue = 7,
                  DefMelee = 4,
                  DefPierce = 4,
                  UpgradeItemSlots = 2
              },
              new Item()
              {
                  ItemName = "Leather Legs",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Legs,
                  ItemValue = 10,
                  DefMelee = 3,
                  DefPierce = 2,
              },
              new Item()
              {
                  ItemName = "Leather Feet",
                  ItemType = ItemType.Armor,
                  ArmorType = ArmorType.Feet,
                  ItemValue = 4,
                  DefMelee = 2,
              });
            dbContext.SaveChanges();
        }
        private static void GenerateUpgradeItems(KingdomAdventureDBContext dbContext)
        {
            dbContext.Upgrade.AddRange(
              new Upgrade()
              {
                  UpgradeItemName = "Blob of CritDmg",
                  UpgradeItemValue = 20,
                  CritDmg = 5,
                  ItemType = ItemType.Upgrade
              },
              new Upgrade()
              {
                  UpgradeItemName = "Blob of Crit",
                  UpgradeItemValue = 20,
                  Crit = 5,
                  ItemType = ItemType.Upgrade
              },
              new Upgrade()
              {
                  UpgradeItemName = "Blob of End",
                  UpgradeItemValue = 20,
                  End = 5,
                  ItemType = ItemType.Upgrade
              },
              new Upgrade()
              {
                  UpgradeItemName = "Blob of Dex",
                  UpgradeItemValue = 20,
                  Dex = 5,
                  ItemType = ItemType.Upgrade
              },
              new Upgrade()
              {
                  UpgradeItemName = "Blob of Int",
                  UpgradeItemValue = 20,
                  Int = 5,
                  ItemType = ItemType.Upgrade
              },
              new Upgrade()
              {
                  UpgradeItemName = "Blob of Str",
                  UpgradeItemValue = 20,
                  Str = 5,
                  ItemType = ItemType.Upgrade
              });
            dbContext.SaveChanges();
        }
    }
}
