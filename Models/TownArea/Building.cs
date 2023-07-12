namespace KingdomAdventure.Models.TownArea
{
    public class Building
    {
        public int BuildingID { get; set; }
        public string BuildingName { get; set; }
        public int Level { get; set; }
        public Dictionary<Ressource,int> BuildingCost { get; set; }
        public Dictionary<Ressource,int> ProduceRessource { get; set; }
        public Dictionary<TownSoldier,int> ProduceSoldier { get; set; }
    }
}