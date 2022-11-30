namespace LegendsCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(int id = 1)
        {
            const int ItemsPerPage = 9;

            var model = new PlayersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                PlayersCount = await this.playerService.GetCountAsync(),
                Players = await this.playerService.GetAllAsync<PlayerInListViewModel>(id, ItemsPerPage),
            };

            return this.View(model);
        }
    }
}
