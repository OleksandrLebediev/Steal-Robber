using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedStateEnemy : EnemyState
{
    public CollectedStateEnemy(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void Enter()
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Falling, true);
        _enemy.EnemyVision.Hide();
    }

    public override void Exit()
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Falling, false);
    }

}
