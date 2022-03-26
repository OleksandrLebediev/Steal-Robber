using System.Collections.Generic;
using System.Linq;

public class CharacterStateMachine
{
    public CharacterState CurrentState { get; private set; }
    private List<CharacterState> _allStates = new List<CharacterState>();

    public void Initialize()
    {
        CurrentState = _allStates[0];
        CurrentState.Enter();
    }

    public CharacterStateMachine AddState(CharacterState state)
    {
        _allStates.Add(state);
        return this;
    }

    public void SwitchState<T>() where T : CharacterState
    {
        CurrentState.Exit();
        var state = _allStates.FirstOrDefault(s => s is T);
        CurrentState = state;
        CurrentState.Enter();
    }
}
