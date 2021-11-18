namespace Deceit.Backend.Domain.Game.States.Actions;

public abstract class ActionBase
{
    public string PlayerId { get; set; }
    public string Action { get; set; }
}

public abstract class ActionBase<T> : ActionBase
{
    public T Data { get; set; }
}
