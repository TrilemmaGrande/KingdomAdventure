using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class TownRessource
    {
        public int TownRessourceID { get; set; }
        public int Amount { get; set; }
        public double ProducedBetweenInterval { get; set; } = 0.0;
        public double ConsumedBetweenInterval { get; set; } = 0.0;
        // Foreign key
        public int TownID { get; set; }

        // Navigation property
        public Town Town { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public Ressource Ressource { get; set; }
    }
}