﻿using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.TownArea
{
    public class TownRessource
    {
        public int TownRessourceID { get; set; }
        public double Amount { get; set; }
        public double ProducingInMinute { get; set; }

        // Foreign key
        public int PlayerTownID { get; set; }

        // Navigation property
        public PlayerTown PlayerTown { get; set; }

        // Foreign key
        public int RessourceID { get; set; }

        // Navigation property
        public Ressource Ressource { get; set; }
    }
}