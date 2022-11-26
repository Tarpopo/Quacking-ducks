using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeReference] private BaseButton _baseButton;

    private void Start() => _baseButton.OnStart();
}

public enum Panels
{
    Menu,
    Shop,
    Levels
}