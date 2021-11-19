namespace Deceit.Domain.Game.Evidence;

/// <summary>
/// Key Evidence selected by the Murderer
/// </summary>
public class KeyEvidence
{
    public string Evidence { get; }
    public string MeansOfMurder { get; }

    public KeyEvidence(string evidence, string meansOfMurder)
    {
        Evidence = evidence;
        MeansOfMurder = meansOfMurder;
    }
}
