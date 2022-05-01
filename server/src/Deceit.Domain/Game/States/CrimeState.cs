using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

public class CrimeState : State
{
    public CrimeState(DeceitGame game)
        : base(game)
    {
    }

    internal override State Handle(ActionBase action) =>
        action switch
        {
            SelectMeansOfMurderAction meansOfMurderAction => HandleAction(meansOfMurderAction),
            _ => throw UnsupportedActionException(nameof(CrimeState), action.GetType().Name)
        };

    private State HandleAction(SelectMeansOfMurderAction selectMeansOfMurderAction)
    {
        // TODO: Need to make sure that action is submitted by the Murderer at some point
        var murderer = game.Investigators!.First(investigator => investigator.Role == Players.Roles.Murderer);
        bool murdererHasSelectedEvidenceCard = murderer.EvidenceCards.Any(evidenceCard => evidenceCard == selectMeansOfMurderAction.Data.Evidence);
        bool murdererHasSelectedMeansOfMurderCard = murderer.MeansOfMurderCards.Any(meansOfMurderCard => meansOfMurderCard == selectMeansOfMurderAction.Data.MeansOfMurder);
        if (!murdererHasSelectedEvidenceCard || !murdererHasSelectedMeansOfMurderCard)
        {
            throw new Exception("Cannot select Key Evidence that does not belong to Murderer");
        }
        game.KeyEvidence = selectMeansOfMurderAction.Data;
        return new LocationOfCrimeSceneCardSelectionState(game);
    }
}
