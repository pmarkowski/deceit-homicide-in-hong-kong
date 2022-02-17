using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

internal class LocationOfCrimeSceneCardSelectionState : State
{
    public LocationOfCrimeSceneCardSelectionState(DeceitContext context)
        : base(context)
    {
    }

    internal override State Handle(ActionBase action)
    {
        throw new NotImplementedException();
    }
}