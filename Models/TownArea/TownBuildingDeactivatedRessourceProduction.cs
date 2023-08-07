using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class TownBuildingDeactivatedRessourceProduction
    {
        public int TownBuildingDeactivatedRessourceProductionID { get; set; }
        // Foreign key
        public int TownBuildingID { get; set; }

        // Navigation property
        [JsonIgnore]
        public TownBuilding? TownBuilding { get; set; }
      
        // Foreign key
        public int? BuildingRessourceProducingID { get; set; }

        // Navigation property
        public BuildingRessourceProducing? ProducingRessource { get; set; }
    }
}