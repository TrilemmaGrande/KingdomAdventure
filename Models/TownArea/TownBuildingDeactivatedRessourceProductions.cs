namespace KingdomAdventure.Models.TownArea
{
    public class DeactivatedRessourceProductions
    {
        public int DeactivatedRessourceProductionsID { get; set; }
        // Foreign key
        public int TownBuildingID { get; set; }

        // Navigation property
        public TownBuilding TownBuilding { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public BuildingRessourceProducing RessourceProducing { get; set; }
    }
}