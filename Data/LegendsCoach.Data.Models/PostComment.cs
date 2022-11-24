namespace LegendsCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Models;

    public class PostComment : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(100)]
        public string Comment { get; set; }

        // public string ImageUrl { get; set; }
        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
