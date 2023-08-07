using KingdomAdventure.Models.WorldArea;
using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class TownSoldier
    {
        public int TownSoldierID { get; set; }
        public double CurrentLP { get; set; }

        // Foreign key
        public int PlayerTownID { get; set; }

        // Navigation property
        [JsonIgnore]
        public PlayerTown PlayerTown { get; set; }

        // Foreign key
        public int SoldierID { get; set; }

        // Navigation property
        public Soldier Soldier { get; set; }
    }
}