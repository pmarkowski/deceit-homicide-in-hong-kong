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
    public IEnumerable<Investigator> Investigators { get; }

    public ForensicScientistGameInformation(
        string role,
        IEnumerable<Investigator> investigators)
    {
        Role = role;
        Investigators = investigators;
    }
}
