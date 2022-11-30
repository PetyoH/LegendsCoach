namespace LegendsCoach.Web.ViewModels.Player
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;

    public class PlayerInListViewModel : IMapFrom<Player>
    {
        public string Id { get; set; }

        public string GameName { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public string RankName { get; set; }

        public string PositionName { get; set; }
    }
}
