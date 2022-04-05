using Deceit.Domain.Connectivity;
using Deceit.Domain.Lobbies;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Deceit.Backend.Hubs;

class GameLobbyHub : Hub<IGameLobbyHubClient>
{
    private readonly ConnectionService connectionService;
    private readonly GameLobbyService lobbyService;

    public GameLobbyHub(GameLobbyService lobbyService, ConnectionService connectionService)
    {
        this.lobbyService = lobbyService;
        this.connectionService = connectionService;
    }

    // Can kind of simplify down to a really straightforward hub with nearly one method now.
    public async Task SubmitAction(string actionType, JsonDocument action)
    {
        // Get context for connection
        // create Action object
        // Dispatch action to context
        // persist new state
        // distribute state
        var playerId = connectionService.GetPlayerIdForConnection(Context.ConnectionId);
        var lobby = lobbyService.GetLobbyWithPlayer(playerId);
        try
        {
            var deserializedAction = ActionFactory.CreateAction(actionType, action);

            lobby.DeceitContext.Handle(deserializedAction);
        }
        catch (Exception ex)
        {
            throw new HubException("An Error Ocurred", ex);
        }

        await Task.WhenAll(lobby.Players.Select(player =>
            Clients.Client(player.PlayerId)
                .GameUpdated(
                    lobby.DeceitContext.Game.GetGameInformationForPlayer(player.PlayerId))));
    }

    public async Task ConnectPlayer(string lobbyId, string name, string playerId)
    {
        // Feel like players should have some client side generated ID persisted
        // in localstorage that can be used to rejoin?
        var lobby = lobbyService.FindLobby(lobbyId);
        if (lobby is null)
        {
            throw new HubException("Lobby not found");
        }
        lobby.ConnectPlayer(new Domain.Players.Player(playerId, name, true));
        await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
        connectionService.AddPlayerIdForConnection(Context.ConnectionId, playerId);

        await Clients.Group(lobbyId).LobbyUpdated(lobby);
    }

    public async Task SetConnectionToForensicScientist()
    {
        var playerId = connectionService.GetPlayerIdForConnection(Context.ConnectionId);
        var lobby = lobbyService.GetLobbyWithPlayer(playerId);
        // Do we still need this Hub Method? Clients can now dispatch this action directly
        lobby.DeceitContext.Handle(new Domain.Game.States.Actions.SetForensicScientistAction(new(playerId)));
        await Clients.Group(lobby.LobbyId).LobbyUpdated(lobby);
    }

    public async Task StartGameInLobby()
    {
        var playerId = connectionService.GetPlayerIdForConnection(Context.ConnectionId);

        var lobby = lobbyService.GetLobbyWithPlayer(playerId);
        lobby.StartGame();

        // Send message telling clients to connect to Game hub instead?
        // This is probably what makes the most sense...
        // Not sure where making the DeceitGame makes the most sense
        // For now, just worry about setting game state, transmitting it,
        // and representing it. Refactor later to a separate hub.

        // Each player needs to get a representation of the game that
        // has all public information and only their private information

        await Task.WhenAll(lobby.Players.Select(player =>
            Clients.Client(player.PlayerId)
                .StartGame(
                    lobby.DeceitContext.Game.GetGameInformationForPlayer(player.PlayerId))));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var playerId = connectionService.GetPlayerIdForConnection(Context.ConnectionId);
        var lobby = lobbyService.GetLobbyWithPlayer(playerId);
        lobby.DisconnectPlayer(playerId);
        RemoveLobbyIfEmpty(lobby);
        connectionService.RemoveConnection(Context.ConnectionId);
        await Clients.Group(lobby.LobbyId).LobbyUpdated(lobby);

        await base.OnDisconnectedAsync(exception);
    }

    private void RemoveLobbyIfEmpty(GameLobby lobby)
    {
        if (!lobby.Players.Any())
        {
            lobbyService.RemoveLobby(lobby);
        }
    }
}
