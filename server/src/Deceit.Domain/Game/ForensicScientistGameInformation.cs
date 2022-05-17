using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;
using Deceit.Domain.Game.SceneCards;

namespace Deceit.Domain.Game;

/// <summary>
/// The representation of the game state that is
/// accessible to the Forensic Scientist
/// </summary>
public class ForensicScientistGameInformation : PlayerGameInformation
{
    public IEnumerable<Investigator> Investigators { get; }
    public CrimeSolution? CrimeSolution { get; }

    public ForensicScientistGameInformation(
        string role,
        IEnumerable<Investigator> investigators,
        CrimeSolution? crimeSolution)
            : base(role)
    {
        Investigators = investigators;
        CrimeSolution = crimeSolution;
    }
}
