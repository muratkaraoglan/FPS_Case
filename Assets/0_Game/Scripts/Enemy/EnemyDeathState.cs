using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        _enemyStateMachine.Agent.ResetPath();
        _enemyStateMachine.Animator.Play(StringHelper.DEATH, -1, 0f);
        _enemyStateMachine.CloseCanvas();
        //score +1
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        
    }
}
