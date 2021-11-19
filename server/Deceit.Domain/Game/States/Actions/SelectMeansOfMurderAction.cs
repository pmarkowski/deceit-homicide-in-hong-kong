using Deceit.Domain.Game.Evidence;

namespace Deceit.Domain.Game.States.Actions;

public class SelectMeansOfMurderAction : ActionBase<KeyEvidence>
{
    public SelectMeansOfMurderAction(KeyEvidence data) : base(data)
    {
    }
}
