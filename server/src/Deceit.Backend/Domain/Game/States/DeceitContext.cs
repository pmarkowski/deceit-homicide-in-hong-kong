using Deceit.Backend.Domain.Game.States.Actions;

namespace Deceit.Backend.Domain.Game.States;

public class DeceitContext
{
    State state;

    public DeceitContext()
    {
        state = new PreGameState(this);
    }

    internal void TransitionTo(State state)
    {
        this.state = state;
    }

    public void Handle(ActionBase action)
    {
        State nextState = state.Handle(action);
        TransitionTo(nextState);
    }
}
