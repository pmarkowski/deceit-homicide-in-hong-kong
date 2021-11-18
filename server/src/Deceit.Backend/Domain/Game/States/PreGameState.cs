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
            ConnectedAction connectedAction => HandleAction(connectedAction),
            DisconnectedAction disconnectedAction => HandleAction(disconnectedAction),
            SetForensicScientistAction setForensicScientistAction => HandleAction(setForensicScientistAction),
            _ => throw UnsupportedActionException()
        };

    private CrimeState HandleAction(StartGameAction startGameAction)
    {
        // Check amount of 
        return new CrimeState(context);
    }

    private State HandleAction(ConnectedAction connectedAction)
    {
        // Add player to Context?
        return this;
    }

    private State HandleAction(DisconnectedAction disconnectedAction)
    {
        // Remove player from context?
        return this;
    }

    private State HandleAction(SetForensicScientistAction setForensicScientistAction)
    {
        // Set playerId on this State object, or on the Context?... Probably Context?
        return this;
    }
}
