using Microsoft.EntityFrameworkCore;

namespace KingdomAdventure.Models
{
    public class KingdomAdventureDBContext : DbContext
    {
        public KingdomAdventureDBContext(DbContextOptions options) : base(options)
        {
        }

    }
}