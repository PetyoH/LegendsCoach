namespace LegendsCoach.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICoachService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(int page, int playersPerPage = 8);

        Task<int> GetCountAsync();
    }
}
