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

        Task<IEnumerable<T>> GetAllAsync<T>(int page, int playersPerPage = 9);

        Task<int> GetCountAsync();

        Task<T> GetPlayerAsync<T>(string userId);

        Task<T> GetPlayerDetailsAsync<T>(string playerId);

        Task UpdatePlayerAsync(PlayerEditViewModel player);
    }
}
