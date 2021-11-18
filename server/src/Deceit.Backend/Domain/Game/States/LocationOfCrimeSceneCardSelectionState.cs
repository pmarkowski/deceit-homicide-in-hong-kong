using Deceit.Backend.Domain.Game.States.Actions;

namespace Deceit.Backend.Domain.Game.States;

internal class LocationOfCrimeSceneCardSelectionState : State
{
    public LocationOfCrimeSceneCardSelectionState(DeceitContext context)
        : base(context)
    {
    }

    public override State Handle(ActionBase action)
    {
        throw new NotImplementedException();
    }
}