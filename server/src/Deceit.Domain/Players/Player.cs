namespace Deceit.Domain.Players;

public class Player
{
    public string PlayerId { get; }
    public string Name { get; }
    public bool IsConnected { get; set; }

    public Player(string playerId, string name, bool isConnected)
    {
        Name = name;
        PlayerId = playerId;
        IsConnected = isConnected;
    }
}
