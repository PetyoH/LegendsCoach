namespace LegendsCoach.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Web.ViewModels.Player;

    public interface IPlayerService
    {
        Task AddPlayerAsync(Player player);

        Task<IEnumerable<T>> GetAllAsync<T>(int page, int playersPerPage = 12);

        Task<int> GetCountAsync();
    }
}
