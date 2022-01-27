namespace Deceit.Domain.Game;

public abstract class PlayerGameInformation
{
    public string Role { get; }

    public PlayerGameInformation(string role)
    {
        Role = role;
    }
}
