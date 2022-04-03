using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Receiver : MonoBehaviour
{
    [Header("Receiver")]
    [SerializeField] private int _amountObjectsToAccept;
    [SerializeField] private ObjectForCollectType _objectForCollectType;
    [SerializeField] private Transform _receptionPoint;
    [SerializeField] private ReceptionZone _receptionZone;
    [SerializeField] private ReceptionDisplay _receptionDisplay;

    public virtual void Start()
    {
        _receptionDisplay.Initialize(_amountObjectsToAccept);
        _receptionZone.Initialize(_objectForCollectType, _receptionPoint);
    }
}
