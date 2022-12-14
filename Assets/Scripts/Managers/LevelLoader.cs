using System;
using System.Collections;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : ManagerBase, IStart
{
    public event UnityAction<string> OnLevelUnloadedStart;
    public event UnityAction<string> OnLevelUnloaded;
    public event UnityAction<string> OnLevelLoadedStart;
    public event UnityAction<string> OnLevelLoaded;
    public string MainScene => _mainScene;
    public string EnabledLevel => _enabledLevel;
    [SerializeField, Scene] private string _mainScene;
    [SerializeField, Scene] private string _enabledLevel;
    private Transition _transition;

    public void OnStart()
    {
        _transition = Toolbox.Get<Transition>();
        SetOnlyScene(_mainScene);
    }

    public void LoadLevel(string levelName)
    {
        SetOnlyScene(_mainScene, true, () => OnLevelLoadedStart?.Invoke(_mainScene),
            () => OnLevelLoaded?.Invoke(_mainScene));
        if (_mainScene.Equals(levelName)) return;
        _transition.PlayCloseAnimation(() => StartCoroutine(LoadSceneAsync(levelName)));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        OnLevelLoadedStart?.Invoke(sceneName);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        _transition.PlayOpenAnimation(() => OnLevelLoaded?.Invoke(sceneName));
    }

    private IEnumerator UnloadSceneAsync(string sceneName, Action onStart = null, Action onEnd = null)
    {
        OnLevelUnloadedStart?.Invoke(sceneName);
        onStart?.Invoke();
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneName);
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        onEnd?.Invoke();
        OnLevelUnloaded?.Invoke(sceneName);
        _transition.PlayOpenAnimation();
    }

    private void SetOnlyScene(string sceneName, bool transition = false, Action onStart = null, Action onEnd = null)
    {
        var countLoaded = SceneManager.sceneCount;

        for (int i = 0; i < countLoaded; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Equals(sceneName)) continue;
            if (transition)
                _transition.PlayCloseAnimation(() => StartCoroutine(UnloadSceneAsync(scene.name, onStart, onEnd)));
            else SceneManager.UnloadSceneAsync(scene);
        }
    }

    private bool IsSceneLoad(string sceneName)
    {
        var countLoaded = SceneManager.sceneCount;

        for (int i = 0; i < countLoaded; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Equals(sceneName)) return true;
            SceneManager.UnloadSceneAsync(scene);
        }

        return false;
    }
}