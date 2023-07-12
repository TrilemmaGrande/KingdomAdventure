using KingdomAdventure.Models.Repository;
using KingdomAdventure.Models.TownArea;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return View(GetTown());
        }
        [HttpGet]
        public IActionResult CreateNewTown()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewTown(Town town)
        {
            GetTown().TownName = town.TownName;
            repo.SaveRepo();
            return RedirectToAction("Index");
        }
        private Town GetTown()
        {
            return repo.Towns
                        .Include(i => i.TownRessources).ThenInclude(ii => ii.Ressource)
                        .Include(i => i.TownBuildings)
                        .Include(i => i.TownSoldiersAttacking).ThenInclude(ii => ii.Soldier)
                        .Include(i => i.TownSoldiersDefending).ThenInclude(ii => ii.Soldier)
                        .FirstOrDefault(p => p.PlayerID == HttpContext.Session.GetInt32("id"));
        }
    }
}
