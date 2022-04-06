using Microsoft.AspNetCore.SignalR;

namespace Deceit.Backend.Hubs;

public class PlayerIdFromQueryUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        var playerIdFromQuery = connection.GetHttpContext()?.Request.Query["playerId"];
        connection.UserIdentifier = playerIdFromQuery ?? throw new Exception("Player ID query parameter is not present");
        return connection.UserIdentifier;
    }
}
