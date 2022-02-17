namespace Deceit.Domain.Game.States.Actions;

public class SetForensicScientistAction : ActionBase<SetForensicScientistAction.SetForensicScientistActionData>
{
    public class SetForensicScientistActionData
    {
        public string NewForensicScientistPlayerId { get; }

        public SetForensicScientistActionData(string newForensicScientistPlayerId)
        {
            NewForensicScientistPlayerId = newForensicScientistPlayerId;
        }
    }

    public SetForensicScientistAction(SetForensicScientistActionData data) : base(data)
    {
    }
}