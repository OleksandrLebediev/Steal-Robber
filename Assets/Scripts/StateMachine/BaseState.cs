using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected readonly IStationStateSwitcher _stateSwitcher;

    public BaseState(IStationStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void UpdateLogic() { }
}