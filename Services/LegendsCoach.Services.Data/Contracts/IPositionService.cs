namespace LegendsCoach.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;

    public interface IPositionService
    {
        Task<List<Position>> GetPositionsAsync();

        Task<Position> GetPositionAsync(string positionName);
    }
}
