using Deceit.Domain.Lobbies;

namespace Deceit.Backend.Controllers
{
    public class LobbyResponse
    {
        public string LobbyId { get; }
        public bool IsJoinable { get; }

        public LobbyResponse(string lobbyId, bool isJoinable)
        {
            LobbyId = lobbyId;
            IsJoinable = isJoinable;
        }
    }
}
