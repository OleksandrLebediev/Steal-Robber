using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPS : MonoBehaviour, IItemForCollect
{
    private bool _isCollected;
    private Animator _animator;

    public Transform CurrentTransform => transform;
    public bool isCollected => _isCollected;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetStateCollected()
    {
        _isCollected = true;
        _animator.SetBool("falling", true);
    }
}
