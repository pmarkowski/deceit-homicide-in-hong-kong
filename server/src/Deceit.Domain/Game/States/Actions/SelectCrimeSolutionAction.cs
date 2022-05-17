using Deceit.Domain.Game.Evidence;

namespace Deceit.Domain.Game.States.Actions;

public class SelectCrimeSolutionAction : ActionBase<CrimeSolution>
{
    public SelectCrimeSolutionAction(CrimeSolution data) : base(data)
    {
    }
}
