using Basketball7.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Basketball7.Models
{
    public class Season
    {
        public long Id { get; set; }
        public long PlayerId { get; set; } 
        [DisplayName("Season")]
        public string SEASON { get; set; }
        [DisplayName("Team")]
        public string TEAM { get; set; }
        [DisplayName("OffensiveRebounds")]
        public int OR { get; set; }
        [DisplayName("DefensiveRebounds")]
        public int DR { get; set; }
        [DisplayName("Rebounds")]
        public int REB { get; set; }
        [DisplayName("Assists")]
        public int AST { get; set; }
        [DisplayName("Blocks")]
        public int BLK { get; set; }
        [DisplayName("Steals")]
        public int STL { get; set; }
        [DisplayName("PersonalFouls")]
        public int PF { get; set; }
        [DisplayName("Turnovers")]
        public int TO { get; set; }
        [DisplayName("Points")]
        public int PTS { get; set; }

        [NotMapped]
        public Player Player { get; set; }
    }
    public static class Extensions
    {
        public static Season SetPlayer(this Season season, ApplicationDbContext context)
        {
            season.Player = context.Player.First(x => x.Id == season.PlayerId);
            return season;
        }
    }
}
