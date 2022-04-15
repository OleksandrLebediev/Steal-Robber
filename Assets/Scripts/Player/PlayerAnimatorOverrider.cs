using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorOverrider : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController _normalAnimations;
    [SerializeField] private AnimatorOverrideController _carryingAnimations;

    private Animator _animator;
    private Bag _bag;

    public void Initialize(Animator animator, Bag bag)
    {
        _animator = animator;
        _bag = bag;

        _bag.Filled += SetCarryingAnimations;
        _bag.Desolated += SetNormalAnimations;
    }

    private void OnDestroy()
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
