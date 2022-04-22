using Deceit.Domain.Lobbies;
using Microsoft.AspNetCore.Mvc;

namespace Deceit.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly GameLobbyService lobbyService;

        public LobbyController(GameLobbyService lobbyService)
        {
            this.lobbyService = lobbyService;
        }

        [HttpGet("{id}")]
        public ActionResult<LobbyResponse> Get(string id, [FromHeader(Name = "Player-Id")] string playerId)
        {
            var lobby = lobbyService.FindLobby(id);

            if (lobby is null)
            {
                return NotFound();
            }

            var playerCanJoinLobbyResult = lobby.PlayerCanConnect(playerId);

            return new LobbyResponse(lobby.LobbyId, playerCanJoinLobbyResult);
        }

        [HttpPost]
        public ActionResult<LobbyResponse> Post()
        {
            GameLobby lobby = new(Guid.NewGuid().ToString());
            lobbyService.AddLobby(lobby);
            return CreatedAtAction(
                nameof(Get),
                new { id = lobby.LobbyId },
                new LobbyResponse(lobby.LobbyId, true));
        }
    }
}
