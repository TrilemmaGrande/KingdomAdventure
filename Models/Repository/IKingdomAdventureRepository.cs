using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.Repository
{
    public interface IKingdomAdventureRepository
    {
        public IQueryable<Player> Player { get; }
        public IQueryable<Inventory> Inventory { get; }
        public IQueryable<EnemyNPC> EnemyNPCs { get; }
        public IQueryable<Item> Items { get; }
        public IQueryable<UpgradeItem> UpgradeItems { get; }
    }
}
