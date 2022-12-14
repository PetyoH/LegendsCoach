using LegendsCoach.Data.Common.Repositories;
using LegendsCoach.Data.Models;
using LegendsCoach.Data.Repositories;
using LegendsCoach.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LegendsCoach.Web.ViewModels.ApplicationUser;
using LegendsCoach.Web.ViewModels.Player;
using LegendsCoach.Services.Mapping;
using System.Reflection;
using System.Numerics;
using Moq;
using static System.Net.Mime.MediaTypeNames;

namespace LegendsCoach.Services.Data.Tests
{
    public class PlayerServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Player> playerRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private IDeletableEntityRepository<Coach> coachRepository;
        private IDeletableEntityRepository<Position> positionRepository;
        private IDeletableEntityRepository<Rank> rankRepository;
        private PlayerService playerService;

        public PlayerServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryPlayer")
                .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.playerRepository = new EfDeletableEntityRepository<Player>(this.applicationDbContext);
            this.applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(this.applicationDbContext);
            this.coachRepository = new EfDeletableEntityRepository<Coach>(this.applicationDbContext);
            this.rankRepository = new EfDeletableEntityRepository<Rank>(this.applicationDbContext);
            this.positionRepository = new EfDeletableEntityRepository<Position>(this.applicationDbContext);
            this.playerService = new PlayerService(this.playerRepository, this.coachRepository);
        }

        [Fact]
        public async Task AddPlayerAsyncWorksRight()
        {
            var player = new RegisterViewModel
            {
                GameName = "Test0",
                Level = 0,
                RankId = 0,
                PositionId = 0,
                Description = "Description0",
            };

            await this.playerService.AddPlayerAsync(player, "0");

            var playerFromDb = this.playerRepository.All().FirstOrDefault();

            Assert.Equal(player.GameName, playerFromDb.GameName);
        }

        [Fact]
        public async Task GetAllAsyncWorksRight()
        {
            await this.SeedAsync();

            AutoMapperConfig.RegisterMappings(typeof(PlayerInListViewModel).GetTypeInfo().Assembly);

            var players = await this.playerService.GetAllAsync<PlayerInListViewModel>(1, 2);

            var playersFromDb = this.playerRepository.All();

            Assert.NotEmpty(players);
            Assert.Equal(playersFromDb.Count(), players.Count());
        }

        [Fact]
        public async Task GetCurrentPlayerAsyncWorksRight()
        {
            await this.SeedAsync();

            const string userId = "1";

            var player = await this.playerService.GetCurrentPlayerAsync(userId);

            Assert.Equal("1", player.Id);
            Assert.Equal("Test1", player.GameName);
        }

        [Fact]
        public async Task GetCountAsyncWorksRight()
        {
            await this.SeedAsync();

            var count = await this.playerService.GetCountAsync();

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetPlayerDetailsAsyncWorksRight()
        {
            await this.SeedAsync();

            const string playerId = "1";

            AutoMapperConfig.RegisterMappings(typeof(PlayerDetailsViewModel).GetTypeInfo().Assembly);

            var player = await this.playerService.GetPlayerDetailsAsync<PlayerDetailsViewModel>(playerId);

            var dbPlayer = await this.playerRepository.All()
                .FirstOrDefaultAsync(p => p.Id == playerId);

            Assert.Equal(dbPlayer.GameName, player.GameName);
        }

        //[Fact]
        //public async Task AddCoachWorksRight()
        //{
        //    await this.SeedAsync();

        //    string playerId = "1";

        //    await this.playerService.AddCoach(playerId);

        //    var player = await this.playerRepository.All()
        //        .FirstOrDefaultAsync(p => p.Id == playerId);

        //    Assert.NotNull(player);
        //}

        //[Fact]
        //public async Task UpdatePlayerAsyncWorksRight()
        //{
        //    var user1 = new ApplicationUser
        //    {
        //        Id = "TestId",
        //        UserName = "Test",
        //        Email = "TestEmail",
        //    };

        //    //await this.applicationUserRepository.AddAsync(user1);
        //    //await this.applicationUserRepository.SaveChangesAsync();

        //    var rank1 = new Rank
        //    {
        //        Id = 1,
        //        Name = "Rank1",
        //    };

        //    //await this.rankRepository.AddAsync(rank1);
        //    //await this.rankRepository.SaveChangesAsync();

        //    var position1 = new Position
        //    {
        //        Id = 1,
        //        Name = "Position1",
        //    };

        //    //await this.positionRepository.AddAsync(position1);
        //    //await this.positionRepository.SaveChangesAsync();

        //    var player1 = new Player
        //    {
        //        Id = "TestPlayer",
        //        GameName = "Test1",
        //        Level = 5,
        //        RankId = rank1.Id,
        //        Rank = rank1,
        //        PositionId = position1.Id,
        //        Position = position1,
        //        UserId = user1.Id,
        //        User = user1,
        //        Description = "Description1",
        //    };

        //    //await this.playerRepository.AddAsync(player1);
        //    //await this.playerRepository.SaveChangesAsync();


        //    await this.applicationDbContext.Ranks.AddAsync(rank1);
        //    await this.applicationDbContext.Positions.AddAsync(position1);
        //    await this.applicationDbContext.Users.AddAsync(user1);

        //    await this.applicationDbContext.Players.AddAsync(player1);
        //    await this.applicationDbContext.SaveChangesAsync();

        //    var model = new PlayerEditViewModel
        //    {
        //        GameName = "ChangedName",
        //        Level = 333,
        //        Description = "Changed",
        //        RankId = rank1.Id,
        //        PositionId = position1.Id,
        //    };

        //    await this.playerService.UpdatePlayerAsync(model, user1.Id, player1.Id);

        //    var player = await this.playerRepository.All()
        //        .FirstOrDefaultAsync(p => p.Id == player1.Id);

        //    Assert.Equal("ChangedName", player.GameName);
        //    Assert.Equal(333, player.Level);
        //}

        [Fact]
        public async Task GetPlayerAsyncWorksRight()
        {
            await this.SeedAsync();

            const string playerId = "1";

            var player = await this.playerService.GetPlayerAsync(playerId);

            var playerDb = await this.playerRepository.All()
                .FirstOrDefaultAsync(p => p.Id == playerId);

            Assert.NotNull(player);
            Assert.Equal(playerDb.Id, player.Id);
            Assert.Equal(playerDb.GameName, player.GameName);
        }

        [Fact]
        public async Task GetPlayerIdAsyncWorksRight()
        {
            await this.SeedAsync();

            const string userId = "1";

            var playerId = await this.playerService.GetPlayerIdAsync(userId);

            var playerDb = await this.playerRepository.All()
                .FirstOrDefaultAsync(p => p.Id == userId);

            Assert.NotNull(playerId);
            Assert.Equal(playerDb.Id, playerId);
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
                Rank = rank1,
                Position = position1,
                UserId = "1",
                CoachId = null,
                Description = "Description1",
            };

            var player2 = new Player
            {
                Id = "2",
                GameName = "Test2",
                Level = 10,
                Rank = rank2,
                Position = position2,
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
