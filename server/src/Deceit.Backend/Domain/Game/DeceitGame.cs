using Deceit.Backend.Domain.Game.Evidence;
using Deceit.Backend.Domain.Game.Players;
using Deceit.Backend.Domain.Game.SceneCards;
using Deceit.Backend.Domain.Lobbies;

namespace Deceit.Backend.Domain.Game;

/// <summary>
/// Implementation of the actual game that tracks game state,
/// set up, and enforces rules and mechanics.
/// </summary>
public class DeceitGame
{
    private readonly SceneCardsDeck sceneCardsDeck = new();
    private readonly EvidenceCardsDeck evidenceCardsDeck = new();
    private readonly MeansOfMurderCardsDeck meansOfMurderCardsDeck = new();

    private readonly ForensicScientist forensicScientist;
    private readonly IEnumerable<Investigator> investigators;

    private readonly List<SceneCard> sceneCards = new();

    public DeceitGame(Lobby lobby)
    {
        if (lobby.ForensicScientistId is null)
        {
            throw new ArgumentNullException($"{nameof(lobby.ForensicScientistId)} was expected.");
        }

        forensicScientist = new ForensicScientist(lobby.ForensicScientistId);
        var roleCards = new InvestigatorRoleCardsDeck(lobby.Players.Count() - 1);
        investigators = lobby.Players
            .Where(player => player.ConnectionId != lobby.ForensicScientistId)
            .Select(player => new Investigator(
                player.ConnectionId,
                roleCards.Draw(),
                meansOfMurderCardsDeck.Draw(4),
                evidenceCardsDeck.Draw(4)
            ))
            .ToList();

        sceneCards.Add(new SceneCards.CauseOfDeathSceneCard());
        sceneCards.Add(SceneCards.LocationOfCrimeSceneCards.Cards.First());
        for (int i = 0; i < 4; i++)
        {
            sceneCards.Add(sceneCardsDeck.Draw());
        }
    }

    internal PlayerGameState GetGameStateForPlayer(string playerId)
    {
        if (forensicScientist.PlayerId == playerId)
        {
            return new ForensicScientistGameState(
                Roles.ForensicScientist,
                sceneCards,
                investigators
            );
        }
        return new InvestigatorGameState(
            investigators.Single(player => player.PlayerId == playerId).Role,
            sceneCards,
            investigators);
    }
}
