using System;
using UnityEngine;

public class Hostage : MonoBehaviour, IObjectForCollect
{
    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private AudioSource _audioSource;
    public Transform CurrentTransform => transform;
    public ObjectForCollectType Type => ObjectForCollectType.Hostage;
    public bool IsCollected { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Initialize(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    
    public void SetStateCollected()
    {
        IsCollected = true;
        _capsuleCollider.isTrigger = true;
        _animator.SetBool("falling", true);
        _audioSource.Play();
    }
    
    public void Remove()
    {
        Destroy(gameObject);
    }
}
