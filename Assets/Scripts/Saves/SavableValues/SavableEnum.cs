using System;
using UnityEngine;

public class SavableEnum<T> : SavableValue<Enum> where T : Enum
{
    public SavableEnum(string saveKey, Enum defaultValue = default) : base(saveKey, defaultValue)
    {
    }

    protected override void SaveValue(Enum value) => PlayerPrefs.SetInt("DataValue " + SaveKey, Convert.ToInt32(value));

    protected override Enum GetSaveValue() => (T)Enum.ToObject(typeof(T),
        PlayerPrefs.GetInt("DataValue " + SaveKey, Convert.ToInt32(_defaultValue)));
}