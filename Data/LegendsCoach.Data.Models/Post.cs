namespace LegendsCoach.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(100)]
        public string Opinion { get; set; }

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }

        public ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();
    }
}
