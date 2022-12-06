namespace LegendsCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Models;

    public class Player : BaseDeletableModel<string>
    {
        public Player()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [StringLength(40)]
        public string GameName { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Level { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int RankId { get; set; }

        public Rank Rank { get; set; }

        [Required]
        public int PositionId { get; set; }

        public Position Position { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string CoachId { get; set; }

        public Coach Coach { get; set; }

        public ICollection<Champion> Champions { get; set; } = new HashSet<Champion>();

        public ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();

        public ICollection<PlayerComment> PlayerComments { get; set; } = new HashSet<PlayerComment>();
    }
}
