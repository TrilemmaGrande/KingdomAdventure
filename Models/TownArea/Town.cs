using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class Town
    {
        public int TownID { get; set; }

        // Foreign key
        public int PlayerID { get; set; }

        // Navigation property
        public Player Player { get; set; }

        public string? TownName { get; set; }
        public int WorkplacesUsed { get; set; }
        public DateTime LastUpdated { get; set; } 
        public List<TownBuilding>? TownBuildings { get; set; } = new List<TownBuilding>();
        public List<TownRessource>? TownRessources { get; set; } = new List<TownRessource>();
        public List<TownSoldier>? TownSoldiers { get; set; } = new List<TownSoldier>();
    }
}
