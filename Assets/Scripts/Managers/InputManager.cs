using DefaultNamespace;
using UnityEngine.Events;

namespace Managers
{
    public class InputManager : ManagerBase, IStart
    {
        public event UnityAction OnMoveDown
        {
            add => _gameUI.MoveButtons.BaseButton.ButtonDown += value;
            remove => _gameUI.MoveButtons.BaseButton.ButtonDown -= value;
        }

        public event UnityAction OnMoveUp
        {
            add => _gameUI.MoveButtons.BaseButton.ButtonUp += value;
            remove => _gameUI.MoveButtons.BaseButton.ButtonUp -= value;
        }

        public event UnityAction OnShootDown
        {
            add => _gameUI.ShootButton.BaseButton.ButtonDown += value;
            remove => _gameUI.ShootButton.BaseButton.ButtonDown -= value;
        }

        public event UnityAction OnShootUp
        {
            add => _gameUI.ShootButton.BaseButton.ButtonUp += value;
            remove => _gameUI.ShootButton.BaseButton.ButtonUp -= value;
        }

        public event UnityAction OnJumpDown
        {
            add => _gameUI.JumpButton.BaseButton.ButtonDown += value;
            remove => _gameUI.JumpButton.BaseButton.ButtonDown -= value;
        }

        public event UnityAction OnJumpUp
        {
            add => _gameUI.JumpButton.BaseButton.ButtonUp += value;
            remove => _gameUI.JumpButton.BaseButton.ButtonUp -= value;
        }

        public event UnityAction OnFButtonDown
        {
            add => _gameUI.FButton.BaseButton.ButtonDown += value;
            remove => _gameUI.FButton.BaseButton.ButtonDown -= value;
        }

        public event UnityAction OnFButtonUp
        {
            add => _gameUI.FButton.BaseButton.ButtonUp += value;
            remove => _gameUI.FButton.BaseButton.ButtonUp -= value;
        }

        public int MoveDirection => _moveButton.MoveDirection;
        public bool IsMovePressed => _moveButton.IsPressed;
        private GameUI _gameUI;
        private MoveButton _moveButton;

        public void OnStart()
        {
            _gameUI = Toolbox.Get<CanvasContainer>().GetCanvas<GameUI>();
            _moveButton = (MoveButton)_gameUI.MoveButtons.BaseButton;
        }
    }
}