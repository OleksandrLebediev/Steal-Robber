using UnityEngine;

public class MoveStatePlayer : BaseState
{
    public MoveStatePlayer(IStationStateSwitcher stateSwitcher, PlayerMovement movement, Animator animator) : base(stateSwitcher)
    {
        _movement = movement;
        _animator = animator;
    }

    private PlayerMovement _movement;
    private Animator _animator;

    public override void Enter()
    {
        _animator.SetBool(PlayerAnimationInfo.Move, true);
    }

    public override void Exit()
    {
        _animator.SetBool(PlayerAnimationInfo.Move, false);
    }

    public override void UpdateLogic()
    {
        Vector3 direction = JoystickInput.Instance.MovementInput;

        _movement.Move(direction);

        if (direction.magnitude == 0)
            _stateSwitcher.SwitchState<IdelStatePlayer>();
    }
}
