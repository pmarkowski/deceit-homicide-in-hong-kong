using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Deceit.Backend.Hubs
{
    class GameHub : Hub
    {
        private readonly GameLobby lobby;

        public GameHub(GameLobby lobby)
        {
            this.lobby = lobby;
        }

        public async Task ConnectPlayer(string name) {
            lobby.AddPlayer(name);

            await Clients.All.SendAsync("PlayerConnected", name);
        }
    }
}
