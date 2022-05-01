namespace Deceit.Domain.Game;

public class DeceitGameSettings
{
    public string ForensicScientistId { get; init; }

    public int NumberOfEvidenceCards { get; } = 4;

    public DeceitGameSettings(string forensicScientistId)
    {
        ForensicScientistId = forensicScientistId;
    }
}
