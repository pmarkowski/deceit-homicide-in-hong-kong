using AutoFixture;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Lobbies;
using Deceit.Domain.Players;
using FluentAssertions;
using Xunit;

namespace Deceit.Backend.Tests.Domain.Lobbies;

public class GameLobbyTests
{
    static readonly Fixture fixture = new();
    GameLobby lobby = new(fixture.Create<string>());

    [Fact]
    public void DisconnectPlayer_GameHasNotYetStarted_PlayerRemovedFromLobby()
    {
        var player = fixture.Create<Player>();
        lobby.ConnectPlayer(player);

        lobby.DisconnectPlayer(player.PlayerId);

        lobby.Players.Should().BeEmpty();
    }

    [Fact]
    public void DisconnectPlayer_GameHasStarted_PlayerMarkedAsDisconnected()
    {
        var player = fixture.Create<Player>();
        lobby.ConnectPlayer(player);
        lobby.DeceitContext.Handle(new SetForensicScientistAction(new(player.PlayerId)));
        lobby.StartGame();

        lobby.DisconnectPlayer(player.PlayerId);

        lobby.Players.Should().NotBeEmpty();
        lobby.Players.Should().ContainEquivalentOf(player).Which.IsConnected.Should().BeFalse();
    }

    [Fact]
    public void ConnectPlayer_GameHasStartedWithoutPlayer_PlayerRejected()
    {
        var player = fixture.Create<Player>();
        lobby.DeceitContext.Handle(new SetForensicScientistAction(new(player.PlayerId)));
        lobby.StartGame();
        var player2 = fixture.Create<Player>();

        var connectPlayerAction = () => lobby.ConnectPlayer(player2);

        connectPlayerAction.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ConnectPlayer_GameHasStartedWithPlayerWhoDisconnected_PlayerConnects()
    {
        var player = fixture.Create<Player>();
        lobby.ConnectPlayer(player);
        lobby.DeceitContext.Handle(new SetForensicScientistAction(new(player.PlayerId)));
        lobby.StartGame();
        lobby.DisconnectPlayer(player.PlayerId);

        lobby.ConnectPlayer(player);

        lobby.Players.Should().NotBeEmpty();
        lobby.Players.Should().ContainEquivalentOf(player).Which.IsConnected.Should().BeTrue();
    }
}
