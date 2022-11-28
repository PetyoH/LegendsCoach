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

            await dbContext.AddAsync(new Position { Name = "Top" });
            await dbContext.AddAsync(new Position { Name = "Jungle" });
            await dbContext.AddAsync(new Position { Name = "Mid" });
            await dbContext.AddAsync(new Position { Name = "Bot" });
            await dbContext.AddAsync(new Position { Name = "Support" });

            await dbContext.SaveChangesAsync();
        }
    }
}
