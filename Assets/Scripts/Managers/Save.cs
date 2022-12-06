using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Save : ManagerBase, IAwake
{
    private const string _saveName = "QuackingSave";
    private SaveData _saveData;

    public void OnAwake() => _saveData = LoadDataSave();

    private SaveData LoadDataSave()
    {
        return PlayerPrefs.HasKey(_saveName)
            ? JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(_saveName))
            : new SaveData();
    }

    public override void ClearScene()
    {
        if (Toolbox.Get<SceneController>().GetIsMainScene()) return;
        SaveAll();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
    }

    public void SaveAll()
    {
        // _saveData.MoneyCount = Toolbox.Get<PlayerStats>().CoinCount;
        PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(_saveData));
    }
}

[Serializable]
public class HashItemSaver
{
    private List<HashItem> _hashItems;
    // public bool
}

[Serializable]
public class HashItem
{
}

[Serializable]
public class SaveData
{
    // public List<ShopItem> ShopItems = new List<ShopItem>();
    // public List<Level> ActiveLevels = new List<Level>();
    // public int MoneyCount;
    // public bool Volume = true;
    public PlayerStats PlayerStats;
    public Settings Settings;
}

[Serializable]
public class PlayerStats
{
    public int Coins;
}

[Serializable]
public class Settings
{
    public bool Volume;
}