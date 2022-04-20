using UnityEngine;
using UnityEngine.Events;

public class DiedStatePlayer : BaseState
{
    private Animator _animator;
    
    public DiedStatePlayer(IStationStateSwitcher stateSwitcher, Animator animator) : base(stateSwitcher)
    {
        _animator = animator;      
    }

    public override void Enter()
    {
        _animator.SetTrigger(PlayerAnimationInfo.Dying);
    }
}

