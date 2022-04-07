using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public static Action PlayerDead;

    public static void SendPlayerDead()
    {
        PlayerDead?.Invoke();
    }
}
