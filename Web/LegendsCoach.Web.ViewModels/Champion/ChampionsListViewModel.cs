namespace LegendsCoach.Web.ViewModels.Champion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChampionsListViewModel
    {
        public IEnumerable<ChampionInListViewModel> Champions { get; set; } = new List<ChampionInListViewModel>();
    }
}
