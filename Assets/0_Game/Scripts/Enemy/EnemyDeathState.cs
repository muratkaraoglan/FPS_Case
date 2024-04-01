using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        float animationTime = _enemyStateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        WaitForAnimation((int)(animationTime * 1000));
        UIController.Instance.SetScoreTexts(_enemyStateMachine.Score);
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {

    }

    async void WaitForAnimation(int animationTime)
    {
        await Task.Delay(animationTime);
        EnemySpawner.Instance.RespawnEnemy(_enemyStateMachine.gameObject);
    }


}
