using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

public class PreGameState : State
{
    public PreGameState(DeceitContext context)
        : base(context)
    {
    }

    public override State Handle(ActionBase action) =>
        action switch
        {
            StartGameAction startGameAction => HandleAction(startGameAction),
            _ => throw UnsupportedActionException(nameof(PreGameState), action.GetType().Name)
        };

    private CrimeState HandleAction(StartGameAction startGameAction)
    {
        EvidenceCardsDeck evidenceCardsDeck = new();
        MeansOfMurderCardsDeck meansOfMurderCardsDeck = new();

        if (startGameAction.Data.ForensicScientistPlayerId is null)
        {
            throw new ArgumentNullException($"{nameof(startGameAction.Data.ForensicScientistPlayerId)} was expected.");
        }

        var roleCards = new InvestigatorRoleCardsDeck(startGameAction.Data.Players.Count() - 1);

        context.Game.ForensicScientist = new ForensicScientist(startGameAction.Data.ForensicScientistPlayerId);
        context.Game.Investigators = startGameAction.Data.Players
            .Where(player => player.ConnectionId != startGameAction.Data.ForensicScientistPlayerId)
            .Select(player => new Investigator(
                player.ConnectionId,
                roleCards.Draw(),
                meansOfMurderCardsDeck.Draw(startGameAction.Data.NumberOfEvidenceCards),
                evidenceCardsDeck.Draw(startGameAction.Data.NumberOfEvidenceCards)
            ))
            .ToList();

        return new CrimeState(context);
    }
}
