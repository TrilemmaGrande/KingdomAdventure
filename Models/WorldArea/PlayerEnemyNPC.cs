﻿namespace KingdomAdventure.Models.WorldArea
{
    public class PlayerEnemyNPC
    {
        public int PlayerEnemyNPCID { get; set; }
        public int? Level { get; set; }
        public double? CurrentLP { get; set; }

        // Foreign key
        public int PlayerID { get; set; }

        // Navigation property
        public Player Player { get; set; }

        // Foreign key
        public int EnemyNPCID { get; set; }

        // Navigation property
        public EnemyNPC EnemyNPC { get; set; }
    }
}
