using System.Diagnostics.CodeAnalysis;

namespace Deceit.Domain.Connectivity;

public class ConnectionService
{
    private readonly Dictionary<string, string> connectionIdToPlayerIdMapping = new();

    public string GetPlayerIdForConnection(string connectionId)
    {
        return connectionIdToPlayerIdMapping[connectionId];
    }

    public void AddPlayerIdForConnection(string connectionId, string playerId) =>
        connectionIdToPlayerIdMapping[connectionId] = playerId;

    public void RemoveConnection(string connectionId) =>
        connectionIdToPlayerIdMapping.Remove(connectionId);
}
