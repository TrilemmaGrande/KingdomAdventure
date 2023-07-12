using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class TownSoldierDefending
    {
        public int TownSoldierDefendingID { get; set; }
        public int Amount { get; set; }

        // Foreign key
        public int TownID { get; set; }

        // Navigation property
        public Town Town { get; set; }

        // Foreign key
        public int SoldierID { get; set; }

        // Navigation property
        public Soldier Soldier { get; set; }
    }
}