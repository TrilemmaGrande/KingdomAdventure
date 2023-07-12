using KingdomAdventure.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KingdomAdventure.Controllers
{
    public class TownController : Controller
    {
        private IKingdomAdventureRepository repo;

        public TownController(IKingdomAdventureRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
