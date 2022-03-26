public class IdelStatePlayer : CharacterState
{
    public IdelStatePlayer(Character character, CharacterStateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void UpdateLogic()
    {
        if (JoystickInput.Instance.MovementInput.magnitude > 0)
        {
            _stateMachine.SwitchState<MoveStatePlayer>();
        }
    }
}
