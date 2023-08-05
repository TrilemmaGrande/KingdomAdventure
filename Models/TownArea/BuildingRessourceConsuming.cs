using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class BuildingRessourceConsuming
    {
        public int BuildingRessourceConsumingID { get; set; }

        public double ConsumeInMinute { get; set; }

        // Foreign key
        public int BuildingID { get; set; }

        // Navigation property
        public Building Building { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public Ressource Ressource { get; set; }
    }
}