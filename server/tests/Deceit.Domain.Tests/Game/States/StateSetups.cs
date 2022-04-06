using Deceit.Domain.Game.States;
using Deceit.Domain.Game.States.Actions;
using Deceit.Domain.Players;

static class StateSetups
{
    public static void SetupContextWithPlayers(this DeceitContext context, IEnumerable<Player> players, Player forensicScientist)
    {
        foreach (var player in players)
        {
            context.Handle(new AddPlayerAction(player));
        }

        context.Handle(new SetForensicScientistAction(new(forensicScientist.PlayerId)));
    }
}
