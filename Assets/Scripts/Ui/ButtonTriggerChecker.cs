using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerChecker : MonoBehaviour
{
    [SerializeField] private TriggerButton[] _buttons;

    public void DisableActiveButton()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (_buttons[i]._isTriggerActive == false) continue;
            _buttons[i].SetActiveFull();
        }
    }
}