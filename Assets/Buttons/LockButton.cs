using System;
using DefaultNamespace;
using UnityEngine;

[Serializable]
public abstract class LockButton : BaseButton
{
    [SerializeField] private SimpleSound _lockSound;
    protected bool _isActive;

    protected override bool IsActive()
    {
        if (_isActive == false) _audioSource.PlaySound(_lockSound);
        return _isActive;
    }

    protected override void OnButtonDown()
    {
        if (_isActive == false)
        {
            _audioSource.PlaySound(_lockSound);
            return;
        }

        _audioSource.PlaySound(_buttonClick);
    }
}