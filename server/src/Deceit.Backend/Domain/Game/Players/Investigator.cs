namespace Deceit.Backend.Domain.Game.Players;

/// <summary>
/// A player that is an investigator. Investigators have a
/// hidden role that alter how they play the game.
/// </summary>
class Investigator : InvestigatorWithoutRole
{
    public string Role { get; }

    public Investigator(
        string playerId,
        string role,
        IEnumerable<string> meansOfMurderCards,
        IEnumerable<string> evidenceCards)
         : base(playerId, meansOfMurderCards, evidenceCards)
    {
        Role = role;
    }
}
