using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IUIAnswer
{
    public event UnityAction PressedRestartButton;
    public event UnityAction PressedNextButton;
}
