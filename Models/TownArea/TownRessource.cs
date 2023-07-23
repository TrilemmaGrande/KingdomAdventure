using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class TownRessource
    {
        public int TownRessourceID { get; set; }
        public int Amount { get; set; }
     
        // Foreign key
        public int TownID { get; set; }

        // Navigation property
        public PlayerTown PlayerTown { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public Ressource Ressource { get; set; }
    }
}