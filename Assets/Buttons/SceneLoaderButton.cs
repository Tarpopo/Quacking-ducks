using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class SceneLoaderButton : BaseButton
{
    [SerializeField, Scene] private string _loadingScene;

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
        Toolbox.Get<LevelLoader>().LoadLevel(_loadingScene);
    }
}