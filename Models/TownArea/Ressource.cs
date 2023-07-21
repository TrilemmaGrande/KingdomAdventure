namespace KingdomAdventure.Models.TownArea
{
    public enum ERessourceName
    {
        PopulationMax,
        Storage,
        Food,
        Wood,
        Stone,
        Cloth,
        Iron,
        Jewelry,
        Gold
    }
    public class Ressource
    {
        public int RessourceID { get; set; }
        public string? RessourceName { get; set; }
        public ERessourceName ERessourceName { get; set; }
        public double? RessourceValue { get; set; }
    }
}