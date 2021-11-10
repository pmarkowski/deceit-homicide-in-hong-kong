using Deceit.Backend.Domain.Game;
using Deceit.Backend.Domain.Lobbies;

namespace Deceit.Backend.Hubs;

public interface IPreGameHubClient
{
    Task LobbyUpdated(Lobby players);
    Task StartGame(PlayerGameState playerGameState);
}
