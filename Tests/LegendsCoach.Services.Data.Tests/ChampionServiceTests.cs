namespace LegendsCoach.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data;
    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Data.Repositories;
    using LegendsCoach.Services.Mapping;
    using LegendsCoach.Web.ViewModels.Champion;
    using LegendsCoach.Web.ViewModels.Coach;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ChampionServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private IDeletableEntityRepository<Champion> championRepository;
        private IDeletableEntityRepository<Image> imageRepository;
        private IDeletableEntityRepository<Player> playerRepository;
        private IDeletableEntityRepository<Rank> rankRepository;
        private IDeletableEntityRepository<Position> positionRepository;
        private ChampionService championService;

        public ChampionServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryChampion")
                .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(this.applicationDbContext);
            this.championRepository = new EfDeletableEntityRepository<Champion>(this.applicationDbContext);
            this.imageRepository = new EfDeletableEntityRepository<Image>(this.applicationDbContext);
            this.playerRepository = new EfDeletableEntityRepository<Player>(this.applicationDbContext);
            this.rankRepository = new EfDeletableEntityRepository<Rank>(this.applicationDbContext);
            this.positionRepository = new EfDeletableEntityRepository<Position>(this.applicationDbContext);

            this.championService = new ChampionService(this.championRepository, this.imageRepository);
        }

        [Fact]
        public async Task GetAllAsyncWorksRight()
        {
            await this.SeedAsync();

            AutoMapperConfig.RegisterMappings(typeof(ChampionInListViewModel).GetTypeInfo().Assembly);

            var champions = await this.championService.GetAllAsync<ChampionInListViewModel>();

            var championsDb = await this.championRepository.All().ToListAsync();

            Assert.Equal(championsDb.Count(), champions.Count());
        }

        [Fact]
        public async Task CreateAsyncWorksRight()
        {
            var content = "TestContent";

            var fileName = "Test";

            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);

            writer.Write(content);

            writer.Flush();

            stream.Position = 0;

            var fakeFile = new FormFile(stream, 0, stream.Length, "test", fileName);

            var model = new ChampionCreateViewModel()
            {
                ChampionName = "Test0",
                Description = "Descriptoin0",
                Level = 10,
                Origin = "Origin0",
                Power = 10,
                Image = fakeFile,
            };

            await this.championService.CreateAsync(model, "1", "imagePath");

            var championsDb = await this.championRepository.All()
                .FirstOrDefaultAsync();

            Assert.Equal(model.ChampionName, championsDb.ChampionName);
        }

        [Fact]
        public async Task GetChampionDetailsAsyncWorksRight()
        {
            await this.SeedAsync();

            AutoMapperConfig.RegisterMappings(typeof(ChampionDetailsViewModel).GetTypeInfo().Assembly);

            var champion = await this.championService.GetChampionDetailsAsync<ChampionDetailsViewModel>(1);

            Assert.Equal(1, champion.Id);
            Assert.Equal("Champion1", champion.ChampionName);
        }

        [Fact]
        public async Task GetLatestChampionsAsyncWorksRight()
        {
            await this.SeedAsync();

            AutoMapperConfig.RegisterMappings(typeof(ChampionInListViewModel).GetTypeInfo().Assembly);

            var latestChampions = await this.championService.GetLatestChampionsAsync<ChampionInListViewModel>();

            Assert.NotEmpty(latestChampions);
        }

        [Fact]
        public async Task GetChampionAsyncWorksRight()
        {
            await this.SeedAsync();

            const int championId = 1;

            var champion = await this.championService.GetChampionAsync(championId);

            var championDb = await this.championRepository.All()
                .FirstOrDefaultAsync(c => c.Id == championId);

            Assert.Equal(championDb.Id, champion.Id);
            Assert.Equal(championDb.ChampionName, champion.ChampionName);
        }
        //[Fact]
        //public async Task DeleteChampionAsyncWorksRight()
        //{
        //    await this.SeedAsync();

        //    const int championId = 1;
        //    const string playerId = "1";

        //    await this.championService.DeleteChampionAsync(championId, playerId);

        //    var championsDb = await this.championRepository.All().ToListAsync();

        //    Assert.Single(championsDb);
        //}

        [Fact]
        public async Task IsOwnerAsyncWorksRight()
        {
            await this.SeedAsync();

            const int championId = 1;
            const string playerId = "1";

            var isOwner = await this.championService.IsOwnerAsync(championId, playerId);

            Assert.True(isOwner);
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
                CoachId = null,
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

            var image1 = new Image
            {
                Id = "1",
                CreatorId = "1",
                Extension = ".png",
            };

            var image2 = new Image
            {
                Id = "2",
                CreatorId = "2",
                Extension = ".jpg",
            };

            await this.imageRepository.AddAsync(image1);
            await this.imageRepository.AddAsync(image2);

            await this.imageRepository.SaveChangesAsync();

            var champion1 = new Champion
            {
                Id = 1,
                ChampionName = "Champion1",
                Origin = "Origin1",
                Level = 1,
                Power = 1,
                Description = "Description1",
                ImageId = "1",
                PlayerId = "1",
            };

            var champion2 = new Champion
            {
                Id = 2,
                ChampionName = "Champion2",
                Origin = "Origin2",
                Level = 2,
                Power = 2,
                Description = "Description2",
                ImageId = "2",
                PlayerId = "2",
            };

            await this.championRepository.AddAsync(champion1);
            await this.championRepository.AddAsync(champion2);

            await this.championRepository.SaveChangesAsync();

        }

    }
}
