using AutoFixture;
using Deceit.Domain.Lobbies;
using Deceit.Domain.Players;
using FluentAssertions;
using Xunit;

namespace Deceit.Backend.Tests.Domain.Lobbies;

public class LobbyTests
{
    readonly Fixture fixture = new();

    [Fact]
    public void DisconnectPlayer_GameHasNotYetStarted_PlayerRemovedFromLobby()
    {
        Lobby lobby = new(fixture.Create<string>());

        var player = fixture.Create<Player>();
        lobby.AddPlayer(player);

        lobby.DisconnectPlayer(player.ConnectionId);

        lobby.Players.Should().BeEmpty();
    }

    [Fact]
    public void DisconnectPlayer_GameHasStarted_PlayerMarkedAsDisconnected()
    {
        Lobby lobby = new(fixture.Create<string>());

        var player = fixture.Create<Player>();
        lobby.AddPlayer(player);

        lobby.StartGame();

        lobby.DisconnectPlayer(player.ConnectionId);

        lobby.Players.Should().NotBeEmpty();
        lobby.Players.Should().Contain(player).Which.IsConnected.Should().BeFalse();
    }
}
