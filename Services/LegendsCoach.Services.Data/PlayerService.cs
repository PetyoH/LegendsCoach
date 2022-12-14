namespace LegendsCoach.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Services.Mapping;
    using LegendsCoach.Web.ViewModels.ApplicationUser;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.EntityFrameworkCore;

    public class PlayerService : IPlayerService
    {
        private readonly IDeletableEntityRepository<Player> playerRepository;
        private readonly IDeletableEntityRepository<Coach> coachRepository;

        public PlayerService(
             IDeletableEntityRepository<Player> playerRepository,
             IDeletableEntityRepository<Coach> coachRepository)
        {
            this.playerRepository = playerRepository;
            this.coachRepository = coachRepository;
        }

        public async Task AddPlayerAsync(RegisterViewModel model, string userId)
        {
            var players = await this.playerRepository
                .AllAsNoTracking()
                .ToListAsync();

            if (players.Any(p => p.GameName == model.GameName))
            {
                throw new ArgumentException("There is already a player with the same name");
            }

            var player = new Player
            {
                UserId = userId,
                GameName = model.GameName,
                Description = model.Description,
                Level = model.Level,
                RankId = model.RankId,
                PositionId = model.PositionId,
            };

            await this.playerRepository.AddAsync(player);
            await this.playerRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int page, int playersPerPage)
        {
            var players = await this.playerRepository.AllAsNoTracking()
                .Where(p => p.CoachId == null)
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

        public async Task<string> GetPlayerIdAsync(string userId)
        {
            var player = await this.playerRepository
                .AllAsNoTracking()
                .Where(p => p.UserId == userId)
                .FirstOrDefaultAsync();

            if (player == null)
            {
                throw new ArgumentNullException("There is no such player");
            }

            return player.Id;
        }

        public async Task<Player> GetCurrentPlayerAsync(string userId)
        {
            var player = await this.playerRepository
                .AllAsNoTracking()
                .Where(p => p.UserId == userId)
                .FirstOrDefaultAsync();

            if (player == null)
            {
                throw new ArgumentNullException("There is no such player");
            }

            return player;
        }

        public async Task<T> GetPlayerDetailsAsync<T>(string playerId)
        {
            var player = await this.playerRepository
                .AllAsNoTracking()
                .Where(p => p.Id == playerId)
                .To<T>()
                .FirstOrDefaultAsync();

            if (player == null)
            {
                throw new ArgumentNullException("There is no such player");
            }

            return player;
        }

        public async Task UpdatePlayerAsync(PlayerEditViewModel model, string userId)
        {
            var player = await this.GetCurrentPlayerAsync(userId);

            var playerNameExists = await this.playerRepository
                .AllAsNoTracking()
                .AnyAsync(p => p.GameName == model.GameName);

            if (playerNameExists && player.GameName != model.GameName)
            {
                throw new ArgumentException("This Game name is already taken");
            }

            player.GameName = model.GameName;
            player.Description = model.Description;
            player.Level = model.Level;
            player.PositionId = model.PositionId;
            player.RankId = model.RankId;

            this.playerRepository.Update(player);
            await this.playerRepository.SaveChangesAsync();
        }

        public async Task AddCoach(string playerId)
        {
            var coach = new Coach();

            coach.PlayerId = playerId;

            await this.coachRepository.AddAsync(coach);

            var player = await this.GetPlayerAsync(playerId);

            player.CoachId = coach.Id;

            this.playerRepository.Update(player);
            await this.playerRepository.SaveChangesAsync();
        }

        public async Task<Player> GetPlayerAsync(string playerId)
        {
            return await this.playerRepository.AllAsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task<IEnumerable<T>> GetAllWithCoachInfoAsync<T>()
        {
            var players = await this.playerRepository.AllAsNoTracking()
                .OrderByDescending(e => e.CreatedOn)
                .To<T>()
                .ToListAsync();

            return players;
        }
    }
}
