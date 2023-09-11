using KingdomAdventure.Models.WorldArea;

public enum ESoldierName
{
    Archer,
    Warrior,
    Mage
}

namespace KingdomAdventure.Models.TownArea
{
    public class Soldier
    {
        public int SoldierID { get; set; }
        public string? SoldierName { get; set; }
        public ESoldierName ESoldierName { get; set; }
        public string Icon { get; set; }
        public double FullLP { get; set; }
        public double AtkMelee { get; set; }
        public double AtkPierce { get; set; }
        public double AtkMagic { get; set; }
        public double DefMelee { get; set; }
        public double DefPierce { get; set; }
        public double DefMagic { get; set; }
    }
}