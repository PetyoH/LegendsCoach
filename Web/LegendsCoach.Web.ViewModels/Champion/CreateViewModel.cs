namespace LegendsCoach.Web.ViewModels.Champion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateViewModel : IMapFrom<Champion>
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ChampionName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Origin { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Power { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Level { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();
    }
}
