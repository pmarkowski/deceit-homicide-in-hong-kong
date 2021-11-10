namespace Deceit.Backend.Domain.Lobbies;

public class LobbyService
{
    Dictionary<string, Lobby> lobbies = new();

    public Lobby? FindLobby(string lobbyId)
    {
        return lobbies.ContainsKey(lobbyId) ?
            lobbies[lobbyId] :
            null;
    }

    public void AddLobby(Lobby lobby)
    {
        lobbies[lobby.LobbyId] = lobby;
    }

    public Lobby GetLobbyWithPlayer(string connectionId)
    {
        return lobbies
            .First(keyValuePair => keyValuePair.Value.Players
                .Any(player => player.ConnectionId == connectionId))
            .Value;
    }

    internal void RemoveLobby(Lobby lobby)
    {
        lobbies.Remove(lobby.LobbyId);
    }
}
