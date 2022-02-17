using Deceit.Domain.Game;
using Deceit.Domain.Lobbies;

namespace Deceit.Backend.Hubs;

public interface IGameLobbyHubClient
{
    Task LobbyUpdated(GameLobby players);
    Task StartGame(PlayerGameInformation playerGameInformation);
    Task GameUpdated(PlayerGameInformation playerGameInformation);
}
