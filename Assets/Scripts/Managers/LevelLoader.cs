using System.Collections.Generic;
using System.Diagnostics;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : ManagerBase, IAwake, IStart
{
    [SerializeField, Scene] private string _mainScene;
    [SerializeField, Scene] private List<string> _scenes;

    private string _currentScene;

    // private Stopwatch _levelStopwatch;
    public void OnAwake()
    {
        // SceneManager.
        // SceneManager.LoadSceneAsync(_scenes[0], LoadSceneMode.Additive);
        // SceneManager.UnloadSceneAsync(_scenes[0]);
        // SetOnlyScene(_mainScene);
    }

    public void OnStart()
    {
        SetOnlyScene(_mainScene);

        // _levelStopwatch = new Stopwatch();
        // _levelStopwatch.Start();
        // _currentScene = _scenes[0];
        // SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
        // GlobalEvents.Instance.InvokeAction(GlobalEventType.OnLevelLoad);
    }

    //public void LoadLevel(int buildIndex) => SceneManager.LoadScene(buildIndex);

    public void LoadNextLevel()
    {
        // _levelStopwatch.Stop();
        // MyAnalytics.Instance.LevelComplete(_scenes.IndexOf(_currentScene) + 1, _levelStopwatch.Elapsed.Seconds,
        //     _currentScene);
        SceneManager.UnloadSceneAsync(_currentScene);
        // _levelStopwatch.Reset();
        // _levelStopwatch.Start();
        // _scenes.TryGetNextItem(_currentScene, out _currentScene);
        // MyAnalytics.Instance.LevelStart(_scenes.IndexOf(_currentScene) + 1);
        SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
        // GlobalEvents.Instance.InvokeAction(GlobalEventType.OnLevelLoad);
    }

    public void RestartLevel()
    {
        // _levelStopwatch.Stop();
        SceneManager.UnloadSceneAsync(_currentScene);
        // MyAnalytics.Instance.LevelFail(_scenes.IndexOf(_currentScene) + 1, _levelStopwatch.Elapsed.Seconds,
        //     FailReason.WaterFull, SceneManager.GetActiveScene().name);
        // _levelStopwatch.Start();
        // MyAnalytics.Instance.LevelRestart(_scenes.IndexOf(_currentScene) + 1);
        print(_currentScene);
        SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
        // GlobalEvents.Instance.InvokeAction(GlobalEventType.OnLevelLoad);
    }

    public void LoadLevel(string levelName)
    {
        
    }

    private void SetOnlyScene(string sceneName)
    {
        var countLoaded = SceneManager.sceneCount;

        for (int i = 0; i < countLoaded; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Equals(sceneName)) continue;
            SceneManager.UnloadSceneAsync(scene);
        }
        // int c = SceneManager.sceneCount;
        // for (int i = 0; i < c; i++)
        // {
        //     Scene scene = SceneManager.GetSceneAt(i);
        //     print(scene.name);
        //     if (scene.name != sceneName)
        //     {
        //         SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        //         print("unload");
        //     }
        // }
    }
}