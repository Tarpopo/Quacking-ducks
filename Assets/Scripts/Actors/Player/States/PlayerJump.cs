using FSM;
using Managers;
using UnityEngine;

public class PlayerJump : State<PlayerData>
{
    private readonly InputManager _inputManager;

    public PlayerJump(PlayerData data, StateMachine<PlayerData> stateMachine) : base(data, stateMachine)
    {
        _inputManager = Toolbox.Get<InputManager>();
    }

    public override void Enter()
    {
        Data.Rigidbody2D.velocity = Vector2.up * Data.JumpForce;
    }

    private void Move(int direction)
    {
        Data.Transform.localScale = new Vector3(direction, 1, 1);
        Data.Rigidbody2D.velocity = new Vector2(Data.MoveSpeed * direction, Data.Rigidbody2D.velocity.y);
        // Data.Animator.Play(CheckGround(jumpRadius) ? data.run.name : data.idle.name);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        if (_inputManager.IsMovePressed) Move(_inputManager.MoveDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }
}