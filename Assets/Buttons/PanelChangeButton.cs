using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class PanelChangeButton : BaseButton
{
    [SerializeField] private Panels _showingPanel;

    private void ChangePanel() => Toolbox.Get<PanelChanger>().ActivatePanel(_showingPanel);

    protected override void OnButtonDown(PointerEventData eventData)
    {
        base.OnButtonDown();
        ChangePanel();
    }
}