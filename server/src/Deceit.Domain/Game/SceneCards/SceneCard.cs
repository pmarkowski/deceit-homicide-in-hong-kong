namespace Deceit.Domain.Game.SceneCards;

class SceneCard
{
    public string Title { get; }

    public IEnumerable<string> Options { get; }

    public SceneCard(string title, IEnumerable<string> options)
    {
        Title = title;
        Options = options;
    }
}
