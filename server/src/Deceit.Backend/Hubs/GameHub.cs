using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deceit.Backend.Domain.Game;
using Deceit.Backend.Domain.Lobbies;
using Deceit.Backend.Domain.Players;
using Microsoft.AspNetCore.SignalR;

namespace Deceit.Backend.Hubs
{
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

            await Clients.Group(lobbyId).LobbyUpdated(lobby);
        }

        public async Task SetConnectionToForensicScientist()
        {
            var lobby = lobbyService.GetLobbyWithPlayer(Context.ConnectionId);
            lobby.SetForensicScientistPlayer(Context.ConnectionId);
            await Clients.Group(lobby.LobbyId).LobbyUpdated(lobby);
        }

        public async Task StartGameInLobby()
        {
            var lobby = lobbyService.GetLobbyWithPlayer(Context.ConnectionId);
            var game = new DeceitGame(lobby);

            // Send message telling clients to connect to Game hub instead?
            // This is probably what makes the most sense...
            // Not sure where making the DeceitGame makes the most sense
            // For now, just worry about setting game state, transmitting it,
            // and representing it. Refactor later to a separate hub.

            // Each player needs to get a representation of the game that
            // has all public information and only their private information

            await Task.WhenAll(lobby.Players.Select(player =>
                Clients.Client(player.ConnectionId)
                    .GameStateUpdated(
                        game.GetGameStateForPlayer(player.ConnectionId))));
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var lobby = lobbyService.GetLobbyWithPlayer(Context.ConnectionId);
            lobby.RemovePlayer(Context.ConnectionId);
            RemoveLobbyIfEmpty(lobby);

            await Clients.Group(lobby.LobbyId).LobbyUpdated(lobby);

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
