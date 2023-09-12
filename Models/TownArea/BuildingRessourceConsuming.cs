using KingdomAdventure.Models.WorldArea;
using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class BuildingRessourceConsuming
    {
        public int BuildingRessourceConsumingID { get; set; }

        public double ConsumeInMinute { get; set; }
        public bool ConsumeOnce { get; set; }

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