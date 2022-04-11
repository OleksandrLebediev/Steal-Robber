using UnityEngine;

public class MoveStatePlayer : BaseState
{
    public MoveStatePlayer(IStationStateSwitcher stateSwitcher, Player player) : base(stateSwitcher)
    {
        _player = player;
    }

    private Player _player;
    private float _gravity = -9.81f;
    private Vector3 _velocity;

    public override void Enter()
    {
        _player.Animator.SetBool(PlayerAnimationInfo.Move, true);
    }

    public override void Exit()
    {
        _player.Animator.SetBool(PlayerAnimationInfo.Move, false);
    }

    public override void UpdateLogic()
    {
        Vector3 moveVector = JoystickInput.Instance.MovementInput;

        _player.transform.rotation = Direction(moveVector, _player.MoveSpeed);
        _player.CharacterController.Move(_player.transform.forward * _player.MoveSpeed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        _player.CharacterController.Move(_velocity * Time.deltaTime);

        if (moveVector == Vector3.zero) _stateSwitcher.SwitchState<IdelStatePlayer>();
    }

    private Quaternion Direction(Vector3 moveVector, float speed)
    {
        Vector3 direct = Vector3.RotateTowards(_player.transform.forward, moveVector, speed, 0.0f);
        return Quaternion.LookRotation(direct);
    }
}
