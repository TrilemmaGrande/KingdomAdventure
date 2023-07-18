namespace KingdomAdventure.Models.TownArea
{
    public class BuildingRessourceProduced
    {
        public int BuildingRessourceProducedID { get; set; }
        public int Amount { get; set; }
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