namespace KingdomAdventure.Models.TownArea
{
    public class Town
    {
        public int TownID { get; set; }
        public string TownName { get; set; }
        public Dictionary<TownBuilding,int> TownBuildings { get; set; }
        public Dictionary<TownRessource,int> TownRessources { get; set; }
        public Dictionary<TownSoldier, int> TownDefenseArmy { get; set; }
        public Dictionary<TownSoldier, int> AreaDefenseArmy { get; set; }
    }
}
