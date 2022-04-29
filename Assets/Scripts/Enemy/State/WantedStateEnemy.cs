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
    private bool _isReturn => _currentNumberOfSearches == _numberOfSearches - 2;

    public override void Enter()
    {
        _movement.SetSpeed(_movement.RunSpeed);
        _animator.SetBool(EnemyAnimationInfo.Run, false);
        _target = _targeter.TargetPosition.VectorPosition;
        _currentNumberOfSearches = 0;
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

        while (_currentNumberOfSearches < _numberOfSearches)
        {
            _animator.SetBool(EnemyAnimationInfo.Run, true);
            yield return _mono.StartCoroutine(_movement.MoveToTargetCoroutine(_target, _maxTimeWay));
            _animator.SetBool(EnemyAnimationInfo.Run, false);

            Vector3 direction = ChoosePath();
            yield return _mono.StartCoroutine(_movement.RotationCoroutine(direction));

            _target = direction;
            _currentNumberOfSearches++;

            yield return null;
        }
        _stateSwitcher.SwitchState<ChoicePatrolStateEnemy>();
    }

    private Vector3 ChoosePath()
    {
        if(_isReturn == true)
        {
            return _movement.GetPath();
        }
        else
        {
            return _movement.GetRandomDirection();
        }
    }
}