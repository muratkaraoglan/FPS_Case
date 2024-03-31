using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    Vector3 destinationPoint;
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {

        _enemyStateMachine.Agent.speed = _enemyStateMachine.MoveSpeed;
        NavMesh.SamplePosition(_enemyStateMachine.GetRandomPointInSafeArae(), out NavMeshHit hit, _enemyStateMachine.MaxAreaRadius, NavMesh.AllAreas);
        _enemyStateMachine.Animator.Play(StringHelper.WALK, -1, 0);
        destinationPoint = hit.position;
        _enemyStateMachine.Agent.SetDestination(destinationPoint);
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        HandlePlayerAndArea();
        if (IsPlayerInNoticeRange)
        {
            //switch to notice state
            _enemyStateMachine.SwitchState(new EnemyChaseState(_enemyStateMachine));
            return;
        }
        if (Vector3.Distance(_enemyStateMachine.Position, destinationPoint) < .1f)
        {
            _enemyStateMachine.SwitchState(new EnemyIdleState(_enemyStateMachine));
        }
    }
}
