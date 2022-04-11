using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Receiver : MonoBehaviour
{
    [Header("Receiver")]
    [SerializeField] private Transform _receptionPoint;
    [SerializeField] private ReceptionZone _receptionZone;

    private ObjectForCollectType _objectForCollectType;

    public event UnityAction<ISender> ObjectAccepted;

    public virtual void Initialize(ObjectForCollectType objectForCollectType)
    {
        _objectForCollectType = objectForCollectType;
    }

    public virtual void Start()
    {
        _receptionZone.Initialize(_objectForCollectType, _receptionPoint);
    }

    public virtual void OnEnable()
    {
        _receptionZone.ObjectAccepted += OnObjectAccepted;
    }

    public virtual void OnDestroy()
    {
        _receptionZone.ObjectAccepted -= OnObjectAccepted;
    }

    private void OnObjectAccepted(ISender sender)
    {
        ObjectAccepted?.Invoke(sender);
    }
}
