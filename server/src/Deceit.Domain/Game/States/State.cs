using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

public abstract class State
{
    protected readonly DeceitGame context;

    public State(DeceitGame context)
    {
        this.context = context;
    }

    internal abstract State Handle(ActionBase action);

    protected Exception UnsupportedActionException(string stateName, string actionName) =>
        new($"Received unsupported action {actionName} for state {stateName}");
}
