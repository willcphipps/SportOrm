using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context context;

        public HomeController(Context DBContext)
        {
            context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = context.Leagues
                .Where(l => l.Sport.Contains("Baseball"));
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.AllWomen = context.Leagues.Where(w => w.Name.Contains("Women"));
            ViewBag.NotFootball = context.Leagues.Where(f => f.Sport != "Football");
            ViewBag.Hockey = context.Leagues.Where(h => h.Sport.Contains("Hockey"));
            ViewBag.Conferences = context.Leagues.Where(f => f.Name.Contains("Conference"));
            ViewBag.Atlanta = context.Leagues.Where(a => a.Name.Contains("Atlantic"));
            ViewBag.Dallas = context.Teams.Where(d => d.Location.Contains("Dallas"));
            ViewBag.Raptors = context.Teams.Where(r => r.TeamName == "Raptors").ToList();
            ViewBag.City = context.Teams.Where(c => c.Location.Contains("City")).ToList();
            ViewBag.T = context.Teams.Where(t => t.TeamName.StartsWith("T")).ToList();
            ViewBag.Alphebet = context.Teams.OrderBy(l => l.Location).ToList();
            ViewBag.RAlphebet = context.Teams.OrderByDescending(l => l.Location).ToList();
            ViewBag.Cooper = context.Players.Where(l => l.LastName == "Cooper").ToList();
            ViewBag.Joshua = context.Players.Where(f => f.FirstName == "Joshua").ToList();
            ViewBag.JCooper = context.Players.Where(c => c.LastName == "Cooper" && c.FirstName != "Joshua").ToList();
            ViewBag.WyattAlex = context.Players.Where(c => c.FirstName == "Alexander" || c.FirstName == "Wyatt").ToList();
            return View();
        }
        [HttpGet("AsModel")]
        public IActionResult AsModel()
        {
            return View(context);
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            ViewBag.Atlantic = context.Teams.Include(l => l.CurrLeague).Where(l => l.CurrLeague.Name.Contains("Atlantic")).ToList();
            ViewBag.Penguins = context.Players.Include(p => p.CurrentTeam).Where(p => p.CurrentTeam.TeamName == "Penguins").ToList();
            // ViewBag.ICBC = context.Leagues.Where(l => l.Name == "International Collegiate Baseball Conference").Include(t => t.Teams).ToList();
            ViewBag.ICBC = context.Teams.Include(i => i.CurrLeague).Where(i => i.CurrLeague.Name == "International Collegiate Baseball Conference").ToList();
            ViewBag.ACAF = context.Teams.Include(i => i.CurrLeague).Where(i => i.CurrLeague.Name == "American Conference of Amateur Football").ToList();
            ViewBag.Football = context.Teams.Include(i => i.CurrLeague).Where(i => i.CurrLeague.Sport == "Football").ToList();
            ViewBag.Sophia = context.Teams.Include(t => t.CurrentPlayers).Where(s => s.CurrentPlayers.All(p => p.FirstName == "Sophia")).ToList();
            // ViewBag.Flores = context.Teams.Include(f => f.CurrentPlayers).Where(f => f.CurrentPlayers.All(l => l.LastName == "Flores"));
            ViewBag.Flores = context.Players.Include(f => f.CurrentTeam).Where(f => f.LastName == "Flores").ToList();
            ViewBag.TigerCats = context.Players.Include(f => f.CurrentTeam).Where(f => f.CurrentTeam.TeamName == "Tiger-Cats").ToList();
            ViewBag.Twelvers = context.Teams.Include(w => w.CurrentPlayers).Where(w => w.CurrentPlayers.Count > 13).ToList();
            ViewBag.AllTeams = context.Teams.Include(a => a.CurrentPlayers).Where(w => w.CurrentPlayers.Count > 1);
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}