using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStationStateSwitcher 
{
    public BaseState CurrentState { get; private set; }
    private List<BaseState> _allStates = new List<BaseState>();

    public void Initialize()
    {
        CurrentState = _allStates[0];
        CurrentState.Enter();
    }

    public StateMachine AddState(BaseState state)
    {
        _allStates.Add(state);
        return this;
    }

    public void SwitchState<T>() where T : BaseState
    {
        CurrentState.Exit();
        var state = _allStates.FirstOrDefault(s => s is T);
        CurrentState = state;
        CurrentState.Enter();
    }
}
