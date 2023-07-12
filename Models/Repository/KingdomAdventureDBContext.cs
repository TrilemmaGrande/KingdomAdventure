using KingdomAdventure.Models.WorldArea;
using Microsoft.EntityFrameworkCore;

namespace KingdomAdventure.Models.Repository
{
    public class KingdomAdventureDBContext : DbContext
    {
        public KingdomAdventureDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Player> Player { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryItem> InventoryItem { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<UpgradeItem> UpgradeItem { get; set; }
        public DbSet<EnemyNPC> EnemyNPC { get; set; }
    }
}