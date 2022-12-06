using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : ManagerBase, IStart
{
    public string MainScene => _mainScene;
    public string EnabledLevel => _enabledLevel;
    [SerializeField, Scene] private string _mainScene;
    [SerializeField, Scene] private string _enabledLevel;

    public void OnStart() => SetOnlyScene(_mainScene);

    public void LoadLevel(string levelName)
    {
        SetOnlyScene(_mainScene);
        if (_mainScene.Equals(levelName)) return;
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
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