namespace LegendsCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Common.Models;

    public class Position : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public ICollection<Player> Players { get; set; } = new HashSet<Player>();
    }
}
