using UnityEngine;

public class SavableInt : SavableValue<int>
{
    public SavableInt(string saveKey) : base(saveKey)
    {
    }

    protected override void SaveValue(int value) => PlayerPrefs.SetInt("DataValue " + SaveKey, value);

    protected override int GetSaveValue() => PlayerPrefs.GetInt("DataValue " + SaveKey, _defaultValue);
}