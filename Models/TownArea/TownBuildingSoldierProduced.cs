namespace KingdomAdventure.Models.TownArea
{
    public class TownBuildingSoldierProduced
    {
        public int TownBuildingSoldierProducedID { get; set; }
        public int Amount { get; set; } = 0;
        public double ProducedBetweenInterval { get; set; } = 0.0;
        // Foreign key
        public int TownBuildingID { get; set; }

        // Navigation property
        public TownBuilding TownBuilding { get; set; }

        // Foreign key
        public int SoldierID { get; set; }

        // Navigation property
        public Soldier Soldier { get; set; }
    }
}