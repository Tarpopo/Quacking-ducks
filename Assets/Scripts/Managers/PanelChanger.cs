using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PanelChanger : ManagerBase, IAwake, IStart
{
    [SerializeField] private List<Panel> _panels;
    private Dictionary<Panels, Panel> _panelsDictionary;
    private Panel _currentPanel;

    public void OnAwake()
    {
        _panelsDictionary = new Dictionary<Panels, Panel>();
        foreach (var panel in _panels) _panelsDictionary.Add(panel.PaneType, panel);
    }

    public void OnStart()
    {
        _currentPanel ??= _panels[0];
        var levelLoader = Toolbox.Get<LevelLoader>();
        levelLoader.OnLevelLoadedStart += DisableCurrentPanel;
        levelLoader.OnLevelLoaded += ActivateGameUI;
        // levelLoader.OnLevelUnloadedStart += DisableCurrentPanel;
        levelLoader.OnLevelUnloadedStart += DisableCurrentPanelWithAnimation;
        levelLoader.OnLevelUnloadedStart += ActivateMainMenu;
    }

    public void ActivatePanel(Panels panelType)
    {
        _currentPanel.DisablePanel(() => EnablePanel(panelType));
    }

    public void DisableCurrentPanel() => _currentPanel.SetActivePanel(false);
    public void DisableCurrentPanelWithAnimation() => _currentPanel.DisablePanel();

    public void ActivateGameUI() => EnablePanel(Panels.GameUI);
    public void ActivateMainMenu() => EnablePanel(Panels.MainMenu);

    private void EnablePanel(Panels panelType)
    {
        _panelsDictionary[panelType].EnablePanel();
        _currentPanel = _panelsDictionary[panelType];
    }

    private void DisablePanel(Panels panelType)
    {
        _panelsDictionary[panelType].DisablePanel();
        _currentPanel = _panelsDictionary[panelType];
    }
}

[Serializable]
public class Panel
{
    public Panels PaneType => _panelType;
    [SerializeField] private Panels _panelType;
    [SerializeField] private GameObject _panel;
    [SerializeReference] private BasePanelChange _basePanelChange;

    public void SetActivePanel(bool active) => _panel.SetActive(active);
    public void DisablePanel(Action onDisable = null) => _basePanelChange.Disable(_panel, onDisable);
    public void EnablePanel(Action onEnable = null) => _basePanelChange.Enable(_panel, onEnable);
}

public abstract class BasePanelChange
{
    public abstract void Enable(GameObject panel, Action onEnable);

    public abstract void Disable(GameObject panel, Action onDisable);
}

[Serializable]
public class PanelChangeTransition : BasePanelChange
{
    public override void Enable(GameObject panel, Action onEnable)
    {
        panel.SetActive(true);
        Toolbox.Get<Transition>().PlayOpenAnimation(onEnable);
    }

    public override void Disable(GameObject panel, Action onDisable)
    {
        Toolbox.Get<Transition>().PlayCloseAnimation(() =>
        {
            panel.SetActive(false);
            onDisable?.Invoke();
        });
    }
}

[Serializable]
public class PanelChangeAnimation : BasePanelChange
{
    [SerializeField] private TweenSequencer _sequencer;

    public override void Enable(GameObject panel, Action onEnable)
    {
        // _sequencer.SetStartValues();
        panel.SetActive(true);
        _sequencer.PlayForward(onEnable);
    }

    public override void Disable(GameObject panel, Action onDisable)
    {
        _sequencer.PlayBackward(() =>
        {
            panel.SetActive(false);
            onDisable?.Invoke();
        });
    }
}