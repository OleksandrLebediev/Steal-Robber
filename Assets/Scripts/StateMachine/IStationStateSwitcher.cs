using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStationStateSwitcher 
{
    void SwitchState<T>() where T : BaseState;
}
