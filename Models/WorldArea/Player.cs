namespace KingdomAdventure.Models.WorldArea
{
    public class Player
    {
        public string PlayerName { get; set; }
        public double PlayerGold { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
        public double CurrentLP { get; set; }
        public double FullLP { get; set; }
        private double CapLP { get; } = 1000;
        public double AtkMelee { get; set; }
        public double AtkPierce { get; set; }
        public double AtkMagic { get; set; }
        public double DefMelee { get; set; }
        public double DefPierce { get; set; }
        public double DefMagic { get; set; }
        public double Str { get; set; } // 1 Strength = 3 AtkMelee
        private double CapStr { get; } = 50;
        public double End { get; set; } // 1 Endurance = 15 LP
        private double CapEnd { get; } = 50;
        public double Dex { get; set; } // 1 Dexterity = 3 AtkPierce
        private double CapDex { get; } = 50;
        public double Int { get; set; } // 1 Intelligence = 3 AtkMagic 
        private double CapInt { get; } = 50;
        public double Crit { get; set; } // 1 Crit = 1% Chance for Crit
        private double CapCrit { get; } = 30;
        public double CritDmg { get; set; } // CritDmg = Dmg + (CritDmg * Dmg / 100)
        private double CapCritDmg { get; } = 50;

        public Item? Mainhand { get; set; }
        public Item? Offhand { get; set; }
        public Item? Head { get; set; }
        public Item? Shoulder { get; set; }
        public Item? Chest { get; set; }
        public Item? Trousers { get; set; }
        public Item? Feet { get; set; }
    }
}
