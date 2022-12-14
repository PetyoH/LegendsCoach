namespace LegendsCoach.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using LegendsCoach.Data;
    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RankServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Rank> rankRepository;
        private RankService rankService;

        public RankServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryRank")
                .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.rankRepository = new EfDeletableEntityRepository<Rank>(this.applicationDbContext);
            this.rankService = new RankService(this.rankRepository);
        }

        [Fact]
        public async Task GetRankAsyncWorksRight()
        {
            await this.SeedAsync();

            var rank = await this.rankService.GetRankAsync("Test1");

            Assert.NotNull(rank);
            Assert.Equal("Test1", rank.Name);
            Assert.Equal(1, rank.Id);
        }

        [Fact]
        public async Task GetRanskAsyncWorksRight()
        {
            await this.SeedAsync();

            var ranks = await this.rankService.GetRanksAsync();

            Assert.NotNull(ranks);
            Assert.Equal(2, ranks.Count());
        }

        private async Task SeedAsync()
        {
            var rank1 = new Rank
            {
                Id = 1,
                Name = "Test1",
            };

            var rank2 = new Rank
            {
                Id = 2,
                Name = "Test2",
            };

            await this.rankRepository.AddAsync(rank1);
            await this.rankRepository.AddAsync(rank2);

            await this.rankRepository.SaveChangesAsync();
        }
    }
}
