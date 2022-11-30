namespace LegendsCoach.Web.ViewModels.Player
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using LegendsCoach.Web.ViewModels;

    public class PlayersListViewModel : PagingViewModel
    {
        public IEnumerable<PlayerInListViewModel> Players { get; set; }
    }
}
