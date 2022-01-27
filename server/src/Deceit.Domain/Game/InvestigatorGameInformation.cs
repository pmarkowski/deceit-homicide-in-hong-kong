using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.SceneCards;

namespace Deceit.Domain.Game;

/// <summary>
/// The representation of the game state that is
/// accessible to Investigators
/// </summary>
public class InvestigatorGameInformation : PlayerGameInformation
{
    public IEnumerable<InvestigatorWithoutRole> Investigators { get; }

    public InvestigatorGameInformation(
        string playerRole,
        IEnumerable<InvestigatorWithoutRole> investigators)
            : base(playerRole)
    {
        Investigators = investigators;
    }
}
