using System;
using UnityEngine;

[Serializable]
public class CharacterButton : BaseTriggerButton
{
    [SerializeField] private Ducks _duckType;
    private OnCharacterSelected _onCharacterSelected;

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
        _onCharacterSelected.Dispatch(_duckType);
    }

    public override void OnStart(GameObject gameObject)
    {
        // _isActiveSavable = new SavableBool(gameObject.transform.parent.name);
        // _isActive = _isActiveSavable.Value;
        base.OnStart(gameObject);
        _onCharacterSelected = Toolbox.Get<Signals>().Get<OnCharacterSelected>();
        _onCharacterSelected.AddListener(TryActiveButton);
    }

    public override void OnDestroy() => _onCharacterSelected.RemoveListener(TryActiveButton);

    // public override void OnDisable()
    // {
    //     base.OnDisable();
    //     _isActiveSavable.Save();
    // }

    private void TryActiveButton(Ducks duckType)
    {
        if (duckType.Equals(_duckType))
        {
            _isActive.Value = true;
        }
        else
        {
            _image.sprite = _fullButton;
            _isActive.Value = false;
        }
    }
}