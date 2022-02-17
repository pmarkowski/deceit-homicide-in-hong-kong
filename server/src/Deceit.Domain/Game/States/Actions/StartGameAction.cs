namespace Deceit.Domain.Game.States.Actions;

public class StartGameAction : ActionBase<StartGameAction.StartGameData>
{
    public class StartGameData
    {
        public int NumberOfEvidenceCards => 4;
    }

    public StartGameAction(StartGameData data) : base(data)
    {
    }
}
