using AutoFixture;
using Deceit.Domain.Game;
using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Players;
using FluentAssertions;
using Xunit;

namespace Deceit.Domain.Tests.Game.States;

public class CrimeStateTests
{
    readonly Fixture fixture = new();

    [Fact]
    public void HandleSelectMeansOfMurderAction_OtherInvestigatorCardsSelected_ThrowsException()
    {
        var players = fixture.CreateMany<Player>(6);
        var forensicScientist = players.First();
        var context = new DeceitGame(
            new DeceitGameSettings(forensicScientist.PlayerId),
            players.Select(player => player.PlayerId));

        var nonMurderer = ((ForensicScientistGameInformation)context.GetGameInformationForPlayer(forensicScientist.PlayerId))
            .Investigators.First(investigator => investigator.Role != Roles.Murderer);

        CrimeSolution crimeSolution = new(
            nonMurderer.EvidenceCards.First(),
            nonMurderer.MeansOfMurderCards.First());

        var handleFunction = () => context.HandleAction(new SelectCrimeSolutionAction(crimeSolution));

        handleFunction.Should().Throw<Exception>();
    }

    [Fact]
    public void HandleSelectMeansOfMurderAction_MurdererCardsSelected_ForensicScientistSeesCrimeSolution()
    {
        var players = fixture.CreateMany<Player>(6);
        var forensicScientist = players.First();
        var context = new DeceitGame(
            new DeceitGameSettings(forensicScientist.PlayerId),
            players.Select(player => player.PlayerId));

        var murderer = ((ForensicScientistGameInformation)context.GetGameInformationForPlayer(forensicScientist.PlayerId))
            .Investigators.First(investigator => investigator.Role == Roles.Murderer);

        CrimeSolution crimeSolution = new(
            murderer.EvidenceCards.First(),
            murderer.MeansOfMurderCards.First());
        context.HandleAction(new SelectCrimeSolutionAction(crimeSolution));

        var forensicScientistInformation = context.GetGameInformationForPlayer(forensicScientist.PlayerId);

        forensicScientistInformation.Should().BeOfType<ForensicScientistGameInformation>()
            .Which.CrimeSolution.Should().NotBeNull()
            .And.Be(crimeSolution);
    }

    [Fact]
    public void HandleSelectMeansOfMurderAction_MurdererCardsSelected_MurdererSeesCrimeSolution()
    {
        var players = fixture.CreateMany<Player>(6);
        var forensicScientist = players.First();
        var context = new DeceitGame(
            new DeceitGameSettings(forensicScientist.PlayerId),
            players.Select(player => player.PlayerId));

        var murderer = ((ForensicScientistGameInformation)context.GetGameInformationForPlayer(forensicScientist.PlayerId))
            .Investigators.First(investigator => investigator.Role == Roles.Murderer);

        CrimeSolution crimeSolution = new(
            murderer.EvidenceCards.First(),
            murderer.MeansOfMurderCards.First());
        context.HandleAction(new SelectCrimeSolutionAction(crimeSolution));

        var murdererInformation = context.GetGameInformationForPlayer(murderer.PlayerId);

        murdererInformation.Should().BeOfType<MurdererGameInformation>()
            .Which.CrimeSolution.Should().NotBeNull()
            .And.Be(crimeSolution);
    }
}
