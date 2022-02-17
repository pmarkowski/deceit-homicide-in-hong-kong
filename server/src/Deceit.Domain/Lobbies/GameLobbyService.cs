namespace Deceit.Domain.Lobbies;

public class GameLobbyService
{
    readonly Dictionary<string, GameLobby> lobbies = new();

    public GameLobby? FindLobby(string lobbyId)
    {
        return lobbies.ContainsKey(lobbyId) ?
            lobbies[lobbyId] :
            null;
    }

    public void AddLobby(GameLobby lobby)
    {
        if (lobbies.ContainsKey(lobby.LobbyId))
        {
            throw new ArgumentException("Lobby with ID already exists.");
        }
        lobbies[lobby.LobbyId] = lobby;
    }

    public GameLobby GetLobbyWithPlayer(string connectionId)
    {
        return lobbies
            .First(keyValuePair => keyValuePair.Value.Players
                .Any(player => player.ConnectionId == connectionId))
            .Value;
    }

    public void RemoveLobby(GameLobby lobby)
    {
        lobbies.Remove(lobby.LobbyId);
    }
}
