namespace LegendsCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using LegendsCoach.Services.Data;
    using LegendsCoach.Services.Data.Contracts;
    using LegendsCoach.Web.Infrastructure.Extensions;
    using LegendsCoach.Web.ViewModels.Comment;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostCommentController : Controller
    {
        private readonly IPostCommentService commentService;
        private readonly IChampionService championService;
        private readonly IPlayerService playerService;

        public PostCommentController(
            IPostCommentService commentService,
            IChampionService championService,
            IPlayerService playerService)
        {
            this.commentService = commentService;
            this.championService = championService;
            this.playerService = playerService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentPostResponseModel>> Post(CommentPostViewModel model)
        {
            //var champion = await this.championService.GetChampionAsync(model.ChampionId);

            var playerId = await this.playerService.GetPlayerIdAsync(this.User.Id());

            await this.commentService.CommentAsync(model.ChampionId, playerId, model.Comment);
            //return new CommentPostResponseModel { Comment = model.Comment };
            
            return this.RedirectToAction("Details", "Champion", new { model.ChampionId });
        }
    }
}
