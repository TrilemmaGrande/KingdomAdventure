using KingdomAdventure.Models.WorldArea;
using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class TownRessource
    {
        public int TownRessourceID { get; set; }
        public double Amount { get; set; }
        public double ProduceInTimestep { get; set; }

        // Foreign key
        public int PlayerTownID { get; set; }

        // Navigation property
        [JsonIgnore]
        public PlayerTown PlayerTown { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public Ressource Ressource { get; set; }
    }
}