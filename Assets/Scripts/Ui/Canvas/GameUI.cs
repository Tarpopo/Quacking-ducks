using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseCanvas
{
    public TMP_Text Ammo => _ammo;
    public TMP_Text Coins => _coins;
    public Image Health => _health;
    public UIButton FButton => _fButton;
    public UIButton ShootButton => _shootButton;
    public UIButton JumpButton => _jumpButton;
    public UIButton MoveButtons => _moveButtons;

    [SerializeField] private TMP_Text _ammo;
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private Image _health;
    [SerializeField] private UIButton _fButton;
    [SerializeField] private UIButton _shootButton;
    [SerializeField] private UIButton _jumpButton;
    [SerializeField] private UIButton _moveButtons;
}