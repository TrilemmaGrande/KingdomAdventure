using KingdomAdventure.Models.WorldArea;
using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class BuildingSoldierProducing
    {
        public int BuildingSoldierProducingID { get; set; }

        // Foreign key
        public int BuildingID { get; set; }

        // Navigation property
        [JsonIgnore]
        public Building Building { get; set; }

        // Foreign key
        public int SoldierID { get; set; }

        // Navigation property
        public Soldier Soldier { get; set; }
    }
}