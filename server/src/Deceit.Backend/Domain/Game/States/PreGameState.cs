using Deceit.Backend.Domain.Game.States.Actions;

namespace Deceit.Backend.Domain.Game.States;

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
            _ => throw UnsupportedActionException()
        };

    private CrimeState HandleAction(StartGameAction startGameAction)
    {
        // Check amount of 
        return new CrimeState(context);
    }
}
