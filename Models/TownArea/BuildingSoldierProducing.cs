using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class BuildingSoldierProducing
    {
        public int BuildingSoldierProducingID { get; set; }

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