using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateEnemy : EnemyState
{
    public PatrolStateEnemy(Enemy enemy, EnemyStateMachine stateMachine, MonoBehaviour mono) : base(enemy, stateMachine)
    {
        _mono = mono;
    }

    private MonoBehaviour _mono;
    private Vector3 _target;
    private int _currentPath;

    public override void Enter()
    {
        if (_enemy.PathTargetList.Length != 0)
        {
            _currentPath = 0;
            _target = _enemy.PathTargetList[_currentPath].position;
            _enemy.Agent.speed = _enemy.MoveSpeed;
            _mono.StartCoroutine(PatrulCoroutine());
        }
        _enemy.Agent.isStopped = false;
    }

    public override void Exit()
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Move, false);

        _mono.StopAllCoroutines();
        if (_enemy.Agent.isActiveAndEnabled == true)
            _enemy.Agent.isStopped = true;
    }

    private IEnumerator PatrulCoroutine()
    {
        while (true)
        {
            yield return _mono.StartCoroutine(MoveToTarget(_target));
            yield return new WaitForSeconds(2);

            _currentPath += 1;
            _currentPath = _currentPath % _enemy.PathTargetList.Length;
            _target = _enemy.PathTargetList[_currentPath].position;


            yield return _mono.StartCoroutine(Rotation(_target));
            yield return new WaitForSeconds(2);
            yield return null;
        }
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Move, true);
        Vector3 distance;

        while (true)
        {
            _enemy.Agent.SetDestination(target);
            distance = (target - _enemy.transform.position);
            distance.y = 0f;
            if (distance.magnitude < 0.1f)
            {
                _enemy.Animator.SetBool(EnemyAnimationInfo.Move, false);
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator Rotation(Vector3 target)
    {
        while (true)
        {
            Vector3 lookPos = target - _enemy.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, rotation, _enemy.RotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(_enemy.transform.rotation, rotation) <= 0.001f)
            {
                break;
            }
            yield return null;
        }

    }
}
