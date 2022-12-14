using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PanelChanger : ManagerBase, IAwake, IStart
{
    [SerializeField] private List<Panel> _panels;
    private Dictionary<Panels, Panel> _panelsDictionary;
    private Panel _currentPanel;
    private LevelLoader _levelLoader;

    public void OnAwake()
    {
        _panelsDictionary = new Dictionary<Panels, Panel>();
        foreach (var panel in _panels) _panelsDictionary.Add(panel.PaneType, panel);
    }

    public void OnStart()
    {
        _currentPanel ??= _panels[0];
        _levelLoader = Toolbox.Get<LevelLoader>();
        _levelLoader.OnLevelLoadedStart += DisableCurrentPanel;
        _levelLoader.OnLevelLoaded += CheckAndActiveUI;
        // _levelLoader.OnLevelUnloadedStart += DisableCurrentPanel;
        // _levelLoader.OnLevelUnloadedStart += ActivateMainMenu;
    }

    public void ActivatePanel(Panels panelType) =>
        _currentPanel.DisablePanel(() => EnablePanel(panelType));

    private void ActivateGameUI() => EnablePanel(Panels.GameUI);

    private void ActivateMainMenu() => EnablePanel(Panels.MainMenu, false);

    private void DisableCurrentPanel(string sceneName) => _currentPanel.SetActivePanel(false);

    private void CheckAndActiveUI(string sceneName)
    {
        if (sceneName.Equals(_levelLoader.MainScene))
        {
            ActivateMainMenu();
        }
        else
        {
            ActivateGameUI();
        }
    }

    private void EnablePanel(Panels panel, bool transition = true)
    {
        if (transition) _panelsDictionary[panel].EnablePanel();
        else _panelsDictionary[panel].SetActivePanel(true);
        _currentPanel = _panelsDictionary[panel];
    }
}

[Serializable]
public class Panel
{
    public Panels PaneType => _panelType;
    [SerializeField] private Panels _panelType;
    [SerializeField] private Canvas _panel;
    [SerializeReference] private BasePanelChange _basePanelChange;

    public void SetActivePanel(bool active) => _panel.gameObject.SetActive(active);
    public void DisablePanel(Action onDisable = null) => _basePanelChange.Disable(_panel, onDisable);
    public void EnablePanel(Action onEnable = null) => _basePanelChange.Enable(_panel, onEnable);
}

public abstract class BasePanelChange
{
    public abstract void Enable(Canvas panel, Action onEnable);

    public abstract void Disable(Canvas panel, Action onDisable);
}

[Serializable]
public class PanelChangeTransition : BasePanelChange
{
    public override void Enable(Canvas panel, Action onEnable)
    {
        panel.gameObject.SetActive(true);
        Toolbox.Get<Transition>().PlayOpenAnimation(onEnable);
    }

    public override void Disable(Canvas panel, Action onDisable)
    {
        Toolbox.Get<Transition>().PlayCloseAnimation(() =>
        {
            panel.gameObject.SetActive(false);
            onDisable?.Invoke();
        });
    }
}

[Serializable]
public class PanelChangeAnimation : BasePanelChange
{
    [SerializeField] private TweenSequencer _sequencer;

    public override void Enable(Canvas panel, Action onEnable)
    {
        _sequencer.SetStartValues();
        panel.gameObject.SetActive(true);
        _sequencer.PlayForward(onEnable);
    }

    public override void Disable(Canvas panel, Action onDisable)
    {
        _sequencer.PlayBackward(() =>
        {
            panel.gameObject.SetActive(false);
            onDisable?.Invoke();
        });
    }
}