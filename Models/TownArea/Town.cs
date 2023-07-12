namespace KingdomAdventure.Models.TownArea
{
    public class Town
    {
        public string TownName { get; set; }
        public List<TownBuilding> TownBuildings = new List<TownBuilding>()
    }
}
