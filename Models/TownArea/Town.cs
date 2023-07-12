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
        public List<TownBuilding>? TownBuildings { get; set; } = new List<TownBuilding>();
        public List<TownRessource>? TownRessources { get; set; } = new List<TownRessource>();
        public List<TownSoldierAttacking>? TownSoldiersAttacking { get; set; } = new List<TownSoldierAttacking>();
        public List<TownSoldierDefending>? TownSoldiersDefending { get; set; } = new List<TownSoldierDefending>();
    }
}
