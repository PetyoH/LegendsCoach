namespace LegendsCoach.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.EntityFrameworkCore;

    public class PlayerService : IPlayerService
    {
        private readonly IDeletableEntityRepository<Player> playerRepository;

        public PlayerService(IDeletableEntityRepository<Player> playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async Task AddPlayerAsync(Player player)
        {
            await this.playerRepository.AddAsync(player);
            await this.playerRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerAllViewModel>> GetAllAsync()
        {
            var entities = await this.playerRepository.All()
                .Include(p => p.Position)
                .Include(p => p.Rank)
                .ToListAsync();

            return entities.Select(e => new PlayerAllViewModel
            {
                GameName = e.GameName,
                Description = e.Description,
                Level = e.Level,
                Position = e.Position.Name,
                Rank = e.Rank.Name,
            });

        }
    }
}
