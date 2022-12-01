namespace LegendsCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.Infrastructure.Extensions;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IRankService rankService;
        private readonly IPositionService positionService;

        public PlayerController(
            IPlayerService playerService,
            IRankService rankService,
            IPositionService positionService)
        {
            this.playerService = playerService;
            this.rankService = rankService;
            this.positionService = positionService;
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

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = this.User.Id();
            var model = await this.playerService.GetPlayerAsync<PlayerEditViewModel>(userId);

            model.Ranks = await this.rankService.GetRanksAsync();
            model.Positions = await this.positionService.GetPositionsAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlayerEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Ranks = await this.rankService.GetRanksAsync();
                model.Positions = await this.positionService.GetPositionsAsync();

                return this.View(model);
            }

            await this.playerService.UpdatePlayerAsync(model);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await this.playerService.GetPlayerDetailsAsync<PlayerDetailsViewModel>(id);
            return this.View(model);
        }
    }
}
