using System.Collections;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : ManagerBase, IStart
{
    public event UnityAction OnLevelUnloadedStart;
    public event UnityAction OnLevelUnloaded;
    public event UnityAction OnLevelLoadedStart;
    public event UnityAction OnLevelLoaded;
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
        SetOnlyScene(_mainScene, true);
        if (_mainScene.Equals(levelName)) return;
        _transition.PlayCloseAnimation(() => StartCoroutine(LoadSceneAsync(levelName)));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        OnLevelLoadedStart?.Invoke();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        _transition.PlayOpenAnimation(() => OnLevelLoaded?.Invoke());
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        OnLevelUnloadedStart?.Invoke();
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneName);
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        _transition.PlayOpenAnimation(() => OnLevelUnloaded?.Invoke());
    }

    private void SetOnlyScene(string sceneName, bool transition = false)
    {
        var countLoaded = SceneManager.sceneCount;

        for (int i = 0; i < countLoaded; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Equals(sceneName)) continue;
            if (transition) _transition.PlayCloseAnimation(() => StartCoroutine(UnloadSceneAsync(scene.name)));
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