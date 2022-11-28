namespace LegendsCoach.Web.ViewModels.ApplicationUser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string GameName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Level { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Rank { get; set; }

        public List<Rank> Ranks { get; set; } = new List<Rank>();

        public List<Position> Positions { get; set; } = new List<Position>();
    }
}
