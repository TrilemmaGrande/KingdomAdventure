using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class TownBuilding
    {
        public int TownBuildingID { get; set; }
        public int WorkersMax { get; set; }
        public int Workers { get; set; } = 0;
        public int Level { get; set; } = 1;
        public List<TownBuildingRessourceConsumed>? RessourcesConsumed { get; set; } = new List<TownBuildingRessourceConsumed>();
        public List<TownBuildingRessourceProduced>? RessourcesProduced { get; set; } = new List<TownBuildingRessourceProduced>();
        public List<TownBuildingSoldierProduced>? SoldiersProduced { get; set; } = new List<TownBuildingSoldierProduced>();

        // Foreign key
        public int PlayerTownID { get; set; }

        // Navigation property
        public PlayerTown PlayerTown{ get; set; }

        // Foreign key
        public int BuildingID { get; set; }

        // Navigation property
        public Building Building { get; set; }
    }
}