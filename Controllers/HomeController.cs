using KingdomAdventure.Models.Repository;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

    }
}