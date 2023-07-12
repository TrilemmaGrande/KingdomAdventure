using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class Soldier
    {
        public int SoldierID { get; set; }
        public string? SoldierName { get; set; }
        public int? Level { get; set; }
        public double? Experience { get; set; }
        public double? CurrentLP { get; set; }
        public double? FullLP { get; set; }
        public double? AtkMelee { get; set; }
        public double? AtkPierce { get; set; }
        public double? AtkMagic { get; set; }
        public double? DefMelee { get; set; }
        public double? DefPierce { get; set; }
        public double? DefMagic { get; set; }
    }
}