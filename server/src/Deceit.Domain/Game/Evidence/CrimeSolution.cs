namespace Deceit.Domain.Game.Evidence;

/// <summary>
/// The Clue and Means selected by the Murderer
/// </summary>
public class CrimeSolution
{
    public string KeyEvidence { get; }
    public string MeansOfMurder { get; }

    public CrimeSolution(string keyEvidence, string meansOfMurder)
    {
        KeyEvidence = keyEvidence;
        MeansOfMurder = meansOfMurder;
    }
}
