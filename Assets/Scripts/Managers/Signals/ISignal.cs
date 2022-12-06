namespace DefaultNamespace.Signals
{
    public interface ISignal
    {
        string Hash { get; }
        void RemoveAllListeners();
    }
}