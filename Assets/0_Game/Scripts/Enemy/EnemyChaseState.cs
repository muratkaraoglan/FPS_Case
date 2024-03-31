using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Chase State");
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        
    }
}
