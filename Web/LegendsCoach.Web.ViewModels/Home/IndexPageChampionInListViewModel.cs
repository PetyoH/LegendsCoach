namespace LegendsCoach.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Web.ViewModels.Champion;

    public class IndexPageChampionInListViewModel
    {
        public int Id { get; set; }

        public string ChampionName { get; set; }

        public string Origin { get; set; }

        public int Power { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string PlayerId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Champion, ChampionInListViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(c => $"/images/champions/{c.Image.Id}.{c.Image.Extension}"));
        }
    }
}
