using UnityEngine;


public class PatrolStandStateEnemy : BaseState
{
    public PatrolStandStateEnemy(IStationStateSwitcher stateSwitcher, Animator animator) : base(stateSwitcher)
    {
        _animator = animator;
    }

    private Animator _animator;

    public override void Enter()
    {
        _animator.SetBool(EnemyAnimationInfo.Walk, false); 
    }
}
