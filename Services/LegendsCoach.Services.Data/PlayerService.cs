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
    using LegendsCoach.Services.Mapping;
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

        public async Task<IEnumerable<T>> GetAllAsync<T>(int page, int playersPerPage)
        {
            var players = await this.playerRepository.AllAsNoTracking()
                .OrderByDescending(e => e.CreatedOn)
                .Skip((page - 1) * playersPerPage)
                .Take(playersPerPage)
                .To<T>()
                .ToListAsync();

            return players;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.playerRepository.All().CountAsync();
        }
    }
}
