using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class SceneLoaderButton : BaseButton
{
    [SerializeField, Scene] private string _loadingScene;

    protected override void OnButtonDown(PointerEventData eventData)
    {
        base.OnButtonDown();
        Toolbox.Get<LevelLoader>().LoadLevel(_loadingScene);
    }
}