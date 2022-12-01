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
    using Microsoft.EntityFrameworkCore;

    public class RankService : IRankService
    {
        private readonly IDeletableEntityRepository<Rank> rankRepository;

        public RankService(IDeletableEntityRepository<Rank> rankRepository)
        {
            this.rankRepository = rankRepository;
        }

        public async Task<Rank> GetRankAsync(string rankName)
        {
            return await this.rankRepository
                .AllAsNoTracking()
                .FirstAsync(r => r.Name == rankName);
        }

        public Task<List<Rank>> GetRanksAsync()
        {
            return this.rankRepository.AllAsNoTracking().ToListAsync();
        }
    }
}
