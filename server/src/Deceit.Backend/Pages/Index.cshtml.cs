using Deceit.Domain.Lobbies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Deceit.Backend.Pages;

public class IndexModel : PageModel
{
    private readonly GameLobbyService lobbyService;

    public IndexModel(GameLobbyService lobbyService)
    {
        this.lobbyService = lobbyService;
    }

    public RedirectToPageResult OnPost()
    {
        // generate new random lobby
        string lobbyId = Guid.NewGuid().ToString();
        lobbyService.AddLobby(new GameLobby(lobbyId));

        // redirect user to it
        return RedirectToPage("Lobby", new { lobbyId });
    }
}
