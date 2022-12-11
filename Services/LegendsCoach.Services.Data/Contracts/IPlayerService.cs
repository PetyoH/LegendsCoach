namespace LegendsCoach.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Web.ViewModels.ApplicationUser;
    using LegendsCoach.Web.ViewModels.Player;

    public interface IPlayerService
    {
        Task AddPlayerAsync(RegisterViewModel model, string userId);

        Task<IEnumerable<T>> GetAllAsync<T>(int page, int playersPerPage = 8);

        Task<IEnumerable<T>> GetAllWithCoachInfoAsync<T>();

        Task<int> GetCountAsync();

        Task<string> GetPlayerIdAsync(string userId);

        Task<Player> GetPlayerAsync(string playerId);

        Task<Player> GetCurrentPlayerAsync(string userId);

        Task<T> GetPlayerDetailsAsync<T>(string playerId);

        Task UpdatePlayerAsync(PlayerEditViewModel player, string userId, string playerId);

        Task AddCoach(string playerId);
    }
}
