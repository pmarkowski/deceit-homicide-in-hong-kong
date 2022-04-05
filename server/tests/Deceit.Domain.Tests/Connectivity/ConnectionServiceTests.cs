using AutoFixture;
using Deceit.Domain.Connectivity;
using FluentAssertions;
using Xunit;

namespace Deceit.Domain.Tests.Connectivity;

public class ConnectionServiceTests
{
    readonly Fixture fixture = new();

    [Fact]
    public void GetPlayerIdForConnection_MappingNotPresent_ThrowsException()
    {
        ConnectionService connectionService = new();
        var connectionId = fixture.Create<string>();

        var getPlayerIdForConnection = () => connectionService.GetPlayerIdForConnection(connectionId);

        getPlayerIdForConnection.Should().Throw<Exception>();
    }

    [Fact]
    public void GetPlayerIdForConnection_MappingPresent_SetsOutVariableToMappedPlayerId()
    {
        ConnectionService connectionService = new();
        var connectionId = fixture.Create<string>();
        var playerId = fixture.Create<string>();
        connectionService.AddPlayerIdForConnection(connectionId, playerId);

        var playerIdResult = connectionService.GetPlayerIdForConnection(connectionId);

        playerIdResult.Should().Be(playerId);
    }

    [Fact]
    public void GetPlayerIdForConnection_MappingRemoved_ThrowsException()
    {
        ConnectionService connectionService = new();
        var connectionId = fixture.Create<string>();
        var playerId = fixture.Create<string>();
        connectionService.AddPlayerIdForConnection(connectionId, playerId);

        connectionService.RemoveConnection(connectionId);

        var getPlayerIdForConnection = () => connectionService.GetPlayerIdForConnection(connectionId);

        getPlayerIdForConnection.Should().Throw<Exception>();
    }
}
