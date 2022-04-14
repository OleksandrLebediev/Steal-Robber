using System.Collections;
using UnityEngine;

public class WantedStateEnemy : BaseState
{
    public WantedStateEnemy(IStationStateSwitcher stateSwitcher, EnemyMovement movement,
        Animator animator, ITargeter targeter, MonoBehaviour mono) : base(stateSwitcher)
    {
        _mono = mono;
        _movement = movement;
        _animator = animator;
        _targeter = targeter;
    }

    private EnemyMovement _movement;
    private MonoBehaviour _mono;
    private Animator _animator;
    private ITargeter _targeter;

    private readonly int _numberOfSearches = 3;
    private readonly float _maxTimeWay = 2f;
    private readonly float _delayBeforeWanted = 1f;
    private Vector3 _target;
    private int _currentNumberOfSearches;

    public override void Enter()
    {
        _movement.Initialize(MovementType.Run);
        _animator.SetBool(EnemyAnimationInfo.Run, false);
        _target = _targeter.TargetPosition.VectorPosition;
        _currentNumberOfSearches = _numberOfSearches;
        _mono.StartCoroutine(WantedCoroutine());
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        _movement.ResetAgentPaths();
    }

    private IEnumerator WantedCoroutine()
    {
        yield return new WaitForSeconds(_delayBeforeWanted);

        while (_currentNumberOfSearches > 0)
        {
            _animator.SetBool(EnemyAnimationInfo.Run, true);
            yield return _mono.StartCoroutine(_movement.MoveToTargetCoroutine(_target, _maxTimeWay));
            _animator.SetBool(EnemyAnimationInfo.Run, false);

            Vector3 randomDirection = _movement.GetRandomDirection();
            yield return _mono.StartCoroutine(_movement.RotationCoroutine(randomDirection));

            _target = randomDirection;
            _currentNumberOfSearches--;

            yield return null;
        }
        _stateSwitcher.SwitchState<PatrolLoopStateEnemy>();
    }
}