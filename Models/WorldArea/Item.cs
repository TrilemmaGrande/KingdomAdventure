﻿using Microsoft.EntityFrameworkCore;

namespace KingdomAdventure.Models.WorldArea
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        QuestItem,
        Upgrade
    }
    public enum WeaponType
    {
        Mainhand, 
        Offhand
    }
    public enum ArmorType
    {
        Head, 
        Shoulder,
        Chest, 
        Trousers,
        Feet
    }
    public class Item
    {
        public int ItemID { get; set; }
        public double? PlayerGold { get; set; }
        public double? Experience { get; set; }
        public double? LP { get; set; }
        public double? FullLP { get; set; }
        public double? AtkMelee { get; set; }
        public double? AtkPierce { get; set; }
        public double? AtkMagic { get; set; }
        public double? DefMelee { get; set; }
        public double? DefPierce { get; set; }
        public double? DefMagic { get; set; }
        public double? Str { get; set; } // 1 Strength = 3 AtkMelee
        public double? End { get; set; } // 1 Endurance = 15 LP
        public double? Dex { get; set; } // 1 Dexterity = 3 AtkPierce
        public double? Int { get; set; } // 1 Intelligence = 3 AtkMagic 
        public double? Crit { get; set; } // 1 Crit = 1% Chance for Crit
        public double? CritDmg { get; set; } // CritDmg = Dmg + (CritDmg * Dmg / 100)
        public UpgradeItem? UpgradeItem1 { get; set; }
        public UpgradeItem? UpgradeItem2 { get; set; }
        public UpgradeItem? UpgradeItem3 { get; set; }
        public UpgradeItem? UpgradeItem4 { get; set; }
        public ItemType ItemType { get; set; }
        public WeaponType? WeaponType { get; set; }
        public ArmorType? ArmorType { get; set; }
    }
}
