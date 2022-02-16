using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Managers/EndLevelChecker")]
public class EndLevelChecker : ManagerBase, ISceneChanged, IAwake
{
    public AnimationClip _fallingDeathScreen;
    public AnimationClip _fallingCompleteScreen;
    
    private GameObject _menuCanvas;
    private GameObject _deathCanvas;
    private GameObject _completeCanvas;
    private int _killCountToComplete;
    private int _killCount;
    private bool _isLockScreen;
    private bool _isFirstLoad = true;
    private EndSceneComponent _endSceneComponent;

    public void OnAwake()
    {
        if (_isFirstLoad == false)
        {
            var transition = FindObjectOfType<Transition>();
            transition.OnStart();
            transition.PlayGateUp();
            GameObject.Find("PresentLogo").SetActive(false);
            return;
        }
        
        _isFirstLoad = false;
    }

    public void OnChangeScene()
    {
        if (Toolbox.Get<SceneController>().GetIsMainScene()) return;
        _endSceneComponent = GameObject.Find("[Setup]").AddComponent<EndSceneComponent>();
        
        _isLockScreen = false;
        _menuCanvas = GameObject.FindGameObjectWithTag("LevelsMenu"); 
        _deathCanvas = GameObject.FindGameObjectWithTag("DeathScreen"); 
        _completeCanvas = GameObject.FindGameObjectWithTag("Finish");
        _menuCanvas.SetActive(false);
        _deathCanvas.SetActive(false);
        _completeCanvas.SetActive(false);
    } 
    
    public void SetCompleteCount(int count)
    {
        _killCount = 0;
        _killCountToComplete = count;
    }

    public void AddDeathCount()
    {
        _killCount++;
        CheckCompleteLevel();
    }

    private void CheckCompleteLevel()
    {
        if (_killCount < _killCountToComplete) return;
        Toolbox.Get<LevelController>().SetLevelPassed();
        LoadCompleteMenuAnimation();
    }
    
    public void LoadDeathScreenAnimation()
    {
        if (GetScreenState()) return;
        _deathCanvas.SetActive(true);
        _deathCanvas.GetComponent<Animator>().Play(_fallingDeathScreen.name);
        _endSceneComponent.ActiveGOWtihTime(_menuCanvas,1.5f);
    }
    private void LoadCompleteMenuAnimation()
    {
        if (GetScreenState()) return;
        _completeCanvas.SetActive(true);
        _completeCanvas.GetComponent<Animator>().Play(_fallingCompleteScreen.name);
        _endSceneComponent.LoadMainSceneWithTime(2.5f);
    }

    private bool GetScreenState()
    {
        var state = _isLockScreen;
        _isLockScreen = true;
        return state;
    }

    public void UnlockScreen()
    {
        _isLockScreen = false;
        CheckCompleteLevel();
    }


}
