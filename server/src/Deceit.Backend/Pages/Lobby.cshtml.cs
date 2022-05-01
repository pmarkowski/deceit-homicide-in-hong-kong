using Deceit.Domain.Lobbies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Deceit.Backend.Pages;

public class LobbyModel : PageModel
{
    private readonly GameLobbyService lobbyService;

    [FromRoute]
    public string LobbyId { get; init; }

    public LobbyModel(GameLobbyService lobbyService)
    {
        this.lobbyService = lobbyService;
        LobbyId = String.Empty;
    }

    public ActionResult OnGet()
    {
        var lobby = lobbyService.FindLobby(LobbyId);
        if (lobby is null)
        {
            return NotFound();
        }
        return Page();
    }
}
