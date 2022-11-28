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

    public class Coach : BaseDeletableModel<string>
    {
        public Coach()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }

    }
}
