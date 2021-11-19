using Deceit.Domain.Game;
using Deceit.Domain.Lobbies;

namespace Deceit.Backend.Hubs;

public interface IPreGameHubClient
{
    Task LobbyUpdated(Lobby players);
    Task StartGame(PlayerGameInformation playerGameInformation);
}
