using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
        _collider = GetComponent<BoxCollider>();
    }

    public void DisablePhysics()
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }
}
