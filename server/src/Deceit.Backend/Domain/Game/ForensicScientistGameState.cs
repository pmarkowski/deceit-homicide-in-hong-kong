using System.Collections.Generic;
using Deceit.Backend.Domain.Game.Players;
using Deceit.Backend.Domain.Game.SceneCards;

namespace Deceit.Backend.Domain.Game
{
    /// <summary>
    /// The representation of the game state that is
    /// accessible to the Forensic Scientist
    /// </summary>
    class ForensicScientistGameState : PlayerGameState
    {
        public string Role { get; }
        public List<SceneCard> SceneCards { get; }
        public IEnumerable<Investigator> Investigators { get; }

        public ForensicScientistGameState(
            string role,
            List<SceneCard> sceneCards,
            IEnumerable<Investigator> investigators)
        {
            Role = role;
            SceneCards = sceneCards;
            Investigators = investigators;
        }
    }
}
