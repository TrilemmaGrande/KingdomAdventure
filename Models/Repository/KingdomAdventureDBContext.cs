﻿using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.EntityFrameworkCore;

namespace KingdomAdventure.Models.Repository
{
    public class KingdomAdventureDBContext : DbContext
    {
        public KingdomAdventureDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Building> Building { get; set; }
        public DbSet<BuildingRessourceConsuming> BuildingRessourceConsuming { get; set; }
        public DbSet<BuildingRessourceCost> BuildingRessourceCost { get; set; }
        public DbSet<BuildingRessourceProducing> BuildingRessourceProducing { get; set; }
        public DbSet<BuildingSoldierProducing> BuildingSoldierProducing { get; set; }
        public IQueryable<Worker> Worker { get; set; }
        public DbSet<Ressource> Ressource { get; set; }
        public DbSet<Soldier> Soldier { get; set; }
        public DbSet<PlayerTown> PlayerTown { get; set; }
        public DbSet<TownBuilding> TownBuilding { get; set; }
        public DbSet<TownRessource> TownRessource { get; set; }
        public DbSet<TownSoldier> TownSoldier { get; set; }
        public DbSet<EnemyNPC> EnemyNPC { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryItem> InventoryItem { get; set; }
        public DbSet<InventoryItemUpgrade> InventoryItemUpgrade { get; set; }
        public DbSet<InventoryUpgrade> InventoryUpgrade { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerEnemyNPC> PlayerEnemyNPC { get; set; }
        public DbSet<Upgrade> Upgrade { get; set; }
    }
}