namespace LegendsCoach.Web.ViewModels.Player
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;

    public class PlayerEditViewModel : IMapFrom<Player>
    {
        [Required]
        [StringLength(40, MinimumLength = 1)]
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
