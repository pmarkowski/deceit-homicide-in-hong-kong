using Deceit.Domain.Players;

namespace Deceit.Domain.Game.States.Actions;

public class StartGameData
{
    public IEnumerable<Player> Players { get; init; }
    public string ForensicScientistPlayerId { get; init; }
    public int NumberOfEvidenceCards => 4;

}

public class StartGameAction : ActionBase<StartGameData>
{
    public StartGameAction(StartGameData data) : base(data)
    {
    }
}
