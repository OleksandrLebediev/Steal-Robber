using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerOverrider : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController _normalAnimations;
    [SerializeField] private AnimatorOverrideController _carryingAnimations;
    private Animator _animator;
    private Bag _bag;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
        _bag = GetComponent<Bag>();
    }

    private void OnEnable()
    {
        _bag.Filled += SetCarryingAnimations;
        _bag.Desolated += SetNormalAnimations;
    }

    private void OnDisable()
    {
        _bag.Filled -= SetCarryingAnimations;
        _bag.Desolated -= SetNormalAnimations;
    }

    private void SetNormalAnimations()
    {
        SetAnimations(_normalAnimations);
    }

    private void SetCarryingAnimations()
    {
        SetAnimations(_carryingAnimations);
    }

    public void SetAnimations(AnimatorOverrideController animatorOverrideController)
    {
        _animator.runtimeAnimatorController = animatorOverrideController;
    }
}
