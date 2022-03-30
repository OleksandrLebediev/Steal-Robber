using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceСar : MonoBehaviour, IRecipient
{
    public Transform ReceiptPlase => transform;
    public ObjectForCollect ObjectForCollectType => ObjectForCollect.Enemy;
}
