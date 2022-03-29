using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState CurrentState { get; private set; }
    private List<EnemyState> _allStates = new List<EnemyState>();

    public void Initialize()
    {
        CurrentState = _allStates[0];
        CurrentState.Enter();
    }

    public EnemyStateMachine AddState(EnemyState state)
    {
        _allStates.Add(state);
        return this;
    }

    public void SwitchState<T>() where T : EnemyState
    {
        CurrentState.Exit();
        var state = _allStates.FirstOrDefault(s => s is T);
        CurrentState = state;
        CurrentState.Enter();
    }
}
