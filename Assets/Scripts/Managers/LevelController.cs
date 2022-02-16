using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/LevelControl")]
public class LevelController : ManagerBase,IAwake,ISceneChanged
{
    public List<Level> ActiveLevels;
    private int _currentLevelIndex;
    private bool _isActive;
    
    public void OnChangeScene()
    {
    }
    
    public void OnAwake()
    {
        if (Toolbox.Get<Save>()._save.ActiveLevels.Count != 0)
        {
            ActiveLevels = Toolbox.Get<Save>()._save.ActiveLevels;
        }
        
        var objects = GameObject.FindWithTag("LevelsMenu").GetComponentsInChildren<LevelButton>();
        //var levelsController = Toolbox.Get<LevelController>();
        for (int i = 0; i < objects.Length; i++)
        {
            //Debug.Log(objects[i].gameObject);
            ActiveLevels[i]._level = objects[i];
            ActiveLevels[i]._level.Start();
            if (ActiveLevels[i].isLock == false)
            {
                objects[i]._isActive = true;
            }
            if(ActiveLevels[i].havePassedMark) objects[i].ActivePassedMark();
        }

        if (_isActive)
        {
            GameObject.FindGameObjectWithTag("MainMenu").SetActive(false);
            UnlockButton();
            return;
        }
        objects[0].transform.parent.gameObject.SetActive(false);
    }

    public override void ClearScene()
    {
        _isActive = true;
    }

    public void LoadLevel(int index)
    {
        var levelController=Toolbox.Get<LevelController>();
        if (levelController.ActiveLevels[index-1].isLock) return;
        levelController._currentLevelIndex = index-1;
        Toolbox.Get<SceneController>().LoadScene(index);
    }

    public void UnlockButton()
    {
        var levelController = Toolbox.Get<LevelController>();
        for (int i = 0; i < levelController.ActiveLevels.Count; i++)
        {
            if (levelController.ActiveLevels[i].isLock == false&&levelController.ActiveLevels[i]._level.IsFirstOpen)
            {
                levelController.ActiveLevels[i]._level.UnlockDoor();
                levelController.ActiveLevels[i]._level.IsFirstOpen = false;
            }
            //ActiveLevels[i].isFirstOpen = false;
        }
    }

    // public void ActivateAllGates()
    // {
    //     // var levelController = Toolbox.Get<LevelController>();
    //     // for (int i = 0; i < levelController.ActiveLevels.Count; i++)
    //     // {
    //     //     if (levelController.ActiveLevels[i].isLock == false)
    //     //     {
    //     //         levelController.ActiveLevels[i]._level.SetFirstState();
    //     //         levelController.ActiveLevels[i]._level.Gate.SetActive(true);
    //     //     }
    //     // }
    // }

    public void SetLevelPassed()
    {
        if (ActiveLevels[_currentLevelIndex].havePassedMark) return;
        ActiveLevels[_currentLevelIndex].havePassedMark = true;
        if (_currentLevelIndex < ActiveLevels.Count - 1) ActiveLevels[_currentLevelIndex + 1].isLock = false;
        Toolbox.Get<Save>()._save.ActiveLevels = ActiveLevels;
        Toolbox.Get<Save>().SaveAll();
    }
}
[Serializable]
public class Level
{
    public LevelButton _level;
    public bool isLock;
    //public bool isFirstOpen;
    public bool havePassedMark;
}
