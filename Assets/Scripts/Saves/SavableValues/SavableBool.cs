using UnityEngine;

public class SavableBool : SavableValue<bool>
{
    public SavableBool(string saveKey, bool defaultValue = default) : base(saveKey, defaultValue)
    {
    }

    protected override void SaveValue(bool value) => PlayerPrefs.SetInt("DataValue " + SaveKey, value ? 1 : 0);

    protected override bool GetSaveValue() => PlayerPrefs.GetInt("DataValue " + SaveKey, _defaultValue ? 1 : 0) == 1;
}