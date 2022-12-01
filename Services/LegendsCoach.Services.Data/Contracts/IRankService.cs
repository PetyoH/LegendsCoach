namespace LegendsCoach.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;

    public interface IRankService
    {
        Task<List<Rank>> GetRanksAsync();

        Task<Rank> GetRankAsync(string rankName);
    }
}
