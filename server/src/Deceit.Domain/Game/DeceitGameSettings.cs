using Deceit.Domain.Players;

namespace Deceit.Domain.Game;

public class DeceitGameSettings
{
    public Player? ForensicScientist { get; set; }

    public int NumberOfEvidenceCards { get; } = 4;
}
