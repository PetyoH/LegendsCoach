namespace LegendsCoach.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.ViewModels;
    using LegendsCoach.Web.ViewModels.Champion;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IChampionService championService;

        public HomeController(IChampionService championService)
        {
            this.championService = championService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ChampionsListViewModel
            {
                Champions = await this.championService.GetLatestChampionsAsync<ChampionInListViewModel>(),
            };

            return this.View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
