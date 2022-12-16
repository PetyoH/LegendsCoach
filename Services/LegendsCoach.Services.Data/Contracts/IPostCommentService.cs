namespace LegendsCoach.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPostCommentService
    {
        Task CommentAsync(int championId, string playerId, string comment);

    }
}
