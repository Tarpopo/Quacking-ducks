using UnityEngine;

public class SavableString : SavableValue<string>
{
    public SavableString(string saveKey) : base(saveKey)
    {
    }

    protected override void SaveValue(string value) => PlayerPrefs.SetString("DataValue " + SaveKey, value);

    protected override string GetSaveValue() => PlayerPrefs.GetString("DataValue " + SaveKey, _defaultValue);
}