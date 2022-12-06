using DefaultNamespace.Signals;

public abstract class BaseSignal : ISignal
{
    protected string _hash;

    public string Hash
    {
        get
        {
            if (string.IsNullOrEmpty(_hash))
            {
                _hash = GetType().ToString();
            }

            return _hash;
        }
    }

    public abstract void RemoveAllListeners();
}