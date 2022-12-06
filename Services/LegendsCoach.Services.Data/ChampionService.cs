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

        public ChampionService(
             IDeletableEntityRepository<Champion> championRepository)
        {
            this.championRepository = championRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var champions = await this.championRepository
                .AllAsNoTracking()
                .OrderByDescending(e => e.CreatedOn)
                .To<T>()
                .ToListAsync();

            return champions;
        }

        public async Task CreateAsync(CreateViewModel model, string playerId, string imagePath)
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
    }
}
