﻿namespace LegendsCoach.Web.Controllers
{
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
            var model = new CreateViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string playerId = await this.playerService.GetPlayerIdAsync(this.User.Id());

            await this.championService.CreateAsync(model, playerId, $"{this.environment.WebRootPath}/images");

            return this.RedirectToAction("All", "Champion");
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
    }
}
