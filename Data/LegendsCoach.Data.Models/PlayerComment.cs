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

    public class PlayerComment : BaseDeletableModel<int>
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }

        public string OwnerId { get; set; }
    }
}
