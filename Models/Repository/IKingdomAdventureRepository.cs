using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.Repository
{
    public interface IKingdomAdventureRepository
    {
        public IQueryable<Player> Players { get; }
        public IQueryable<Inventory> Inventories { get; }
        public IQueryable<EnemyNPC> EnemyNPCs { get; }
        public IQueryable<Item> Items { get; }
        public IQueryable<UpgradeItem> UpgradeItems { get; }
        public IQueryable<Account> Accounts { get; }
    }
}
