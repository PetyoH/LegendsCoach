namespace LegendsCoach.Web.ViewModels.ApplicationUser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;

    public class RegisterViewModel : IMapFrom<Player>
    {
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string GameName { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Level { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        public int RankId { get; set; }

        public ICollection<Rank> Ranks { get; set; } = new HashSet<Rank>();

        public ICollection<Position> Positions { get; set; } = new HashSet<Position>();
    }
}
