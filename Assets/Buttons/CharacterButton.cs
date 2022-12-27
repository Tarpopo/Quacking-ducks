using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class CharacterButton : BaseTriggerButton
{
    [SerializeField] private Ducks _duckType;
    private OnCharacterSelected _onCharacterSelected;
    private DataContainer _dataContainer;

    protected override void OnButtonDown(PointerEventData eventData)
    {
        base.OnButtonDown(eventData);
        _onCharacterSelected.Dispatch(_duckType);
    }

    public override void OnStart(GameObject gameObject)
    {
        // _isActiveSavable = new SavableBool(gameObject.transform.parent.name);
        // _isActive = _isActiveSavable.Value;
        base.OnStart(gameObject);
        _dataContainer = Toolbox.Get<DataContainer>();
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
            _dataContainer.SetCurrentDuck(_duckType);
        }
        else
        {
            _image.sprite = _fullButton;
            _isActive.Value = false;
        }
    }
}