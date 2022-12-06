using System;
using DefaultNamespace.Signals;

public class Signals : ManagerBase
{
    private readonly SignalHub _hub = new SignalHub();

    public T Get<T>() where T : ISignal, new() => _hub.Get<T>();

    public void AddListenerToHash(string signalHash, Action handler) => _hub.AddListenerToHash(signalHash, handler);

    public void RemoveListenerFromHash(string signalHash, Action handler) =>
        _hub.RemoveListenerFromHash(signalHash, handler);
}