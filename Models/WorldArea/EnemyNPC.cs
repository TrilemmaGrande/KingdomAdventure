namespace KingdomAdventure.Models.WorldArea
{
    public class EnemyNPC
    {
        public bool Boss { get; set; }
        public string EnemyNPCName { get; set; }
        public int Level { get; set; }
        public double CurrentLP { get; set; }
        public double FullLP { get; set; }
        public double? AtkMelee { get; set; }
        public double? AtkPierce { get; set; }
        public double? AtkMagic { get; set; }
        public double? DefMelee { get; set; }
        public double? DefPierce { get; set; }
        public double? DefMagic { get; set; }
        public double? Crit { get; set; } // 1 Crit = 1% Chance for Crit
        public double? CritDmg { get; set; } // CritDmg = Dmg + (CritDmg * Dmg / 100)
    }
}
