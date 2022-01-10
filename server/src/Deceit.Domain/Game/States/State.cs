using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

public abstract class State
{
    protected readonly DeceitContext context;

    public State(DeceitContext context)
    {
        this.context = context;
    }

    public abstract State Handle(ActionBase action);

    protected Exception UnsupportedActionException(string stateName, string actionName) =>
        new($"Received unsupported action {actionName} for state {stateName}");
}
