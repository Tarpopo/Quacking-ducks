using FSM;
using Managers;
using UnityEngine;

public class PlayerMove : State<PlayerData>
{
    private readonly InputManager _inputManager;

    public PlayerMove(PlayerData data, StateMachine<PlayerData> stateMachine) : base(data, stateMachine)
    {
        _inputManager = Toolbox.Get<InputManager>();
    }

    private void Move(int direction)
    {
        Data.Transform.localScale = new Vector3(direction, 1, 1);
        Data.Rigidbody2D.velocity = new Vector2(Data.MoveSpeed * direction, Data.Rigidbody2D.velocity.y);
        // Data.Animator.Play(CheckGround(jumpRadius) ? data.run.name : data.idle.name);
    }

    public override void PhysicsUpdate() => Move(_inputManager.MoveDirection);

    public override void Exit() => Data.Rigidbody2D.velocity = new Vector2(0, Data.Rigidbody2D.velocity.y);
}