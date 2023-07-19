namespace KingdomAdventure.Models.TownArea
{
    public class TownBuildingRessourceConsumed
    {
        public int TownBuildingRessourceConsumedID { get; set; }
        public int Amount { get; set; } = 0;
        // Foreign key
        public int TownBuildingID { get; set; }

        // Navigation property
        public TownBuilding TownBuilding { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public Ressource Ressource { get; set; }
    }
}