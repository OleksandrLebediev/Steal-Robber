using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Receiver : MonoBehaviour
{
    [Header("Receiver")]
    [SerializeField] private Transform _receptionPoint;
    [SerializeField] private ReceptionZone _receptionZone;

    private ObjectForCollectType _objectForCollectType;

    public event UnityAction<ISender> ObjectAccepted;

    public virtual void Initialize(ObjectForCollectType objectForCollectType, AudioSource audioSource)
    {
        _objectForCollectType = objectForCollectType;
        _receptionZone.Initialize(_objectForCollectType, _receptionPoint, audioSource);

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
