using Deceit.Backend.Domain.Game.States.Actions;

namespace Deceit.Backend.Domain.Game.States;

public abstract class State
{
    protected readonly DeceitContext context;

    public State(DeceitContext context)
    {
        this.context = context;
    }

    public abstract State Handle(ActionBase action);

    protected Exception UnsupportedActionException() => new("Received unsupported action for state");
}
