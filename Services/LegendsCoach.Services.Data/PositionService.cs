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
    using Microsoft.EntityFrameworkCore;

    public class PositionService : IPositionService
    {
        private readonly IDeletableEntityRepository<Position> positionRepository;

        public PositionService(IDeletableEntityRepository<Position> positionRepository)
        {
            this.positionRepository = positionRepository;
        }

        public async Task<Position> GetPositionIdAsync(string position)
        {
            return await this.positionRepository
                .AllAsNoTracking()
                .FirstAsync(p => p.Name == position);
        }

        public async Task<List<Position>> GetPositionsAsync()
        {
            return await this.positionRepository.AllAsNoTracking().ToListAsync();
        }
    }
}
