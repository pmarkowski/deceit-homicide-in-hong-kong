using Deceit.Backend.Domain.Game.Players;
using Deceit.Backend.Domain.Game.SceneCards;

namespace Deceit.Backend.Domain.Game;

/// <summary>
/// The representation of the game state that is
/// accessible to Investigators
/// </summary>
class InvestigatorGameState : PlayerGameState
{
    public string PlayerRole { get; }

    public IEnumerable<SceneCard> SceneCards { get; }

    public IEnumerable<InvestigatorWithoutRole> Investigators { get; }

    public InvestigatorGameState(
        string playerRole,
        IEnumerable<SceneCard> sceneCards,
        IEnumerable<InvestigatorWithoutRole> investigators)
    {
        PlayerRole = playerRole;
        SceneCards = sceneCards;
        Investigators = investigators;
    }
}
