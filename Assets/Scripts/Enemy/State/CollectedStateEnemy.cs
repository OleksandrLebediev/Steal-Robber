using UnityEngine;

public class CollectedStateEnemy : BaseState
{
    public CollectedStateEnemy(IStationStateSwitcher stateSwitcher,
        Animator animator ,EnemyVision enemyVision) : base(stateSwitcher)
    {
        _enemyVision = enemyVision;
        _animator = animator;
    }

    private EnemyVision _enemyVision;
    private Animator _animator;

    public override void Enter()
    {
        _animator.SetBool(EnemyAnimationInfo.Falling, true);
        _enemyVision.Hide();
    }

    public override void Exit()
    {
        _animator.SetBool(EnemyAnimationInfo.Falling, false);
    }
}
