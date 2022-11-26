using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Save : ManagerBase, IAwake, ISceneChanged
{
    private const string _saveName = "QuackingSave";
    public SaveData _save;
    private bool _isFirstLoad = true;

    public void OnAwake()
    {
        if (_isFirstLoad == false) return;
        _save = LoadDataSave();
        _isFirstLoad = false;
    }

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

    public void SaveAll()
    {
        _save.MoneyCount = Toolbox.Get<PlayerStats>().CoinCount;
        PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(_save));
    }

    public void OnChangeScene()
    {
        //_isSceneClear = false;
    }
}

public class SaveData
{
    public List<ShopItem> ShopItems = new List<ShopItem>();
    public List<Level> ActiveLevels = new List<Level>();
    public int MoneyCount;
    public bool Volume = true;
}