using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;

namespace Deceit.Domain.Game;

public class MurdererGameInformation : InvestigatorGameInformation
{
    public CrimeSolution? CrimeSolution { get; }

    public MurdererGameInformation(
        string playerRole,
        IEnumerable<InvestigatorWithoutRole> investigators,
        CrimeSolution? crimeSolution)
            : base(playerRole, investigators)
    {
        CrimeSolution = crimeSolution;
    }

}
