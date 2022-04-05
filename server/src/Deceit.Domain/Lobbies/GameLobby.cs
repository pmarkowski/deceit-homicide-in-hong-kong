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
        if (!DeceitContext.IsInState<PreGameState>())
        {
            if (PlayerIsInLobbyAndDisconnected(player))
            {
                ReconnectPlayer(player);
            }
            else
            {
                throw new InvalidOperationException("Cannot add a new player to a game that is in progress");
            }
        }
        else
        {
            players.Add(player);
            DeceitContext.Handle(new AddPlayerAction(player));
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
