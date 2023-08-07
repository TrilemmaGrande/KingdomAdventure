using KingdomAdventure.Models.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace KingdomAdventure.Models.TownArea
{
    public class TownHub : Hub
    {
        private IKingdomAdventureRepository repo;
        public TownHub(IKingdomAdventureRepository repo)
        {
            this.repo = repo;
        }
        public async Task RequestUpdateRessources(PlayerTown town)
        {
            repo.UpdateRessources(town);

            await Clients.All.SendAsync("ReceiveUpdateRessources", town);
        }
    }
}
