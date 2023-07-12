namespace KingdomAdventure.Models.TownArea
{
    public class Town
    {
        public int TownID { get; set; }
        public string TownName { get; set; }
        public Dictionary<TownBuilding,int> TownBuildings { get; set; }
        public Dictionary<TownRessource,int> TownRessources { get; set; }
    }
}
