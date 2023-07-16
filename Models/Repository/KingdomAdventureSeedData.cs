using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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
            }
        }
        private static void GenerateBuildings(KingdomAdventureDBContext dbContext)
        {
            dbContext.Building.AddRange(
                new Building()
                {
                    BuildingName = "Lumber",
                    WorkersMaxTemplate = 1
                },
                 new Building()
                 {
                     BuildingName = "Storage",
                     WorkersMaxTemplate = 0
                 },
                  new Building()
                  {
                      BuildingName = "Hunting Lodge",
                      WorkersMaxTemplate = 1
                  },
                   new Building()
                   {
                       BuildingName = "Quarry",
                       WorkersMaxTemplate = 2
                   },
                    new Building()
                    {
                        BuildingName = "Windmill",
                        WorkersMaxTemplate = 3
                    },
                     new Building()
                     {
                         BuildingName = "House",
                         WorkersMaxTemplate = 0
                     },
                   new Building()
                   {
                       BuildingName = "Tent",
                       WorkersMaxTemplate = 0
                   }
                );
            dbContext.SaveChanges();
            dbContext.BuildingRessourceCost.AddRange(
                new BuildingRessourceCost()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Lumber"),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Wood"),
                    Amount = 5
                },
                 new BuildingRessourceCost()
                 {
                     Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Lumber"),
                     Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Food"),
                     Amount = 5
                 },
                  new BuildingRessourceCost()
                  {
                      Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Hunting Lodge"),
                      Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Wood"),
                      Amount = 10
                  },
                   new BuildingRessourceCost()
                   {
                       Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Quarry"),
                       Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Wood"),
                       Amount = 20
                   },
                   new BuildingRessourceCost()
                   {
                       Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Windmill"),
                       Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Wood"),
                       Amount = 15
                   },
                     new BuildingRessourceCost()
                     {
                         Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Windmill"),
                         Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Stone"),
                         Amount = 10
                     },
                    new BuildingRessourceCost()
                    {
                        Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Storage"),
                        Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Wood"),
                        Amount = 10
                    },
                   new BuildingRessourceCost()
                   {
                       Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "House"),
                       Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Stone"),
                       Amount = 5
                   },
                    new BuildingRessourceCost()
                    {
                        Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Tent"),
                        Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Wood"),
                        Amount = 10
                    }
                    );
            dbContext.BuildingRessourceProducing.AddRange(
              new BuildingRessourceProducing()
              {
                  Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Lumber"),
                  Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Wood"),
                  Amount = 30
              },
                new BuildingRessourceProducing()
                {
                    Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Hunting Lodge"),
                    Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Food"),
                    Amount = 5
                },
                 new BuildingRessourceProducing()
                 {
                     Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Quarry"),
                     Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Stone"),
                     Amount = 1
                 },
                 new BuildingRessourceProducing()
                 {
                     Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Windmill"),
                     Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Food"),
                     Amount = 10
                 },
                    new BuildingRessourceProducing()
                    {
                        Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Storage"),
                        Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "Storage"),
                        ProduceOnce = true,
                        Amount = 10
                    },
                 new BuildingRessourceProducing()
                 {
                     Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "House"),
                     Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "PopulationMax"),
                     ProduceOnce = true,
                     Amount = 5
                 },
                  new BuildingRessourceProducing()
                  {
                      Building = dbContext.Building.FirstOrDefault(n => n.BuildingName == "Tent"),
                      Ressource = dbContext.Ressource.FirstOrDefault(r => r.RessourceName == "PopulationMax"),
                      ProduceOnce = true,
                      Amount = 2
                  }
              );
            dbContext.SaveChanges();
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
                    building.BuildingRessourcesCosts.Add(ressourceCost);
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
                    AtkMelee = 10,
                    DefMelee = 5,
                    FullLP = 25
                },
                new Soldier()
                {
                    SoldierName = "Archer",
                    AtkPierce = 10,
                    DefMelee = 5,
                    DefPierce = 5,
                    FullLP = 15
                },
                new Soldier()
                {
                    SoldierName = "Mage",
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
                      RessourceName = "PopulationMax",
                      RessourceValue = 0
                  },
                   new Ressource()
                   {
                       RessourceName = "Storage",
                       RessourceValue = 0
                   },
                new Ressource()
                {
                    RessourceName = "Food",
                    RessourceValue = 0.5
                },
                new Ressource()
                {
                    RessourceName = "Wood",
                    RessourceValue = 1.0
                },
                new Ressource()
                {
                    RessourceName = "Stone",
                    RessourceValue = 1.0
                },
                new Ressource()
                {
                    RessourceName = "Cloth",
                    RessourceValue = 1.5
                },
                new Ressource()
                {
                    RessourceName = "Iron",
                    RessourceValue = 2.0
                },
                new Ressource()
                {
                    RessourceName = "Jewelry",
                    RessourceValue = 4.0
                },
                new Ressource()
                {
                    RessourceName = "Gold",
                    RessourceValue = 1.0
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
