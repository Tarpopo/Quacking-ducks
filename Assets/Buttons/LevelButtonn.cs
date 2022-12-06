using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class LevelButtonn : LockButton
{
    [SerializeField] private Animator _animator;
    [SerializeField, Scene] private string _loadingScene;
    private SavableBool _isUnlock;

    public override void OnStart(GameObject gameObject)
    {
        _isUnlock = new SavableBool(_loadingScene);
        base.OnStart(gameObject);
    }

    protected override void TryActive()
    {
        if (_loadingScene.Equals(Toolbox.Get<LevelLoader>().EnabledLevel)) _isUnlock.Value = true;
        if (_isUnlock.Value == false) return;
        _animator.PlayStateAnimation(LockAnimation.Open).AddOnComplete(() => _animator.enabled = false);
    }

    protected override void ActiveButtonDown() => Toolbox.Get<LevelLoader>().LoadLevel(_loadingScene);

    protected override bool IsActive() => _isUnlock.Value;
    public override void OnDisable() => _isUnlock.Save();
}