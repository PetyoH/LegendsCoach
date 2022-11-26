namespace LegendsCoach.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;

    public class RanksSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Ranks.Any())
            {
                return;
            }

            await dbContext.Ranks.AddAsync(new Rank { Name = "Iron" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Bronze" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Silver" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Gold" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Platinum" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Diamond" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Master" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Grandmaster" });
            await dbContext.Ranks.AddAsync(new Rank { Name = "Challenger" });

            await dbContext.SaveChangesAsync();
        }
    }
}
