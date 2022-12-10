namespace LegendsCoach.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.Infrastructure.Extensions;
    using LegendsCoach.Web.ViewModels.Champion;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ChampionController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IWebHostEnvironment environment;
        private readonly IChampionService championService;

        public ChampionController(
            IChampionService championService,
            IPlayerService playerService,
            IWebHostEnvironment environment)
        {
            this.championService = championService;
            this.playerService = playerService;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ChampionCreateViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChampionCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                string playerId = await this.playerService.GetPlayerIdAsync(this.User.Id());

                await this.championService.CreateAsync(model, playerId, $"{this.environment.WebRootPath}/images");

                return this.RedirectToAction("All", "Champion");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = new ChampionsListViewModel
            {
                Champions = await this.championService.GetAllAsync<ChampionInListViewModel>(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.championService.GetChampionDetailsAsync<ChampionDetailsViewModel>(id);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.championService.GetChampionDetailsAsync<ChampionEditViewModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ChampionEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.User.Id();
            var playerId = await this.playerService.GetPlayerIdAsync(userId);

            await this.championService.UpdateChampionAsync(id, playerId, model, $"{this.environment.WebRootPath}/images");

            return this.RedirectToAction("All", "Champion");
        }
    }
}
