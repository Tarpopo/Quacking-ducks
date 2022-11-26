using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class LevelButtonn : LockButton
{
    [SerializeField, Scene] private string _loadingScene;

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
        Toolbox.Get<LevelLoader>().LoadLevel(_loadingScene);
    }
}