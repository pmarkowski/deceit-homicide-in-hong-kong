using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.SceneCards;

namespace Deceit.Domain.Game;

/// <summary>
/// The representation of the game state that is
/// accessible to Investigators
/// </summary>
class InvestigatorGameInformation : PlayerGameInformation
{
    public string PlayerRole { get; }

    public IEnumerable<InvestigatorWithoutRole> Investigators { get; }

    public InvestigatorGameInformation(
        string playerRole,
        IEnumerable<InvestigatorWithoutRole> investigators)
    {
        PlayerRole = playerRole;
        Investigators = investigators;
    }
}
