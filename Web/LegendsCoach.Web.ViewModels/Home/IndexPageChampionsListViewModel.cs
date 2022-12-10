namespace LegendsCoach.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Web.ViewModels.Champion;

    public class IndexPageChampionsListViewModel
    {
        public IEnumerable<ChampionInListViewModel> Champions { get; set; } = new List<ChampionInListViewModel>();
    }
}
