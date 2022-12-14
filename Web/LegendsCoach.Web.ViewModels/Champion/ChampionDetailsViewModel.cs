namespace LegendsCoach.Web.ViewModels.Champion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;

    public class ChampionDetailsViewModel : IMapFrom<Champion>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ChampionName { get; set; }

        public Player Player { get; set; }

        public string Origin { get; set; }

        public int Power { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 30)]
        public string Comment { get; set; }

        public bool IsOwner { get; set; }

        public ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Champion, ChampionDetailsViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(c => $"/images/champions/{c.Image.Id}.{c.Image.Extension}"));
        }
    }
}
