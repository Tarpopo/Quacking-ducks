using System;
using DefaultNamespace;
using UnityEngine;

[Serializable]
public abstract class LockButton : BaseButton
{
    [SerializeField] protected SimpleSound _lockSound;
    // protected bool _isActive;

    public override void OnStart(GameObject gameObject)
    {
        base.OnStart(gameObject);
        TryActive();
    }

    protected override void OnButtonDown()
    {
        if (IsActive() == false)
        {
            _audioPlayer.PlaySound(_lockSound);
            return;
        }

        base.OnButtonDown();
        ActiveButtonDown();

        // _audioSource.PlaySound(_buttonClick);
    }

    protected abstract void ActiveButtonDown();
    protected abstract void TryActive();
    protected abstract bool IsActive();
}