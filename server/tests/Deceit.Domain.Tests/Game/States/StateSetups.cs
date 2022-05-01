using Deceit.Domain.Game;
using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Players;

static class StateSetups
{
    public static void SetupContextWithPlayers(this DeceitGame game, IEnumerable<Player> players, Player forensicScientist)
    {
        foreach (var player in players)
        {
            game.HandleAction(new AddPlayerAction(player));
        }

        game.HandleAction(new SetForensicScientistAction(new(forensicScientist.PlayerId)));
    }
}
