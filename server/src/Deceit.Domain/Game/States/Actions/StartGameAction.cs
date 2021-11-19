using Deceit.Domain.Players;

namespace Deceit.Domain.Game.States.Actions;

public class StartGameAction : ActionBase<StartGameAction.StartGameData>
{
    public class StartGameData
    {
        public IEnumerable<Player> Players { get; init; }
        public string ForensicScientistPlayerId { get; init; }
        public int NumberOfEvidenceCards => 4;
    }

    public StartGameAction(StartGameData data) : base(data)
    {
    }
}
