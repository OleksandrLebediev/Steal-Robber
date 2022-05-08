using System.Collections;
using UnityEngine;

public class PatrolLoopStateEnemy : BaseState
{
    public PatrolLoopStateEnemy(IStationStateSwitcher stateSwitcher, EnemyMovement movement,
        Animator animator, MonoBehaviour mono) : base(stateSwitcher)
    {
        _movement = movement;
        _animator = animator;
        _mono = mono;
    }

    private MonoBehaviour _mono;
    private EnemyMovement _movement;
    private Animator _animator;
    private WaitForSeconds _waitForSeconds;
    private Vector3 _target;

    public override void Enter()
    {
        _animator.SetBool(EnemyAnimationInfo.Walk, false);
        _waitForSeconds = new WaitForSeconds(_movement.DelayLoop);
        if (_movement.PathTargetList.Length != 0)
        {
            _movement.SetSpeed(_movement.WalkSpeed);
            _target = _movement.GetPath();
            _mono.StartCoroutine(PatrulLoopScenarioCoroutine());
        }
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        _movement.ResetAgentPaths();
        _animator.SetBool(EnemyAnimationInfo.Walk, false);
    }

    private IEnumerator PatrulLoopScenarioCoroutine()
    {
        while (true)
        {
            _animator.SetBool(EnemyAnimationInfo.Walk, true);
            yield return _mono.StartCoroutine(_movement.MoveToTargetCoroutine(_target));
            _animator.SetBool(EnemyAnimationInfo.Walk, false);
            yield return _waitForSeconds;

            _target = _movement.GetPath();

            yield return _mono.StartCoroutine(_movement.RotationCoroutine(_target));
            yield return _waitForSeconds;
            yield return null;
        }
    }
}


