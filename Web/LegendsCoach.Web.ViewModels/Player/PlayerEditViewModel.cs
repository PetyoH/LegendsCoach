﻿namespace LegendsCoach.Web.ViewModels.Player
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using LegendsCoach.Data.Models;
    using LegendsCoach.Services.Mapping;

    public class PlayerEditViewModel : IMapFrom<Player>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string GameName { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Level { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public string PositionName { get; set; }

        [Required]
        public string RankName { get; set; }

        public List<Rank> Ranks { get; set; } = new List<Rank>();

        public List<Position> Positions { get; set; } = new List<Position>();

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
