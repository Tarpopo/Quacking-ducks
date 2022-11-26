using System;
using UnityEngine;

[Serializable]
public class PanelChangeButton : BaseButton
{
    [SerializeField] private Panels _showingPanel;

    public void ChangePanel(Panels panel)
    {
    }
}