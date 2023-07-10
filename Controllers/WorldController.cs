using KingdomAdventure.Models;
using KingdomAdventure.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KingdomAdventure.Controllers
{
    public class WorldController : Controller
    {
        private IKingdomAdventureRepository repo;

        public WorldController(IKingdomAdventureRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int id)
        {
            return View(repo.Players.FirstOrDefault(i => i.PlayerID == id));
        }
    }
}
