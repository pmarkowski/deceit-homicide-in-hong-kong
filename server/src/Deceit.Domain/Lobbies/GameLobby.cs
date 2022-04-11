using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Players;

namespace Deceit.Domain.Lobbies;

public class GameLobby
{
    public string LobbyId { get; }

    readonly List<Player> players;
    public IEnumerable<Player> Players => players;

    public DeceitContext DeceitContext { get; }

    public GameLobby(string lobbyId)
    {
        LobbyId = lobbyId;
        players = new();
        DeceitContext = new();
    }

    public void ConnectPlayer(Player player)
    {
        if (!PlayerCanConnect(player.PlayerId))
        {
            throw new InvalidOperationException("Cannot add a new player to a game that is in progress");
        }

        if (PlayerIsInLobbyAndDisconnected(player))
        {
            ReconnectPlayer(player);
        }
        else
        {
            players.Add(player);
            DeceitContext.Handle(new AddPlayerAction(player));
        }
    }

    public bool PlayerCanConnect(string playerId)
    {
        // This can occur if a player is opening another connection in a new tab,
        // on another device, or, if they were previously connected and are now
        // seeking to reconnect
        if (players.Any(player => player.PlayerId == playerId))
        {
            return true;
        }
        // Before the game has started, then anyone can join
        else if (DeceitContext.IsInState<PreGameState>())
        {
            return true;
        }
        // Once the game has started, and it is not a player already in the lobby,
        // then new players are not allowed to join
        else
        {
            return false;
        }
    }

    private void ReconnectPlayer(Player player)
    {
        players
            .Single(p => p.PlayerId == player.PlayerId)
            .IsConnected = true;
    }

    private bool PlayerIsInLobbyAndDisconnected(Player player)
    {
        return players.Any(p => p.PlayerId == player.PlayerId && !p.IsConnected);
    }

    public void DisconnectPlayer(string playerId)
    {
        if (DeceitContext.IsInState<PreGameState>())
        {
            players.RemoveAt(players.FindIndex(player => player.PlayerId == playerId));
        }
        else
        {
            players.Single(player => player.PlayerId == playerId).IsConnected = false;
        }
    }

    // Is this a worthwhile abstraction or should this also just be handled
    // via consumers directly passing StartGameAction to deceit context?
    public void StartGame()
    {
        DeceitContext.Handle(new StartGameAction(new()));
    }
}
