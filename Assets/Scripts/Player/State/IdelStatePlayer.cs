public class IdelStatePlayer : BaseState
{
    public IdelStatePlayer(IStationStateSwitcher stateSwitcher, Joystick joystick) : base(stateSwitcher)
    {
        _joystick = joystick;
    }
    
    private Joystick _joystick;
    
    public override void UpdateLogic()
    {
        if (_joystick.MovementInput.magnitude > 0)
        {
            _stateSwitcher.SwitchState<MoveStatePlayer>();
        }
    }
}
