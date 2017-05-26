using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basketball7.Data;
using Basketball7.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basketball7
{
    public class Compare : Controller
    {
        private readonly ApplicationDbContext _context;

        public Compare(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Careers()
        {
            var compareVM = new CompareViewModel();

            var players = _context.Player;

            foreach (Player player in players)
            {
                PlayerAndSeasons pas = new PlayerAndSeasons();
                pas.Player = player;
                pas.Seasons = _context.Season.Where(x => x.PlayerId == player.Id).ToList();
                pas.Career = pas.Seasons.Where(x => x.SEASON == "Career").FirstOrDefault();
                //bug could be error here with career not being available and null ref
                compareVM.Players.Add(pas);
            }
            //compareVM.Positions = players.Select(x => x.Position);
            foreach (Player player in players)
            {
                if (!compareVM.Positions.Contains(player.Position))
                {
                    compareVM.Positions.Add(player.Position);
                }
            }

            //compareVM.Players = players;
            return View(compareVM);
        }
    }
}
