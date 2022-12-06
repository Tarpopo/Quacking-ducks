using System;

public abstract class SavableValue<T> where T : IComparable<T>
{
    public event Action<T> OnChangeValue;
    private T _value;
    protected T _defaultValue;
    protected readonly string SaveKey;

    protected SavableValue(string saveKey, T defaultValue = default)
    {
        SaveKey = saveKey;
        _defaultValue = defaultValue;
        _value = GetSaveValue();
    }

    public T Value
    {
        get => _value;

        set
        {
            _value = value;
            OnChangeValue?.Invoke(_value);
        }
    }

    public void Save() => SaveValue(_value);
    protected abstract void SaveValue(T value);
    protected abstract T GetSaveValue();
}