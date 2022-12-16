namespace LegendsCoach.Web.ViewModels.Comment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommentPostViewModel
    {
        [Required]
        [StringLength(300, MinimumLength = 30)]
        public string Comment { get; set; }

        [Required]
        public int ChampionId { get; set; }
    }
}
