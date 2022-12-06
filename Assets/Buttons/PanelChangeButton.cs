using System;
using UnityEngine;

[Serializable]
public class PanelChangeButton : BaseButton
{
    [SerializeField] private Panels _showingPanel;

    private void ChangePanel() => Toolbox.Get<PanelChanger>().ActivatePanel(_showingPanel);

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
        ChangePanel();
    }
}