using UnityEngine;

public class MoveStatePlayer : BaseState
{
    public MoveStatePlayer(IStationStateSwitcher stateSwitcher, PlayerMovement movement, 
        Animator animator, Joystick joystick) : base(stateSwitcher)
    {
        _movement = movement;
        _animator = animator;
        _joystick = joystick;
    }

    private readonly PlayerMovement _movement;
    private readonly Animator _animator;
    private Joystick _joystick;

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
        Vector3 direction = _joystick.MovementInput;

        _movement.Move(direction);

        if (direction.magnitude == 0)
            _stateSwitcher.SwitchState<IdelStatePlayer>();
    }
}
