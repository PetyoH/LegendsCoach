namespace LegendsCoach.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.AspNetCore.Mvc;

    public class CoachController : AdministrationController
    {
        private readonly IPlayerService playerService;

        public CoachController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> MakeCoach()
        {
            var model = new PlayerToCoachListViewModel
            {
                Players = await this.playerService.GetAllWithCoachInfoAsync<PlayerToCoachInListViewModel>(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Promote(string id)
        {
            await this.playerService.AddCoach(id);

            return this.RedirectToAction("MakeCoach", "Coach");
        }
    }
}
