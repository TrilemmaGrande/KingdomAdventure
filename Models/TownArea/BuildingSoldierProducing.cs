using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class BuildingSoldierProducing
    {
        public int BuildingSoldierProducingID { get; set; }
        public int? Level { get; set; }
        public double? Experience { get; set; }
        public double? CurrentLP { get; set; }
        public int Amount { get; set; }

        // Foreign key
        public int BuildingID { get; set; }

        // Navigation property
        public Building Building { get; set; }

        // Foreign key
        public int SoldierID { get; set; }

        // Navigation property
        public Soldier Soldier { get; set; }
    }
}