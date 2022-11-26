namespace LegendsCoach.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;

    public class PositionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Positions.Any())
            {
                return;
            }

            await dbContext.AddAsync(new Position { Name = "Top", Description = "Hello, I play top!" });
            await dbContext.AddAsync(new Position { Name = "Jungle", Description = "Hello, I play jungle!" });
            await dbContext.AddAsync(new Position { Name = "Mid", Description = "Hello, I play mid!" });
            await dbContext.AddAsync(new Position { Name = "Bot", Description = "Hello, I play bot!" });
            await dbContext.AddAsync(new Position { Name = "Support", Description = "Hello, I play support!" });

            await dbContext.SaveChangesAsync();
        }
    }
}
