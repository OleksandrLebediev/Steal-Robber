using UnityEngine;

public class MoveStatePlayer : CharacterState
{
    private IPlayer _player;
    private float _gravity = -9.81f;
    private Vector3 _velocity;

    public MoveStatePlayer(Character character, CharacterStateMachine stateMachine, IPlayer player) : base(character, stateMachine)
    {
        _player = player;
    }

    public override void Enter()
    {
        _character.Animator.SetBool(_character.MoveAnimation, true);
    }

    public override void Exit()
    {
        _character.Animator.SetBool(_character.MoveAnimation, false);
    }

    public override void UpdateLogic()
    {
        Vector3 moveVector = JoystickInput.Instance.MovementInput;

        _character.transform.rotation = Direction(moveVector, _character._moveSpeed);
        _player.CharacterController.Move(_character.transform.forward * _character._moveSpeed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        _player.CharacterController.Move(_velocity * Time.deltaTime);

        if (moveVector == Vector3.zero) _stateMachine.SwitchState<IdelStatePlayer>();
    }

    private Quaternion Direction(Vector3 moveVector, float speed)
    {
        Vector3 direct = Vector3.RotateTowards(_character.transform.forward, moveVector, speed, 0.0f);
        return Quaternion.LookRotation(direct);
    }
}
