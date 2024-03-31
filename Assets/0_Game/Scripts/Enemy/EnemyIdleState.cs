using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    int idleTime;
    CancellationTokenSource cancellationTokenSource;
    CancellationToken cancellationToken;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle State");
        _enemyStateMachine.Agent.stoppingDistance = 0;
        _enemyStateMachine.Agent.ResetPath();
        cancellationTokenSource = new CancellationTokenSource();
        cancellationToken = cancellationTokenSource.Token;

        _enemyStateMachine.Animator.Play(StringHelper.IDLE, -1, 0);
        idleTime = Mathf.CeilToInt(Random.Range(.6f, 1.2f) * 1000);
        WaitForIdleComplete();
    }

    public override void Exit()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource.Dispose();
    }

    public override void Tick()
    {
        
    }
    private async void WaitForIdleComplete()
    {
        try
        {
            await Task.Delay(idleTime, cancellationToken);
            if (IsPlayerInNoticeRange)
            {
                _enemyStateMachine.SwitchState(new EnemyChaseState(_enemyStateMachine));
                return;
            }

            _enemyStateMachine.SwitchState(new EnemyPatrolState(_enemyStateMachine));
        }
        catch (TaskCanceledException)
        {


        }
    }
}
