namespace LegendsCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.ViewModels.Player;
    using Microsoft.AspNetCore.Mvc;

    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await this.playerService.GetAllAsync();

            return this.View(model);
        }
    }
}
