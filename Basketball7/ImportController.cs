using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basketball7.Models;
using Basketball7.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basketball7
{
    public class ImportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImportPlayerAndSeasons([FromBody] List<PlayerAndSeasons> players)
        {
            if (ModelState.IsValid)
            {
                foreach (PlayerAndSeasons player in players)
                {
                    _context.Add(player.Player);
                    _context.AddRange(player.Seasons);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> ImportPlayer([FromBody] PlayerAndSeasons player)
        {
            if (ModelState.IsValid)
            {

                var play = _context.Add(player.Player);
                _context.AddRange(player.Seasons);


                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

    }

    public class PlayerAndSeasons
    {
        public Player Player { get; set; }
        public List<Season> Seasons { get; set; }
        public Season Career { get; set; }
    }
}
