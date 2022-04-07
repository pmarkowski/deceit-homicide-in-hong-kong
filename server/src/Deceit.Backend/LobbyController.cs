using Deceit.Domain.Lobbies;
using Microsoft.AspNetCore.Mvc;

namespace Deceit.Backend
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
        public ActionResult<GameLobby> Get(string id)
        {
            var lobby = lobbyService.FindLobby(id);

            if (lobby is null)
            {
                return NotFound();
            }

            return lobby;
        }

        [HttpPost]
        public ActionResult Post()
        {
            GameLobby lobby = new(Guid.NewGuid().ToString());
            lobbyService.AddLobby(lobby);
            return CreatedAtAction(
                nameof(Get),
                new { id = lobby.LobbyId },
                lobby);
        }
    }
}
