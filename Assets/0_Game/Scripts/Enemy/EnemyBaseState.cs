using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected readonly EnemyStateMachine _enemyStateMachine;
    public bool IsPlayerInAttackRange;
    public bool IsPlayerInNoticeRange;
    public bool IsPositionInSafeArea;

    protected EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        _enemyStateMachine = enemyStateMachine;
    }

    protected void HandlePlayerAndArea()
    {
        IsPositionInSafeArea = Vector3.Distance(_enemyStateMachine.Position, _enemyStateMachine.Center)
                < _enemyStateMachine.MaxAreaRadius;
        IsPlayerInAttackRange = GameManager.Instance.FPSController.IsPlayerAlive &&
                Vector3.Distance(_enemyStateMachine.Position, GameManager.Instance.FPSController.Position) < _enemyStateMachine.AttackRadius;
       
        IsPlayerInNoticeRange = GameManager.Instance.FPSController.IsPlayerAlive &&
               Vector3.Distance(_enemyStateMachine.Position, GameManager.Instance.FPSController.Position) <= _enemyStateMachine.PlayerDetectRadius;
    }
}
