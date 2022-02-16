using System;
using System.Collections;
using System.Collections.Generic;
using Actors.Boss;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossRun : StateMachineBehaviour
{
    public Boss Boss;
    public Rigidbody2D RigidBody;
    public Transform PlayerTransform;
    public Transform Transform;
    public float AttackDistance;
    public float Speed;
    private int _nextState;
    private int _statesCount = Enum.GetValues(typeof(BossStates)).Length;
    private static readonly int State = Animator.StringToHash("State");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _nextState = Random.Range(0, _statesCount-1);
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((BossStates) _nextState == BossStates.Idle) animator.SetInteger(State, _nextState);
        var direction = new Vector2(PlayerTransform.position.x - RigidBody.position.x, 0).normalized;
        var scale = Transform.localScale;
        Transform.localScale = new Vector3(direction.x>0?-1:1, scale.y, scale.z);
        
        RigidBody.position = Vector2.MoveTowards(RigidBody.position,
            RigidBody.position + direction, Speed*Time.deltaTime);
        
        if (Vector2.Distance(PlayerTransform.position, RigidBody.position) <= AttackDistance)
        {
            animator.SetInteger(State, _nextState);
        }
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(State, (int)BossStates.Empty);
    }
}
