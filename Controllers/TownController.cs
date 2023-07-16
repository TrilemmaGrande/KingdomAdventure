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
            if (GetTown().TownName is not null)
            {
                repo.UpdateRessources(GetTown());
            }
            ViewBag.Buildings = repo.Buildings
                .Include(i => i.BuildingRessourcesCosts).ThenInclude(ii => ii.Ressource)
                .Include(i => i.ConsumingRessources).ThenInclude(ii => ii.Ressource)
                .Include(i => i.ProducingRessources).ThenInclude(ii => ii.Ressource)
                .Include(i => i.ProducingSoldiers).ThenInclude(ii => ii.Soldier);
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
        public IActionResult AddBuilding(int id)
        {
            repo.AddBuilding(GetTown(), id);
            return RedirectToAction("Index");
        }
        public IActionResult AddWorkerToBuilding(int id) 
        {
            repo.AddWorkerToBuilding(GetTown(), id);
            return RedirectToAction("Index");
        }
        public IActionResult SubWorkerFromBuilding(int id)
        {
            repo.SubWorkerFromBuilding(GetTown(), id);
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
    }
}
