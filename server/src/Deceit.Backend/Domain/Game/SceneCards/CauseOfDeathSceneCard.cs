using System.Collections.Generic;

namespace Deceit.Backend.Domain.Game.SceneCards
{
    class CauseOfDeathSceneCard : SceneCard
    {
        public CauseOfDeathSceneCard() : base("Cause of Death", new List<string>
        {
            "Suffocation",
            "Severe Injury",
            "Loss of Blood",
            "Illness/Disease",
            "Poisoning",
            "Accident"
        })
        {
        }
    }
}
