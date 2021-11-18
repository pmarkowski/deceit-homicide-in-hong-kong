using Deceit.Backend.Domain.Game.States;
using Deceit.Backend.Domain.Game.States.Actions;
using Xunit;

namespace Deceit.Backend.Tests;

public class StartGameActionTest
{
    [Fact]
    public void Test1()
    {
        DeceitContext context = new();
        context.Handle(new StartGameAction());
    }
}