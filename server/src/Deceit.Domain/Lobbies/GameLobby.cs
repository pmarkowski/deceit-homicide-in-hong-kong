using Deceit.Domain.Game;
using Deceit.Domain.Players;

namespace Deceit.Domain.Lobbies;

public class GameLobby
{
    public string LobbyId { get; }

    readonly List<Player> players;
    public IEnumerable<Player> Players => players;

    public DeceitGameSettings? DeceitGameSettings { get; private set; }

    public DeceitGame? DeceitGame { get; private set; }

    public GameLobby(string lobbyId)
    {
        LobbyId = lobbyId;
        players = new();
    }

    private bool GameHasStarted => DeceitGame is not null;

    private bool PlayerIsInLobby(string playerId) => players.Any(player => player.PlayerId == playerId);

    private bool PlayerIsInLobbyAndDisconnected(string playerId) => players.Any(p => p.PlayerId == playerId && !p.IsConnected);

    public bool PlayerCanConnect(string playerId) => !GameHasStarted || PlayerIsInLobbyAndDisconnected(playerId);

    public void ConnectPlayer(Player player)
    {
        bool playerCanConnect = PlayerCanConnect(player.PlayerId);
        if (playerCanConnect && PlayerIsInLobbyAndDisconnected(player.PlayerId))
        {
            ReconnectPlayer(player);
        }
        else if (playerCanConnect && !GameHasStarted)
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

        if (players.Count == 1)
        {
            SetForensicScientist(player.PlayerId);
        }
    }

    private void ReconnectPlayer(Player player)
    {
        players
            .Single(p => p.PlayerId == player.PlayerId)
            .IsConnected = true;
    }

    public void DisconnectPlayer(string playerId)
    {
        if (!GameHasStarted)
        {
            players.RemoveAt(players.FindIndex(player => player.PlayerId == playerId));
        }
        else
        {
            players.Single(player => player.PlayerId == playerId).IsConnected = false;
        }
    }

    public void SetForensicScientist(string playerId)
    {
        if (GameHasStarted)
        {
            throw new InvalidOperationException("Cannot set Forensic Scientist once game has started.");
        }

        if (!PlayerIsInLobby(playerId))
        {
            throw new InvalidOperationException("Cannot set Forensic Scientist to a player that is not in the lobby.");
        }

        DeceitGameSettings = new DeceitGameSettings(playerId);
    }

    public void StartGame()
    {
        if (DeceitGameSettings is null)
        {
            throw new InvalidOperationException("Cannot start a game in a lobby with no settings");
        }

        DeceitGame = new DeceitGame(
            DeceitGameSettings,
            players.Select(player => player.PlayerId));
    }
}
