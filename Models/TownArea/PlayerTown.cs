using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class PlayerTown
    {
        public int PlayerTownID { get; set; }

        // Foreign key
        public int PlayerID { get; set; }

        // Navigation property
        public Player Player { get; set; }
        public int Stage { get; set; } = 1;
        public int PopulationMax { get; set; }
        public int SoldiersMax { get; set; } = 50;
        public string? TownName { get; set; }
        public int Storage { get; set; } = 20;
        public DateTime LastUpdated { get; set; } 
        public List<TownBuilding>? TownBuildings { get; set; } = new List<TownBuilding>();
        public List<TownRessource>? TownRessources { get; set; } = new List<TownRessource>();
        public List<TownSoldier>? TownSoldiers { get; set; } = new List<TownSoldier>();
        public List<Worker>? Workers { get; set; } = new List<Worker>();
    }
}
