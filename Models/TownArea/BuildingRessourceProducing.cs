using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class BuildingRessourceProducing
    {
        public int BuildingRessourceProducingID { get; set; }
        public int Amount { get; set; }
        public bool ProduceOnce { get; set; } = false;
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