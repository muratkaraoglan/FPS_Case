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
        _enemyStateMachine.Agent.speed = _enemyStateMachine.ChaseSpeed;
        _enemyStateMachine.Animator.Play(StringHelper.RUN);
        _enemyStateMachine.Agent.stoppingDistance = 1f;
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        _enemyStateMachine.Agent.SetDestination(GameManager.Instance.FPSController.Position);
        HandlePlayerAndArea();
        if (!IsPositionInSafeArea)
        {
            _enemyStateMachine.SwitchState(new EnemyBackToCenterState(_enemyStateMachine));
            return;
        }
        if (!IsPlayerInNoticeRange)
        {
            _enemyStateMachine.SwitchState(new EnemyIdleState(_enemyStateMachine));
        }
        if (IsPlayerInAttackRange)
        {
            _enemyStateMachine.SwitchState(new EnemyAttackState(_enemyStateMachine));
            return;
        }
    }
}
