namespace Deceit.Domain.Game.States.Actions;

public abstract class ActionBase
{
}

public abstract class ActionBase<T> : ActionBase
{
    public T Data { get; }

    public ActionBase(T data)
    {
        Data = data;
    }
}
