using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class Worker
    {
        public int TownWorkerID { get; set; }
        public bool IsProducing { get; set; }
        public bool IsAssigned { get; set; }
        public List<BuildingRessourceProducing>? ProducingInBuilding { get; set; } = new List<BuildingRessourceProducing>();
        public List<BuildingRessourceConsuming>? ConsumingInBuilding { get; set; } = new List<BuildingRessourceConsuming>();

        // Foreign key
        public int PlayerTownID { get; set; }

        // Navigation property
        [JsonIgnore]
        public PlayerTown PlayerTown { get; set; }

        // Foreign key
        public int? TownBuildingID { get; set; }

        // Navigation property
        [JsonIgnore]
        public TownBuilding TownBuilding { get; set; }

        // Foreign key
        public int? TownRessourceID { get; set; }

        // Navigation property
        [JsonIgnore]
        public TownRessource TownRessource { get; set; }
    }
}
