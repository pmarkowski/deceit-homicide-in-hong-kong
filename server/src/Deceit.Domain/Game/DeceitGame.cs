using Deceit.Domain.Game.Evidence;
using Deceit.Domain.Game.Players;

namespace Deceit.Domain.Game;

/// <summary>
/// Holds all information about the game
/// </summary>
public class DeceitGame
{
    internal ForensicScientist? ForensicScientist { get; set; }
    public IEnumerable<Investigator>? Investigators { get; internal set; }
    internal KeyEvidence? KeyEvidence { get; set; }

    public PlayerGameInformation GetGameInformationForPlayer(string playerId)
    {
        if (ForensicScientist.PlayerId == playerId)
        {
            return new ForensicScientistGameInformation(
                Roles.ForensicScientist,
                Investigators,
                KeyEvidence
            );
        }
        else
        {
            var investigator = Investigators.Single(player => player.PlayerId == playerId);
            if (investigator.Role == Roles.Murderer)
            {
                return new MurdererGameInformation(investigator.Role, Investigators, KeyEvidence);
            }
            else
            {
                return new InvestigatorGameInformation(
                    investigator.Role,
                    Investigators);
            }
        }
    }
}
