namespace LegendsCoach.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data;
    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Data.Repositories;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Services.Mapping;
    using LegendsCoach.Web.ViewModels.Coach;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CoachServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Player> playerRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private IDeletableEntityRepository<Coach> coachRepository;
        private IDeletableEntityRepository<Position> positionRepository;
        private IDeletableEntityRepository<Rank> rankRepository;
        private CoachService coachService;

        public CoachServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryCoach")
                .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.playerRepository = new EfDeletableEntityRepository<Player>(this.applicationDbContext);
            this.applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(this.applicationDbContext);
            this.coachRepository = new EfDeletableEntityRepository<Coach>(this.applicationDbContext);
            this.rankRepository = new EfDeletableEntityRepository<Rank>(this.applicationDbContext);
            this.positionRepository = new EfDeletableEntityRepository<Position>(this.applicationDbContext);
            this.coachService = new CoachService(this.playerRepository);
        }

        [Fact]
        public async Task GetAllWithCoachInfoAsyncWorksRight()
        {
            await this.SeedAsync();

            var coachesCount = await this.coachService.GetCountAsync();

            Assert.Equal(1, coachesCount);
        }

        [Fact]
        public async Task GetAllAsyncWorksRight()
        {
            await this.SeedAsync();

            AutoMapperConfig.RegisterMappings(typeof(CoachInListViewModel).GetTypeInfo().Assembly);

            var coaches = await this.coachService.GetAllAsync<CoachInListViewModel>(1, 2);

            Assert.NotEmpty(coaches);
        }

        private async Task SeedAsync()
        {
            var user1 = new ApplicationUser()
            {
                Id = "1",
            };

            var user2 = new ApplicationUser()
            {
                Id = "2",
            };

            await this.applicationUserRepository.AddAsync(user1);
            await this.applicationUserRepository.AddAsync(user2);

            await this.applicationUserRepository.SaveChangesAsync();

            var rank1 = new Rank
            {
                Id = 1,
                Name = "Rank1",
            };

            var rank2 = new Rank
            {
                Id = 2,
                Name = "Rank2",
            };

            await this.rankRepository.AddAsync(rank1);
            await this.rankRepository.AddAsync(rank2);

            await this.rankRepository.SaveChangesAsync();

            var position1 = new Position
            {
                Id = 1,
                Name = "Position1",
            };

            var position2 = new Position
            {
                Id = 2,
                Name = "Position2",
            };

            await this.positionRepository.AddAsync(position1);
            await this.positionRepository.AddAsync(position2);

            await this.positionRepository.SaveChangesAsync();

            var player1 = new Player
            {
                Id = "1",
                GameName = "Test1",
                Level = 5,
                RankId = 1,
                PositionId = 1,
                UserId = "1",
                CoachId = "1",
                Description = "Description1",
            };

            var player2 = new Player
            {
                Id = "2",
                GameName = "Test2",
                Level = 10,
                RankId = 2,
                PositionId = 2,
                UserId = "2",
                CoachId = null,
                Description = "Description2",
            };

            await this.playerRepository.AddAsync(player1);
            await this.playerRepository.AddAsync(player2);

            await this.playerRepository.SaveChangesAsync();
        }
    }
}
