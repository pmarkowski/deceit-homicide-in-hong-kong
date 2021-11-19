using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

public class DeceitContext
{
    State state;
    public DeceitGame Game { get; }

    public DeceitContext()
    {
        state = new PreGameState(this);
        Game = new DeceitGame();
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

    public bool IsInState<T>() where T : State => state.GetType() == typeof(T);
}
