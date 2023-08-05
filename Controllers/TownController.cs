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
            LoadBuildingViewData();
            return View(GetTown());
        }
        [HttpGet]
        public IActionResult CreateNewTown()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewTown(PlayerTown town)
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
        public IActionResult RemoveBuilding(int id)
        {
            repo.RemoveBuilding(GetTown(), id);
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
        public IActionResult LevelUpBuilding(int id)
        {
            repo.LevelUpBuilding(GetTown(), id);
            return RedirectToAction("Index");
        }
        public IActionResult IncreaseTownStage()
        {
            repo.IncreaseTownStage(GetTown());
            return RedirectToAction("Index");
        }
        private PlayerTown GetTown()
        {
            return repo.PlayerTowns
                        .Include(i => i.TownRessources).ThenInclude(ii => ii.Ressource)
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.RessourceCost).ThenInclude(iiii => iiii.Ressource)
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.ConsumingRessources).ThenInclude(iiii => iiii.Ressource)
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.ProducingRessources).ThenInclude(iiii => iiii.Ressource)
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.Building).ThenInclude(iii => iii.ProducingSoldiers).ThenInclude(iiii => iiii.Soldier)
                        .Include(i => i.TownBuildings).ThenInclude(ii => ii.DeactivatedRessourceProductions).ThenInclude(iii => iii.RessourceProducing)
                        .Include(i => i.TownSoldiers).ThenInclude(ii => ii.Soldier)
                        .FirstOrDefault(p => p.PlayerID == HttpContext.Session.GetInt32("id"));
        }
        private void LoadBuildingViewData()
        {
            ViewData["Buildings"] = repo.Buildings
                .Include(i => i.RessourceCost).ThenInclude(ii => ii.Ressource)
                .Include(i => i.ConsumingRessources).ThenInclude(ii => ii.Ressource)
                .Include(i => i.ProducingRessources).ThenInclude(ii => ii.Ressource)
                .Include(i => i.ProducingSoldiers).ThenInclude(ii => ii.Soldier);
        }
    }
}
