using Deceit.Backend.Hubs;
using Deceit.Domain.Game.States.Actions;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace Deceit.Backend.Tests.Hubs;

public class ActionFactoryTests
{
    [Fact]
    public void CreateAction_ActionPassedIn_ShouldReturnDeserializedAction()
    {
        SelectCrimeSolutionAction selectMeansOfMurderAction = new SelectCrimeSolutionAction(new("evidence", "meansOfMurder"));
        var document = JsonSerializer.SerializeToDocument(selectMeansOfMurderAction);
        var result = ActionFactory.CreateAction("SelectCrimeSolutionAction", document);

        result.Should().BeAssignableTo<SelectCrimeSolutionAction>()
            .And.BeEquivalentTo(selectMeansOfMurderAction);
    }
}
