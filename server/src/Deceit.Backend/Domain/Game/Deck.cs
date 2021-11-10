namespace Deceit.Backend.Domain.Game;

/// <summary>
/// Class that simulates a deck of cards.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Deck<T>
{
    private List<T> cards;

    public Deck(List<T> cards)
    {
        this.cards = cards;
        Shuffle();
    }

    public int CardsRemaining => cards.Count;

    public T Draw()
    {
        var drawnCard = cards.FirstOrDefault();
        if (drawnCard is null)
        {
            throw new InvalidOperationException("Attempt to draw a card from empty deck");
        }
        cards.RemoveAt(0);
        return drawnCard;
    }

    public IEnumerable<T> Draw(int drawCount)
    {
        var drawnCards = new T[drawCount];
        for (int i = 0; i < drawCount; i++)
        {
            drawnCards[i] = Draw();
        }
        return drawnCards;
    }

    public void Shuffle()
    {
        var random = new Random();
        cards = cards
            .OrderBy(_ => random.Next())
            .ToList();
    }
}
