namespace LegendsCoach.Web.ViewModels.Player
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;

    public class PlayerToCoachInListViewModel : IMapFrom<Player>
    {
        public string Id { get; set; }

        public string GameName { get; set; }

        public string CoachId { get; set; }
    }
}
