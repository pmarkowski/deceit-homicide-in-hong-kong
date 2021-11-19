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
        var context = new DeceitContext();

        var players = fixture.CreateMany<Player>(6);
        var forensicScientist = players.First();
        context.Handle(new StartGameAction(new()
        {
            Players = players,
            ForensicScientistPlayerId = forensicScientist.ConnectionId
        }));

        var nonMurderer = context.Game.Investigators!.First(investigator => investigator.Role != Roles.Murderer);

        KeyEvidence selectedKeyEvidence = new(
            nonMurderer.EvidenceCards.First(),
            nonMurderer.MeansOfMurderCards.First());

        var handleFunction = () => context.Handle(new SelectMeansOfMurderAction(selectedKeyEvidence));

        handleFunction.Should().Throw<Exception>();
    }

    [Fact]
    public void HandleSelectMeansOfMurderAction_MurdererCardsSelected_ForensicScientistSeesKeyEvidence()
    {
        var context = new DeceitContext();

        var players = fixture.CreateMany<Player>(6);
        var forensicScientist = players.First();
        context.Handle(new StartGameAction(new()
        {
            Players = players,
            ForensicScientistPlayerId = forensicScientist.ConnectionId
        }));

        var murderer = context.Game.Investigators!.Single(investigator => investigator.Role == Roles.Murderer);

        KeyEvidence selectedKeyEvidence = new(
            murderer.EvidenceCards.First(),
            murderer.MeansOfMurderCards.First());
        context.Handle(new SelectMeansOfMurderAction(selectedKeyEvidence));

        var forensicScientistInformation = context.Game.GetGameInformationForPlayer(forensicScientist.ConnectionId);

        forensicScientistInformation.Should().BeOfType<ForensicScientistGameInformation>()
            .Which.KeyEvidence.Should().NotBeNull()
            .And.Be(selectedKeyEvidence);
    }

    [Fact]
    public void HandleSelectMeansOfMurderAction_MurdererCardsSelected_MurdererSeesKeyEvidence()
    {
        var context = new DeceitContext();

        var players = fixture.CreateMany<Player>(6);
        var forensicScientist = players.First();
        context.Handle(new StartGameAction(new()
        {
            Players = players,
            ForensicScientistPlayerId = forensicScientist.ConnectionId
        }));

        // TODO: Most of the public interface obscures this information, but you can
        // still figure out the murderer from the public interface through this.
        // It's handy for this test, but will want to revisit this later.
        var murderer = context.Game.Investigators!.Single(investigator => investigator.Role == Roles.Murderer);

        KeyEvidence selectedKeyEvidence = new(
            murderer.EvidenceCards.First(),
            murderer.MeansOfMurderCards.First());
        context.Handle(new SelectMeansOfMurderAction(selectedKeyEvidence));

        var murdererInformation = context.Game.GetGameInformationForPlayer(murderer.PlayerId);

        murdererInformation.Should().BeOfType<MurdererGameInformation>()
            .Which.KeyEvidence.Should().NotBeNull()
            .And.Be(selectedKeyEvidence);
    }
}
