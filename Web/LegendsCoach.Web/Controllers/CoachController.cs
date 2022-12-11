namespace LegendsCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.ViewModels.Coach;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CoachController : Controller
    {
        private readonly ICoachService coachService;

        public CoachController(ICoachService coachService)
        {
            this.coachService = coachService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(int id = 1)
        {
            const int ItemsPerPage = 8;

            var model = new CoachesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                PlayersCount = await this.coachService.GetCountAsync(),
                Players = await this.coachService.GetAllAsync<CoachInListViewModel>(id, ItemsPerPage),
            };

            return this.View(model);
        }
    }
}
