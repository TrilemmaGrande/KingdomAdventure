namespace KingdomAdventure.Models.TownArea
{
    public class TownBuildingDeactivatedRessourceProduction
    {
        public int TownBuildingDeactivatedRessourceProductionID { get; set; }
        // Foreign key
        public int TownBuildingID { get; set; }

        // Navigation property
        public TownBuilding TownBuilding { get; set; }

        // Foreign key
        public int BuildingRessourceProducingID { get; set; }

        // Navigation property
        public BuildingRessourceProducing RessourceProducing { get; set; }
    }
}