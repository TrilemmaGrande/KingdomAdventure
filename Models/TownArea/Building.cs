using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace KingdomAdventure.Models.TownArea
{
    public enum EBuildingName
    {
        Tent,
        House,
        Mansion,
        Storage,
        Lumber,
        Hunter,
        Mine,
        Quarry,
        Farm,
        Smith,
        Tanner,
        Fletcher,
        Weaver,
        RitualFountain,
        MageGuild,
        Barracks,
        Archery
    }
    public class Building
    {
        public int BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public int AvailableInStage { get; set; }
        public bool LockedInStage { get; set; } = false;
        public EBuildingName EBuildingName { get; set; }
        public int WorkersMaxTemplate { get; set; }
        public int PopulationMaxTemplate { get; set; }
        public int StorageMaxTemplate { get; set; }
        public string Icon { get; set; }
        public List<BuildingRessourceConsuming>? ConsumingRessources { get; set; } = new List<BuildingRessourceConsuming>();
        public List<BuildingRessourceProducing>? ProducingRessources { get; set; } = new List<BuildingRessourceProducing>();
        public List<BuildingSoldierProducing>? ProducingSoldiers { get; set; } = new List<BuildingSoldierProducing>();
        public List<BuildingRessourceCost>? RessourceCost { get; set; } = new List<BuildingRessourceCost>();
    }
}