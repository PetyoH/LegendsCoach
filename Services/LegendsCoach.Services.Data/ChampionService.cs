namespace LegendsCoach.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Services.Mapping;
    using LegendsCoach.Web.ViewModels.Champion;
    using Microsoft.EntityFrameworkCore;

    public class ChampionService : IChampionService
    {
        private readonly IDeletableEntityRepository<Champion> championRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ChampionService(
             IDeletableEntityRepository<Champion> championRepository,
             IDeletableEntityRepository<Image> imageRepository)
        {
            this.championRepository = championRepository;
            this.imageRepository = imageRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var champions = await this.championRepository
                .AllAsNoTracking()
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToListAsync();

            return champions;
        }

        public async Task CreateAsync(ChampionCreateViewModel model, string playerId, string imagePath)
        {
            var champion = new Champion
            {
                ChampionName = model.ChampionName,
                Origin = model.Origin,
                Level = model.Level,
                Power = model.Power,
                Description = model.Description,
                PlayerId = playerId,
            };

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var image = new Image
            {
                CreatorId = playerId,
                Extension = extension,
            };

            champion.Image = image;

            Directory.CreateDirectory($"{imagePath}/champions/");
            var physicalPath = $"{imagePath}/champions/{image.Id}.{extension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this.championRepository.AddAsync(champion);
            await this.championRepository.SaveChangesAsync();
        }

        public async Task<T> GetChampionDetailsAsync<T>(int championId)
        {
            var championDetails = await this.championRepository
                .AllAsNoTracking()
                .Where(c => c.Id == championId)
                .To<T>()
                .FirstOrDefaultAsync();

            if (championDetails == null)
            {
                throw new ArgumentNullException();
            }

            return championDetails;
        }

        public async Task<IEnumerable<T>> GetLatestChampionsAsync<T>()
        {
            var champions = await this.championRepository
                .AllAsNoTracking()
                .OrderByDescending(e => e.CreatedOn)
                .Take(8)
                .To<T>()
                .ToListAsync();

            return champions;
        }

        public async Task UpdateChampionAsync(int id, string playerId, ChampionEditViewModel model, string imagePath)
        {
            var champion = await this.GetChampionAsync(id);

            champion.ChampionName = model.ChampionName;
            champion.Level = model.Level;
            champion.Power = model.Power;
            champion.Description = model.Description;

            if (model.Image != null)
            {
                var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

                var image = new Image
                {
                    CreatorId = playerId,
                    Extension = extension,
                };

                await this.imageRepository.AddAsync(image);

                champion.ImageId = image.Id;

                Directory.CreateDirectory($"{imagePath}/champions/");
                var physicalPath = $"{imagePath}/champions/{image.Id}.{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await model.Image.CopyToAsync(fileStream);
            }

            if (await this.IsOwnerAsync(id, playerId))
            {
                this.championRepository.Update(champion);
                await this.championRepository.SaveChangesAsync();
            }
        }

        public async Task<Champion> GetChampionAsync(int championId)
        {
            var champion = await this.championRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == championId);

            if (champion == null)
            {
                throw new ArgumentNullException();
            }

            return champion;
        }

        public async Task DeleteChampionAsync(int championId, string playerId, bool isAdmin)
        {
            var champion = await this.GetChampionAsync(championId);

            if (await this.IsOwnerAsync(championId, playerId) || isAdmin)
            {
                this.championRepository.Delete(champion);
                await this.championRepository.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<bool> IsOwnerAsync(int championId, string playerId)
        {
            bool isOwner = false;

            var champion = await this.GetChampionAsync(championId);

            if (champion.PlayerId == playerId)
            {
                isOwner = true;
            }

            return isOwner;
        }
    }
}
