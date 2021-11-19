namespace Deceit.Domain.Game.Players;

/// <summary>
/// The Forensic Scientist player
/// </summary>
public class ForensicScientist
{
    public string PlayerId { get; }

    public ForensicScientist(string playerId)
    {
        PlayerId = playerId;
    }
}
