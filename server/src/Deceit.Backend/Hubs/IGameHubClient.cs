using System.Threading.Tasks;
using Deceit.Backend.Domain.Game;
using Deceit.Backend.Domain.Lobbies;

namespace Deceit.Backend.Hubs
{
    public interface IGameHubClient
    {
        Task LobbyUpdated(Lobby players);
        Task GameStateUpdated(PlayerGameState playerGameState);
    }
}
