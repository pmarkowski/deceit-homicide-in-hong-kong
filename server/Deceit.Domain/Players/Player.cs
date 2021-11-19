namespace Deceit.Domain.Players;

public class Player
{
    public string ConnectionId { get; }
    public string Name { get; }
    public bool IsConnected { get; set; }

    public Player(string connectionId, string name, bool isConnected)
    {
        Name = name;
        ConnectionId = connectionId;
        IsConnected = isConnected;
    }
}
