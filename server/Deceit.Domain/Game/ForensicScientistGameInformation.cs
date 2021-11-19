using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.SceneCards;

namespace Deceit.Domain.Game;

/// <summary>
/// The representation of the game state that is
/// accessible to the Forensic Scientist
/// </summary>
class ForensicScientistGameInformation : PlayerGameInformation
{
    public string Role { get; }
    public List<SceneCard> SceneCards { get; }
    public IEnumerable<Investigator> Investigators { get; }

    public ForensicScientistGameInformation(
        string role,
        List<SceneCard> sceneCards,
        IEnumerable<Investigator> investigators)
    {
        Role = role;
        SceneCards = sceneCards;
        Investigators = investigators;
    }
}
