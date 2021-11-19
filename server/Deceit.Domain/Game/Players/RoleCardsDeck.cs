namespace Deceit.Domain.Game.Players;

public class InvestigatorRoleCardsDeck : Deck<string>
{
    public InvestigatorRoleCardsDeck(int numberOfPlayers) : base(GenerateDeckForPlayers(numberOfPlayers))
    {
    }

    private static List<string> GenerateDeckForPlayers(int numberOfPlayers)
    {
        List<string> initialCards = new();
        initialCards.Add(Roles.Murderer);
        // Start at 1 since there's 1 Murderer card already in the deck
        for (int i = 1; i < numberOfPlayers; i++)
        {
            initialCards.Add(Roles.Investigator);
        }
        return initialCards;
    }
}
