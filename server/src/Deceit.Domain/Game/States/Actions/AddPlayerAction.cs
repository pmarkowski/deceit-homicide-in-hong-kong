using Deceit.Domain.Players;

namespace Deceit.Domain.Game.States.Actions;

public class AddPlayerAction : ActionBase<Player>
{
    public AddPlayerAction(Player data) : base(data)
    {
    }
}
