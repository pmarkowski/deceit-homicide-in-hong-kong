namespace Deceit.Backend.Domain.Players;

public class Player
{
    public string ConnectionId { get; }
    public string Name { get; }

    public Player(string connectionId, string name)
    {
        Name = name;
        ConnectionId = connectionId;
    }
}
