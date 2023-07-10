﻿using KingdomAdventure.Models.WorldArea;

namespace KingdomAdventure.Models.Repository
{
    public class EFKingdomAdventureRepository : IKingdomAdventureRepository
    {
        private KingdomAdventureDBContext ctx;

        public EFKingdomAdventureRepository(KingdomAdventureDBContext ctx)
        {
            this.ctx = ctx;
        }
        public IQueryable<Player> Players => ctx.Player;
        public IQueryable<Inventory> Inventories => ctx.Inventory;
        public IQueryable<EnemyNPC> EnemyNPCs => ctx.EnemyNPCs;
        public IQueryable<Item> Items => ctx.Items;
        public IQueryable<UpgradeItem> UpgradeItems => ctx.UpgradeItems;
        public IQueryable<Account> Accounts => ctx.Accounts;
    }
}
