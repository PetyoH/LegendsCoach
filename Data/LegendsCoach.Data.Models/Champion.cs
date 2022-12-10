namespace LegendsCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Principal;

    using LegendsCoach.Data.Common.Models;

    public class Champion : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(50)]
        public string ChampionName { get; set; }

        [Required]
        [StringLength(50)]
        public string Origin { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Power { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Level { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public string PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [Required]
        public string ImageId { get; set; }

        public Image Image { get; set; }

        public ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();
    }
}
