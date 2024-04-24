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
        var directionToPlayer = GameManager.Instance.FPSController.Position - _enemyStateMachine.Position;
        var angleToPlayer = Vector3.Angle(directionToPlayer, _enemyStateMachine.transform.forward);



        IsPositionInSafeArea = Vector3.Distance(_enemyStateMachine.Position, _enemyStateMachine.Center)
                < _enemyStateMachine.MaxAreaRadius;

        IsPlayerInNoticeRange = angleToPlayer < _enemyStateMachine.PlayerDetectionAngle / 2f
            && directionToPlayer.magnitude < _enemyStateMachine.PlayerDetectRadius;
        //IsPlayerInAttackRange = GameManager.Instance.FPSController.IsPlayerAlive &&
        //        Vector3.Distance(_enemyStateMachine.Position, GameManager.Instance.FPSController.Position)
        //        < _enemyStateMachine.AttackRadius;

        //IsPlayerInNoticeRange = GameManager.Instance.FPSController.IsPlayerAlive &&
        //       Vector3.Distance(_enemyStateMachine.Position, GameManager.Instance.FPSController.Position)
        //       <= _enemyStateMachine.PlayerDetectRadius;
    }
}
