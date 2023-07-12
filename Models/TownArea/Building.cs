using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomAdventure.Models.TownArea
{
    public class Building
    {
        public int BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public List<TownRessource>? BuildingCost { get; set; } = new List<TownRessource>();
        public List<TownRessource>? ProduceRessource { get; set; } = new List<TownRessource>();
        public List<TownSoldierAttacking>? ProduceSoldierAttacking { get; set; } = new List<TownSoldierAttacking>();
        public List<TownSoldierDefending>? ProduceSoldierDefending { get; set; } = new List<TownSoldierDefending>();
    }
}