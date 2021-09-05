using System.Threading.Tasks;
using Deceit.Backend.Domain.Lobbies;

namespace Deceit.Backend.Hubs
{
    public interface IGameHubClient
    {
        Task LobbyUpdated(Lobby players);
    }
}
