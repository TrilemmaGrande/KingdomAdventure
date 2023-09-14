using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace KingdomAdventure.Models.TownArea
{
    public class TownBuilding
    {
        public int TownBuildingID { get; set; }
        public int Level { get; set; } = 1;
        public int Storage { get; set; } = 0;
        public int PopulationMax { get; set; } = 0;
        public int WorkersMax { get; set; }
        public List<Worker>? Workers { get; set; } = new List<Worker>();

        // Foreign key
        public int PlayerTownID { get; set; }

        // Navigation property
        [JsonIgnore]
        public PlayerTown PlayerTown{ get; set; }

        // Foreign key
        public int BuildingID { get; set; }

        // Navigation property
        public Building Building { get; set; }
    }
}