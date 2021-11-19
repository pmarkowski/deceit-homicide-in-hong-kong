using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

public class CrimeState : State
{
    public CrimeState(DeceitContext context)
        : base(context)
    {
    }

    public override State Handle(ActionBase action) =>
        action switch
        {
            SelectMeansOfMurderAction meansOfMurderAction => HandleAction(meansOfMurderAction),
            _ => throw UnsupportedActionException()
        };

    private State HandleAction(SelectMeansOfMurderAction meansOfMurderAction)
    {
        // Need to make sure that action is submitted by the Murderer
        // Make sure the evidence is in front of the Murderer
        // Set the KeyEvidence on the context and transition to next state
        return new LocationOfCrimeSceneCardSelectionState(context);
    }
}
