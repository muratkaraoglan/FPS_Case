using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBackToCenterState : EnemyBaseState
{
    Vector3 destinationPoint;
    public EnemyBackToCenterState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        _enemyStateMachine.Agent.stoppingDistance = 0;
        _enemyStateMachine.Agent.speed = 5;
        NavMesh.SamplePosition(_enemyStateMachine.Center, out NavMeshHit hit, _enemyStateMachine.MaxAreaRadius, NavMesh.AllAreas);
        destinationPoint = hit.position;
        _enemyStateMachine.Agent.SetDestination(destinationPoint);
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        if (Vector3.Distance(_enemyStateMachine.Position, destinationPoint) < .1f)
        {
            _enemyStateMachine.SwitchState(new EnemyIdleState(_enemyStateMachine));
        }
    }
}
