﻿using System.Collections;
using UnityEngine;

public class PatrolTurnStateEnemy : BaseState
{
    public PatrolTurnStateEnemy(IStationStateSwitcher stateSwitcher, Animator animator,
        EnemyMovement movement ,MonoBehaviour mono) : base(stateSwitcher)
    {
        _animator = animator;
        _movement = movement;
        _mono = mono;
    }

    private Animator _animator;
    private EnemyMovement _movement;
    private MonoBehaviour _mono;
    private WaitForSeconds _waitForSeconds;

    public override void Enter()
    {
        _mono.StartCoroutine(PatrulTurnScenarioCoroutine());
        _waitForSeconds = new WaitForSeconds(_movement.DelayTurn);
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        _animator.SetBool(EnemyAnimationInfo.Turn, false);
    }

    private IEnumerator PatrulTurnScenarioCoroutine()
    {
        while (true)
        {
            _animator.SetBool(EnemyAnimationInfo.Turn, true);
            Quaternion rotation = Quaternion.Euler(0, _movement.transform.eulerAngles.y + 180, 0);
             yield return _mono.StartCoroutine(_movement.RotationAround(rotation));
            _animator.SetBool(EnemyAnimationInfo.Turn, false);
            yield return _waitForSeconds;
        }
    }
}
