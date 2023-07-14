using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomAdventure.Models.TownArea
{
    public class Building
    {
        public int BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public int Population { get; set; }
        public List<BuildingRessourceConsuming>? ConsumingRessources { get; set; } = new List<BuildingRessourceConsuming>();
        public List<BuildingRessourceProducing>? ProducingRessources { get; set; } = new List<BuildingRessourceProducing>();
        public List<BuildingSoldierProducing>? ProducingSoldiers { get; set; } = new List<BuildingSoldierProducing>();
        public List<BuildingRessourceCost>? BuildingRessourcesCosts { get; set; } = new List<BuildingRessourceCost>();
    }
}