namespace LegendsCoach.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Web.ViewModels.Champion;

    public interface IChampionService
    {
        Task CreateAsync(ChampionCreateViewModel model, string playerId, string imagePath);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetChampionDetailsAsync<T>(int championId);
    }
}
