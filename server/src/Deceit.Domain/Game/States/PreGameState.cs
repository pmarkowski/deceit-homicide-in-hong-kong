using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.States.Actions;

namespace Deceit.Domain.Game.States;

public class PreGameState : State
{
    private string? forensicScientistPlayerId;
    private List<string> playerIds = new();

    public PreGameState(DeceitContext context)
        : base(context)
    {
    }

    internal override State Handle(ActionBase action) =>
        action switch
        {
            AddPlayerAction addPlayerAction => HandleAction(addPlayerAction),
            SetForensicScientistAction setForensicScientistAction => HandleAction(setForensicScientistAction),
            StartGameAction startGameAction => HandleAction(startGameAction),
            _ => throw UnsupportedActionException(nameof(PreGameState), action.GetType().Name)
        };

    private PreGameState HandleAction(SetForensicScientistAction setForensicScientistAction)
    {
        forensicScientistPlayerId = setForensicScientistAction.Data.NewForensicScientistPlayerId;
        return this;
    }

    private PreGameState HandleAction(AddPlayerAction addPlayerAction)
    {
        if (!playerIds.Any())
        {
            forensicScientistPlayerId = addPlayerAction.Data.ConnectionId;
        }
        playerIds.Add(addPlayerAction.Data.ConnectionId);
        return this;
    }

    private CrimeState HandleAction(StartGameAction startGameAction)
    {
        EvidenceCardsDeck evidenceCardsDeck = new();
        MeansOfMurderCardsDeck meansOfMurderCardsDeck = new();

        if (forensicScientistPlayerId is null)
        {
            throw new InvalidOperationException($"{nameof(forensicScientistPlayerId)} cannot be null before starting the game.");
        }

        var roleCards = new InvestigatorRoleCardsDeck(playerIds.Count - 1);

        context.Game.ForensicScientist = new ForensicScientist(forensicScientistPlayerId);
        context.Game.Investigators = playerIds
            .Where(playerId => playerId != forensicScientistPlayerId)
            .Select(investigatorPlayerId => new Investigator(
                investigatorPlayerId,
                roleCards.Draw(),
                meansOfMurderCardsDeck.Draw(startGameAction.Data.NumberOfEvidenceCards),
                evidenceCardsDeck.Draw(startGameAction.Data.NumberOfEvidenceCards)
            ))
            .ToList();

        return new CrimeState(context);
    }
}
