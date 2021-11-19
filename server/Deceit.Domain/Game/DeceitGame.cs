using Deceit.Domain.Game.Players;

namespace Deceit.Domain.Game;

/// <summary>
/// Holds all information about the game
/// </summary>
public class DeceitGame
{
    public ForensicScientist ForensicScientist { get; internal set; }
    public IEnumerable<Investigator> Investigators { get; internal set; }

    public PlayerGameInformation GetGameInformationForPlayer(string playerId)
    {
        if (ForensicScientist.PlayerId == playerId)
        {
            return new ForensicScientistGameInformation(
                Roles.ForensicScientist,
                Investigators
            );
        }
        return new InvestigatorGameInformation(
            Investigators.Single(player => player.PlayerId == playerId).Role,
            Investigators);
    }
}
