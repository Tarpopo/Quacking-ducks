using UnityEngine;

public class SavableFloat : SavableValue<float>
{
    public SavableFloat(string saveKey) : base(saveKey)
    {
    }

    protected override void SaveValue(float value) => PlayerPrefs.SetFloat("DataValue " + SaveKey, value);

    protected override float GetSaveValue() => PlayerPrefs.GetFloat("DataValue " + SaveKey, _defaultValue);
}