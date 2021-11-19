namespace Deceit.Domain.Game.Players;

/// <summary>
/// A player that is an investigator. No hidden information
/// is accessible.
/// </summary>
public class InvestigatorWithoutRole
{
    public string PlayerId { get; }
    public IEnumerable<string> MeansOfMurderCards { get; }
    public IEnumerable<string> EvidenceCards { get; }

    public InvestigatorWithoutRole(
        string playerId,
        IEnumerable<string> meansOfMurderCards,
        IEnumerable<string> evidenceCards)
    {
        PlayerId = playerId;
        MeansOfMurderCards = meansOfMurderCards;
        EvidenceCards = evidenceCards;
    }
}
