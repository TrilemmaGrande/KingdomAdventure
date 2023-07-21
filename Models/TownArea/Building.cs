using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace KingdomAdventure.Models.TownArea
{
    public enum EBuildingName
    {
        Tent,
        House,
        Castle,
        Storage,
        Lumber,
        Hunter,
        Mine,
        Quarry,
        Windmill,
        Smith
    }
    public class Building
    {
        public int BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public EBuildingName EBuildingName { get; set; }
        public int WorkersMaxTemplate { get; set; }
        public List<BuildingRessourceConsuming>? ConsumingRessources { get; set; } = new List<BuildingRessourceConsuming>();
        public List<BuildingRessourceProducing>? ProducingRessources { get; set; } = new List<BuildingRessourceProducing>();
        public List<BuildingSoldierProducing>? ProducingSoldiers { get; set; } = new List<BuildingSoldierProducing>();
        public List<BuildingRessourceCost>? BuildingRessourcesCosts { get; set; } = new List<BuildingRessourceCost>();
    }
}