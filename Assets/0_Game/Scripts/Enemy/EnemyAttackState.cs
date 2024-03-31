using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    CancellationToken cancellationToken;
    CancellationTokenSource cancellationTokenSource;
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        cancellationTokenSource = new CancellationTokenSource();
        cancellationToken = cancellationTokenSource.Token;
        _enemyStateMachine.Agent.ResetPath();
        _enemyStateMachine.Animator.Play(StringHelper.ATTACK, -1, 0f);
         
        WaitForAnimation();
    }

    public override void Exit()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource.Dispose();
    }

    public override void Tick()
    {
        
    }
    private async void WaitForAnimation()
    {
        try
        {
            await Task.Delay(2000, cancellationToken);
            HandlePlayerAndArea();
            if (IsPlayerInAttackRange)
            {
                _enemyStateMachine.SwitchState(new EnemyAttackState(_enemyStateMachine));
                return;
            }
            if (IsPlayerInNoticeRange)
            {
                _enemyStateMachine.SwitchState(new EnemyChaseState(_enemyStateMachine));
                return;
            }
            _enemyStateMachine.SwitchState(new EnemyIdleState(_enemyStateMachine));
        }
        catch (TaskCanceledException)
        {

        }
    }
}
