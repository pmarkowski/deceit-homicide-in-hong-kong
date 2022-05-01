using Deceit.Domain.Game;
using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Players;

namespace Deceit.Domain.Lobbies;

public class GameLobby
{
    public string LobbyId { get; }

    readonly List<Player> players;
    public IEnumerable<Player> Players => players;

    public DeceitGame DeceitGame { get; }

    public GameLobby(string lobbyId)
    {
        LobbyId = lobbyId;
        players = new();
        DeceitGame = new();
    }

    private bool GameHasStarted() => !DeceitGame.IsInState<PreGameState>();

    private bool PlayerIsInLobbyAndDisconnected(string playerId) => players.Any(p => p.PlayerId == playerId && !p.IsConnected);

    public bool PlayerCanConnect(string playerId) => !GameHasStarted() || PlayerIsInLobbyAndDisconnected(playerId);

    public void ConnectPlayer(Player player)
    {
        bool playerCanConnect = PlayerCanConnect(player.PlayerId);
        if (playerCanConnect && PlayerIsInLobbyAndDisconnected(player.PlayerId))
        {
            ReconnectPlayer(player);
        }
        else if (playerCanConnect && !GameHasStarted())
        {
            AddNewPlayer(player);
        }
        else
        {
            throw new InvalidOperationException("Cannot add a new player to a game that is in progress");
        }
    }

    private void AddNewPlayer(Player player)
    {
        players.Add(player);
        DeceitGame.Handle(new AddPlayerAction(player));
    }

    private void ReconnectPlayer(Player player)
    {
        players
            .Single(p => p.PlayerId == player.PlayerId)
            .IsConnected = true;
    }

    public void DisconnectPlayer(string playerId)
    {
        if (DeceitGame.IsInState<PreGameState>())
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
        DeceitGame.Handle(new StartGameAction(new()));
    }
}
