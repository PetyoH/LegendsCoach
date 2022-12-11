namespace LegendsCoach.Web.ViewModels.Coach
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;

    public class CoachInListViewModel : IMapFrom<Player>
    {
        public string Id { get; set; }

        public string GameName { get; set; }

        public int Level { get; set; }

        public string RankName { get; set; }

        public string PositionName { get; set; }

        public string CoachId { get; set; }
    }
}
