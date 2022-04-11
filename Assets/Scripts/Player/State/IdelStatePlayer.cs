public class IdelStatePlayer : BaseState
{
    public IdelStatePlayer(IStationStateSwitcher stateSwitcher) : base(stateSwitcher) { }

    public override void UpdateLogic()
    {
        if (JoystickInput.Instance.MovementInput.magnitude > 0)
        {
            _stateSwitcher.SwitchState<MoveStatePlayer>();
        }
    }
}
