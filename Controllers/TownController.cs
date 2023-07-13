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
            GenerateViewBagBuildings();
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
            repo.CreateTownValues(GetTown());
            return RedirectToAction("Index");
        }
        private Town GetTown()
        {
            return repo.Towns
                        .Include(i => i.TownRessources).ThenInclude(ii => ii.Ressource)
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.BuildingRessourcesCosts)                
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.ConsumingRessources)                
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.ProducingRessources)                
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.ProducingSoldiers)                
                        .Include(i => i.TownSoldiers).ThenInclude(ii => ii.Soldier)
                        .FirstOrDefault(p => p.PlayerID == HttpContext.Session.GetInt32("id"));
        }
        private void GenerateViewBagBuildings()
        {
            ViewBag.Buildings = repo.Buildings;
        }
    }
}
