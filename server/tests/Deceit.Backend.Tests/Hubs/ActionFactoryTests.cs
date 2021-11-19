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
        SelectMeansOfMurderAction selectMeansOfMurderAction = new SelectMeansOfMurderAction(new("evidence", "meansOfMurder"));
        var document = JsonSerializer.SerializeToDocument(selectMeansOfMurderAction);
        var result = ActionFactory.CreateAction("SelectMeansOfMurderAction", document);

        result.Should().BeAssignableTo<SelectMeansOfMurderAction>()
            .And.BeEquivalentTo(selectMeansOfMurderAction);
    }
}
