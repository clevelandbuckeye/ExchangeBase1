using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Basketball7.Data;
using Basketball7.Models;

namespace Basketball7
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var players = await _context.Player.ToListAsync();
            foreach (Player player in players)
            {
                player.Seasons = await _context.Season.Where(x => x.PlayerId == player.Id).ToListAsync();
            }
            return View(players);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            player.Seasons = await _context.Season.Where(x => x.PlayerId == player.Id).ToListAsync();
            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Jersey,Age,Height,Weight,HighSchool,College,DraftYear,Position,ImageUrl")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<Player> players)
        {
            if (ModelState.IsValid)
            {
                _context.AddRange(players);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(players);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Jersey,Age,Height,Weight,HighSchool,College,DraftYear,Position,ImageUrl")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var player = await _context.Player.SingleOrDefaultAsync(m => m.Id == id);
            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlayerExists(long id)
        {
            return _context.Player.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Careers()
        {
            var compareVM = new CompareViewModel();

            var players = _context.Player;

            foreach (Player player in players)
            {
                PlayerAndSeasons pas = new PlayerAndSeasons();
                pas.Player = player;
                pas.Seasons = await _context.Season.Where(x => x.PlayerId == player.Id).ToListAsync();
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

            return View(compareVM);
        }

        public IActionResult Comparison()
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
            foreach (Player player in players)
            {
                if (!compareVM.Positions.Contains(player.Position))
                {
                    compareVM.Positions.Add(player.Position);
                }
            }

            return View(compareVM);
        }

        public IActionResult Fun()
        {
            return View();
        }
    }
}
