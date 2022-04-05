using AutoFixture;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Lobbies;
using Deceit.Domain.Players;
using FluentAssertions;
using Xunit;

namespace Deceit.Backend.Tests.Domain.Lobbies;

public class GameLobbyTests
{
    readonly Fixture fixture = new();

    [Fact]
    public void DisconnectPlayer_GameHasNotYetStarted_PlayerRemovedFromLobby()
    {
        GameLobby lobby = new(fixture.Create<string>());

        var player = fixture.Create<Player>();
        lobby.ConnectPlayer(player);

        lobby.DisconnectPlayer(player.PlayerId);

        lobby.Players.Should().BeEmpty();
    }

    [Fact]
    public void DisconnectPlayer_GameHasStarted_PlayerMarkedAsDisconnected()
    {
        GameLobby lobby = new(fixture.Create<string>());

        var player = fixture.Create<Player>();
        lobby.ConnectPlayer(player);
        lobby.DeceitContext.Handle(new SetForensicScientistAction(new(player.PlayerId)));
        lobby.StartGame();

        lobby.DisconnectPlayer(player.PlayerId);

        lobby.Players.Should().NotBeEmpty();
        lobby.Players.Should().ContainEquivalentOf(player).Which.IsConnected.Should().BeFalse();
    }
}
