using KingdomAdventure.Models.WorldArea;
using Microsoft.EntityFrameworkCore;

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
      if (!dbContext.UpgradeItem.Any())
      {
        GenerateUpgradeItems(dbContext);
      }
      if (!dbContext.Item.Any())
      {
        GenerateMainHandWeapons(dbContext);
        GenerateOffHandWeapons(dbContext);
        GenerateArmor(dbContext);
      }
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
              }) ;
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
      dbContext.UpgradeItem.AddRange(
        new UpgradeItem()
        {
          UpgradeItemName = "Blob of CritDmg",
          UpgradeItemValue = 20,
          CritDmg = 5,
          ItemType = ItemType.Upgrade
        },
        new UpgradeItem()
        {
          UpgradeItemName = "Blob of Crit",
          UpgradeItemValue = 20,
          Crit = 5,
          ItemType = ItemType.Upgrade
        },
        new UpgradeItem()
        {
          UpgradeItemName = "Blob of End",
          UpgradeItemValue = 20,
          End = 5,
          ItemType = ItemType.Upgrade
        },
        new UpgradeItem()
        {
          UpgradeItemName = "Blob of Dex",
          UpgradeItemValue = 20,
          Dex = 5,
          ItemType = ItemType.Upgrade
        },
        new UpgradeItem()
        {
          UpgradeItemName = "Blob of Int",
          UpgradeItemValue = 20,
          Int = 5,
          ItemType = ItemType.Upgrade
        },
        new UpgradeItem()
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
