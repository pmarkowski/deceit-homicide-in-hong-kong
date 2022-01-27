using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;

namespace Deceit.Domain.Game;

public class MurdererGameInformation : InvestigatorGameInformation
{
    public KeyEvidence? KeyEvidence { get; }

    public MurdererGameInformation(
        string playerRole,
        IEnumerable<InvestigatorWithoutRole> investigators,
        KeyEvidence? keyEvidence)
            : base(playerRole, investigators)
    {
        KeyEvidence = keyEvidence;
    }

}
