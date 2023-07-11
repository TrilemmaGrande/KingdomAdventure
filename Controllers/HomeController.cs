using KingdomAdventure.Models.Repository;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KingdomAdventure.Controllers
{
    public class HomeController : Controller
    {
        private IKingdomAdventureRepository repo;
        public HomeController(IKingdomAdventureRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(repo.Players.OrderBy(i => i.PlayerID));
        }
        [HttpGet]
        public IActionResult CreateNewPlayer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewPlayer(Player player)
        {
            repo.AddPlayer(player);
            HttpContext.Session.SetInt32("id", player.PlayerID);

            return RedirectToAction("Index","World");
        }
        public IActionResult DeletePlayer(int id)
        {
            Player player = repo.Players.FirstOrDefault(i => i.PlayerID == id);
            repo.DeletePlayer(player);
            return RedirectToAction("Index");
        }
    }
}