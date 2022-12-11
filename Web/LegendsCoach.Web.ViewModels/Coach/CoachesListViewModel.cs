namespace LegendsCoach.Web.ViewModels.Coach
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Web.ViewModels.Player;

    public class CoachesListViewModel : PagingViewModel
    {
        public IEnumerable<CoachInListViewModel> Players { get; set; } = new List<CoachInListViewModel>();
    }
}
