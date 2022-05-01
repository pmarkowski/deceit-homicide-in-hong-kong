using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game;

public class DeceitGame
{
    internal ForensicScientist? ForensicScientist { get; set; }

    public IEnumerable<Investigator>? Investigators { get; internal set; }

    internal KeyEvidence? KeyEvidence { get; set; }

    State currentGameState;

    public DeceitGame()
    {
        currentGameState = new PreGameState(this);
    }

    internal void TransitionTo(State state)
    {
        this.currentGameState = state;
    }

    public void Handle(ActionBase action)
    {
        State nextState = currentGameState.Handle(action);
        TransitionTo(nextState);
    }

    public bool IsInState<T>() where T : State => currentGameState.GetType() == typeof(T);

    public PlayerGameInformation GetGameInformationForPlayer(string playerId)
    {
        if (ForensicScientist.PlayerId == playerId)
        {
            return new ForensicScientistGameInformation(
                Roles.ForensicScientist,
                Investigators,
                KeyEvidence
            );
        }
        else
        {
            var investigator = Investigators.Single(player => player.PlayerId == playerId);
            if (investigator.Role == Roles.Murderer)
            {
                return new MurdererGameInformation(investigator.Role, Investigators, KeyEvidence);
            }
            else
            {
                return new InvestigatorGameInformation(
                    investigator.Role,
                    Investigators);
            }
        }
    }
}
