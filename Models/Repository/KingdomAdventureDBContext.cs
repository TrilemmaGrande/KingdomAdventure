using Microsoft.EntityFrameworkCore;

namespace KingdomAdventure.Models.Repository
{
    public class KingdomAdventureDBContext : DbContext
    {
        public KingdomAdventureDBContext(DbContextOptions options) : base(options)
        {
        }

    }
}