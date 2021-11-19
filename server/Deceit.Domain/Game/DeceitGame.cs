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
        throw new NotImplementedException();
        //if (forensicScientist.PlayerId == playerId)
        //{
        //    return new ForensicScientistGameInformation(
        //        Roles.ForensicScientist,
        //        sceneCards,
        //        investigators
        //    );
        //}
        //return new InvestigatorGameInformation(
        //    investigators.Single(player => player.PlayerId == playerId).Role,
        //    sceneCards,
        //    investigators);
    }
}