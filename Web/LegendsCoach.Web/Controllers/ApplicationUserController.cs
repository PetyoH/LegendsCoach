namespace LegendsCoach.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.Infrastructure.Extensions;
    using LegendsCoach.Web.ViewModels.ApplicationUser;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IPositionService positionService;
        private readonly IRankService rankService;
        private readonly IPlayerService playerService;
        private readonly IDeletableEntityRepository<Player> playerRepository;

        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPositionService positionService,
            IRankService rankService,
            IPlayerService playerService,
            IDeletableEntityRepository<Player> playerRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.positionService = positionService;
            this.rankService = rankService;
            this.playerService = playerService;
            this.playerRepository = playerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                this.RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel
            {
                Ranks = await this.rankService.GetRanksAsync(),
                Positions = await this.positionService.GetPositionsAsync(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var playerNameExists = await this.playerRepository
                .AllAsNoTracking()
                .AnyAsync(p => p.GameName == model.GameName);

            if (playerNameExists)
            {
                this.ModelState.AddModelError(string.Empty, "This Game name is already taken");
            }

            if (!this.ModelState.IsValid)
            {
                model.Ranks = await this.rankService.GetRanksAsync();
                model.Positions = await this.positionService.GetPositionsAsync();

                return this.View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var curUser = await this.userManager.FindByNameAsync(model.UserName);

                await this.playerService.AddPlayerAsync(model, curUser.Id);

                return this.RedirectToAction("Login", "ApplicationUser");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            model.Ranks = await this.rankService.GetRanksAsync();
            model.Positions = await this.positionService.GetPositionsAsync();

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login");

            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
