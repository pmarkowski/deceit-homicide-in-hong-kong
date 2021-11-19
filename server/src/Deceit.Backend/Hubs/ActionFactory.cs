using Deceit.Domain.Game.States.Actions;
using System.Text.Json;

namespace Deceit.Backend.Hubs;

public static class ActionFactory
{
    public static ActionBase CreateAction(string actionType, JsonDocument serializedAction)
    {
        var actionAssembly = typeof(ActionBase).Assembly;
        var type = actionAssembly.GetType($"{typeof(ActionBase).Namespace}.{actionType}");

        if (type == null)
        {
            throw new Exception("Tried to create invalid action");
        }
        var deserializedAction = serializedAction.Deserialize(type) as ActionBase;
        if (deserializedAction is null)
        {
            throw new Exception("Error deserializing action");
        }

        return deserializedAction;
    }
}
