using AutoFixture;
using Deceit.Domain.Players;
using FluentAssertions;
using Xunit;

namespace Deceit.Domain.Tests.Players;

public class PlayerTests
{
    static readonly Fixture fixture = new();

    [Fact]
    public void Constructor_PlayerNameNotEmpty_CreatesPlayer()
    {
        var playerId = fixture.Create<string>();
        var playerName = "Philip";

        var player = new Player(playerId, playerName, true);

        player.Name.Should().Be(playerName);
    }

    [Fact]
    public void Constructor_PlayerNameNull_ThrowsException()
    {
        var playerId = fixture.Create<string>();
        string? playerName = null;

        var createPlayer = () => new Player(playerId, playerName!, true);

        createPlayer.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_PlayerNameEmpty_ThrowsException()
    {
        var playerId = fixture.Create<string>();
        var playerName = "";

        var createPlayer = () => new Player(playerId, playerName, true);

        createPlayer.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_PlayerNameWhitespace_ThrowsException()
    {
        var playerId = fixture.Create<string>();
        var playerName = "   ";

        var createPlayer = () => new Player(playerId, playerName, true);

        createPlayer.Should().Throw<ArgumentException>();
    }
}
