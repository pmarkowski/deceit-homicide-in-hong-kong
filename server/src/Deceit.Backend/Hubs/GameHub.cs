using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deceit.Backend.Domain.Lobbies;
using Deceit.Backend.Domain.Players;
using Microsoft.AspNetCore.SignalR;

namespace Deceit.Backend.Hubs
{
    public interface IGameHubClient
    {
        Task LobbyUpdated(IEnumerable<Player> players);
    }

    class GameHub : Hub<IGameHubClient>
    {
        private readonly LobbyService lobbyService;

        public GameHub(LobbyService lobbyService)
        {
            this.lobbyService = lobbyService;
        }

        public async Task ConnectPlayer(string lobbyId, string name)
        {
            // Feel like players should have some client side generated ID persisted
            // in localstorage that can be used to rejoin?
            var lobby = lobbyService.FindLobby(lobbyId);
            if (lobby is null)
            {
                throw new HubException("Lobby not found");
            }
            lobby.AddPlayer(new Domain.Players.Player(Context.ConnectionId, name));
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);

            await Clients.Group(lobbyId).LobbyUpdated(lobby.Players);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var lobby = lobbyService.GetLobbyWithPlayer(Context.ConnectionId);
            lobby.RemovePlayer(Context.ConnectionId);
            RemoveLobbyIfEmpty(lobby);

            await Clients.Group(lobby.LobbyId).LobbyUpdated(lobby.Players);

            await base.OnDisconnectedAsync(exception);
        }

        private void RemoveLobbyIfEmpty(Lobby lobby)
        {
            if (!lobby.Players.Any())
            {
                lobbyService.RemoveLobby(lobby);
            }
        }
    }
}
