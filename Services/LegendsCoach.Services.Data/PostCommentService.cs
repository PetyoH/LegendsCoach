namespace LegendsCoach.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Repositories;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Data.Contracts;

    public class PostCommentService : IPostCommentService
    {
        private readonly IDeletableEntityRepository<PostComment> postCommentRepository;
        private readonly IDeletableEntityRepository<Champion> championRepository;
        private readonly IChampionService championService;

        public PostCommentService(
            IDeletableEntityRepository<PostComment> postCommentRepository,
            IDeletableEntityRepository<Champion> championRepository,
            IChampionService championService)
        {
            this.postCommentRepository = postCommentRepository;
            this.championRepository = championRepository;
            this.championService = championService;
        }

        public async Task CommentAsync(int championId, string playerId, string comment)
        {
            var postComment = new PostComment
            {
                ChampionId = championId,
                PlayerId = playerId,
                Comment = comment,
            };

            await this.postCommentRepository.AddAsync(postComment);

            var champion = await this.championService.GetChampionAsync(championId);

            champion.PostComments.Add(postComment);

            this.championRepository.Update(champion);
            await this.championRepository.SaveChangesAsync();
        }
    }
}
