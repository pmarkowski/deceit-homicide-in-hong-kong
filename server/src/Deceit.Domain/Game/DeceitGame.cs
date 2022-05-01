using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game;

public class DeceitGame
{
    internal ForensicScientist ForensicScientist { get; }

    internal IEnumerable<Investigator> Investigators { get; }

    internal KeyEvidence? KeyEvidence { get; set; }

    State currentGameState;

    public DeceitGame(DeceitGameSettings gameSettings, IEnumerable<string> playerIds)
    {
        ArgumentNullException.ThrowIfNull(gameSettings.ForensicScientistId);

        EvidenceCardsDeck evidenceCardsDeck = new();
        MeansOfMurderCardsDeck meansOfMurderCardsDeck = new();

        var roleCards = new InvestigatorRoleCardsDeck(playerIds.Count() - 1);

        ForensicScientist = new ForensicScientist(gameSettings.ForensicScientistId);
        Investigators = playerIds
            .Where(playerId => playerId != gameSettings.ForensicScientistId)
            .Select(investigatorPlayerId => new Investigator(
                investigatorPlayerId,
                roleCards.Draw(),
                meansOfMurderCardsDeck.Draw(gameSettings.NumberOfEvidenceCards),
                evidenceCardsDeck.Draw(gameSettings.NumberOfEvidenceCards)
            ))
            .ToList();

        currentGameState = new CrimeState(this);
    }

    private void TransitionTo(State state)
    {
        this.currentGameState = state;
    }

    public void HandleAction(ActionBase action)
    {
        State nextState = currentGameState.Handle(action);
        TransitionTo(nextState);
    }

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
