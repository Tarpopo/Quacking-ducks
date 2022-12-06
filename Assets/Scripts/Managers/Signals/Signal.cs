using System;

public abstract class Signal : BaseSignal
{
    private Action _callback;

    public void AddListener(Action handler) => _callback += handler;

    public void RemoveListener(Action handler) => _callback -= handler;
    public override void RemoveAllListeners() => _callback = null;

    public void Dispatch() => _callback?.Invoke();
}

public abstract class Signal<T> : BaseSignal
{
    private Action<T> _callback;

    public void AddListener(Action<T> handler) => _callback += handler;

    public void RemoveListener(Action<T> handler) => _callback -= handler;

    public override void RemoveAllListeners() => _callback = null;

    public void Dispatch(T arg1) => _callback?.Invoke(arg1);
}

public abstract class Signal<T, U> : BaseSignal
{
    private Action<T, U> _callback;

    public void AddListener(Action<T, U> handler) => _callback += handler;

    public void RemoveListener(Action<T, U> handler) => _callback -= handler;

    public override void RemoveAllListeners() => _callback = null;

    public void Dispatch(T arg1, U arg2) => _callback?.Invoke(arg1, arg2);
}

public abstract class Signal<T, U, V> : BaseSignal
{
    private Action<T, U, V> _callback;

    public void AddListener(Action<T, U, V> handler) => _callback += handler;

    public void RemoveListener(Action<T, U, V> handler) => _callback -= handler;

    public override void RemoveAllListeners() => _callback = null;

    public void Dispatch(T arg1, U arg2, V arg3) => _callback?.Invoke(arg1, arg2, arg3);
}