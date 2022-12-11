namespace LegendsCoach.Web.ViewModels.Player
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Services.Mapping;

    public class PlayerToCoachListViewModel
    {
        public IEnumerable<PlayerToCoachInListViewModel> Players { get; set; } = new List<PlayerToCoachInListViewModel>();
    }
}
