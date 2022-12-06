using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PanelChanger : ManagerBase, IAwake, IStart
{
    [SerializeField] private List<Panel> _panels;
    private Dictionary<Panels, Panel> _panelsDictionary;
    private Transition _transition;
    private bool _isChanging;

    public void OnAwake()
    {
        _panelsDictionary = new Dictionary<Panels, Panel>();
        foreach (var panel in _panels) _panelsDictionary.Add(panel.PaneType, panel);
    }

    public void OnStart() => _transition = Toolbox.Get<Transition>();

    public void ActivatePanel(Panels panelType)
    {
        if (_isChanging) return;
        _isChanging = true;
        _transition.DoTransitionAnimation(() =>
        {
            foreach (var panel in _panels) panel.DisablePanel();
            _panelsDictionary[panelType].EnablePanel();
            _isChanging = false;
        });
    }
}

[Serializable]
public class Panel
{
    public Panels PaneType => _panelType;
    [SerializeField] private Panels _panelType;
    [SerializeField] private GameObject _panelObject;
    public void DisablePanel() => _panelObject.SetActive(false);
    public void EnablePanel() => _panelObject.SetActive(true);
}