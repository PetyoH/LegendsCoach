namespace LegendsCoach.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data;
    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Data.Repositories;
    using LegendsCoach.Services.Data.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PositionServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Position> positionRepository;
        private PositionService positionService;

        public PositionServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryPosition")
                .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.positionRepository = new EfDeletableEntityRepository<Position>(this.applicationDbContext);
            this.positionService = new PositionService(this.positionRepository);
        }

        [Fact]
        public async Task GetPositionAsyncWorksRight()
        {
            await this.SeedAsync();

            var position = await this.positionService.GetPositionAsync("Test1");

            Assert.NotNull(position);
            Assert.Equal("Test1", position.Name);
            Assert.Equal(1, position.Id);
        }

        [Fact]
        public async Task GetPositinosAsyncWorksRight()
        {
            await this.SeedAsync();

            var positions = await this.positionService.GetPositionsAsync();

            Assert.NotNull(positions);
            Assert.Equal(2, positions.Count());
        }

        private async Task SeedAsync()
        {
            var position1 = new Position
            {
                Id = 1,
                Name = "Test1",
            };

            var position2 = new Position
            {
                Id = 2,
                Name = "Test2",
            };

            await this.positionRepository.AddAsync(position1);
            await this.positionRepository.AddAsync(position2);

            await this.positionRepository.SaveChangesAsync();
        }
    }
}
