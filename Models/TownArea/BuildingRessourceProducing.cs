using KingdomAdventure.Models.WorldArea;
using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class BuildingRessourceProducing
    {
        public int BuildingRessourceProducingID { get; set; }

        public double ProduceInMinute { get; set; }

        // Foreign key
        public int BuildingID { get; set; }

        // Navigation property
        [JsonIgnore]
        public Building Building { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public Ressource Ressource { get; set; }
    }
}