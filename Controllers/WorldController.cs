using KingdomAdventure.Models;
using KingdomAdventure.Models.Repository;
using KingdomAdventure.Models.WorldArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace KingdomAdventure.Controllers
{
    public class WorldController : Controller
    {
        private IKingdomAdventureRepository repo;

        public WorldController(IKingdomAdventureRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                HttpContext.Session.SetInt32("id", (int)id);
            }
            return View(GetPlayer());
        }
        public IActionResult Inventory()
        {
            return View(GetPlayer());
        }
        public IActionResult AddInventoryItem()
        {
            Random rand = new Random();

            // Random Item from Item Table
            var item = repo.Items.FirstOrDefault(i => i.ItemValue == Math.Round((rand.NextDouble() * 10.00),0));
            if (item is null)
            {
                item = repo.Items.FirstOrDefault(i => i.ItemValue == 4); // Maple Bow
            }
            // Player Inventory
            var inventory = GetPlayer().Inventory;

            repo.AddInventoryItem(item, inventory);
            return RedirectToAction("Inventory");
        }
        public IActionResult DeleteInventoryItem(int itemID)
        {
            var inventoryItem = GetPlayer().Inventory.InventoryItems.FirstOrDefault(i => i.InventoryItemID == itemID);
            var inventory = GetPlayer().Inventory;
            repo.DeleteInventoryItem(inventoryItem, inventory);
            return RedirectToAction("Inventory");
        }
        private Player GetPlayer()
        {
            return repo.Players
                        .Include(i => i.Inventory).ThenInclude(ii => ii.InventoryItems).ThenInclude(iii => iii.Item)
                        .Include(i => i.Inventory).ThenInclude(ii => ii.InventoryUpgrades).ThenInclude(iii => iii.Upgrade)
                        .FirstOrDefault(p => p.PlayerID == HttpContext.Session.GetInt32("id"));
        }
    }
}
