﻿using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class TownBuilding
    {
        public int TownBuildingID { get; set; }
        public int Amount { get; set; } = 1;
        public int Level { get; set; } = 1;

        // Foreign key
        public int TownID { get; set; }

        // Navigation property
        public Town Town{ get; set; }

        // Foreign key
        public int BuildingID { get; set; }

        // Navigation property
        public Building Building { get; set; }
    }
}