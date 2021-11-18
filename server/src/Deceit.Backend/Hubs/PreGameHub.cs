using Deceit.Backend.Domain.Game;
using Deceit.Backend.Domain.Lobbies;
using Microsoft.AspNetCore.SignalR;

namespace Deceit.Backend.Hubs;

class PreGameHub : Hub<IPreGameHubClient>
{
    private readonly LobbyService lobbyService;

    public PreGameHub(LobbyService lobbyService)
    {
        this.lobbyService = lobbyService;
    }

    // Can kind of simplify down to a really straightforward hub with nearly one method now.
    public async Task HandleAction(string actionName, object actionData)
    {
        // Get context for connection
        // create Action object
        // Dispatch action to context
        // persist new state
        // distribute state
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
        var playerConnectedAction = new Domain.Game.States.Actions.ConnectedAction()
        {
            PlayerId = Context.ConnectionId,
            Data = new(Context.ConnectionId, name)
        };
        // Retrieve DeceitContext based on Lobby/Player ID
        Domain.Game.States.DeceitContext deceitContext = new();
        deceitContext.Handle(playerConnectedAction);

        // Persist DeceitContext data

        // Return new state?

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
                .StartGame(
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
