using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basketball7.Models
{
    public class CompareViewModel
    {
        public long Id { get; set; }
        //public string Position { get; set; } //could be enumeration
        //maybe selected players and selected positions??
        public List<string> Positions { get; set; } = new List<string>();
        public List<PlayerAndSeasons> Players { get; set; } = new List<PlayerAndSeasons>();
    }
}
