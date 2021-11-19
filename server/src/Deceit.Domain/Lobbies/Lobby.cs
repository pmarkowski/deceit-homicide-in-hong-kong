using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Players;

namespace Deceit.Domain.Lobbies;

public class Lobby
{
    public string LobbyId { get; }

    readonly List<Player> players;
    public IEnumerable<Player> Players => players;

    public string? ForensicScientistId { get; private set; }

    public DeceitContext DeceitContext { get; }

    public Lobby(string lobbyId)
    {
        LobbyId = lobbyId;
        players = new();
        DeceitContext = new();
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
        SetForensicScientistForFirstPlayer(player);
    }

    private void SetForensicScientistForFirstPlayer(Player player)
    {
        if (players.Count == 1)
        {
            SetForensicScientistPlayer(player.ConnectionId);
        }
    }

    public void SetForensicScientistPlayer(string connectionId)
    {
        ForensicScientistId = connectionId;
    }

    public void DisconnectPlayer(string connectionId)
    {
        if (DeceitContext.IsInState<PreGameState>())
        {
            players.RemoveAt(players.FindIndex(player => player.ConnectionId == connectionId));
        }
        else
        {
            players.Single(player => player.ConnectionId == connectionId).IsConnected = false;
        }
    }

    public void StartGame()
    {
        DeceitContext.Handle(new StartGameAction(new()
        {
            Players = players,
            ForensicScientistPlayerId = ForensicScientistId
        }));
    }
}
