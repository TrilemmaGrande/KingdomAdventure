using Microsoft.EntityFrameworkCore;

namespace KingdomAdventure.Models
{
    public static class KingdomAdventureSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KingdomAdventureDBContext dbContext = app
              .ApplicationServices
              .CreateScope()
              .ServiceProvider
              .GetRequiredService<KingdomAdventureDBContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();

            }
        }
    }
}
