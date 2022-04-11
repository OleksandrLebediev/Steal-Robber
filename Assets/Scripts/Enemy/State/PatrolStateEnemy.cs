using System.Collections;
using UnityEngine;

public class PatrolStateEnemy : BaseState
{
    private MonoBehaviour _mono;
    private Enemy _enemy;
    private Vector3 _target;
    private int _currentPath;

    public PatrolStateEnemy(IStationStateSwitcher stateSwitcher, Enemy enemy, MonoBehaviour mono) : base(stateSwitcher)
    {
        _enemy = enemy;
        _mono = mono;
    }

    public override void Enter()
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Move, false);
        if (_enemy.PathTargetList.Length != 0)
        {
            _currentPath = 0;
            _target = _enemy.PathTargetList[_currentPath].position;
            _enemy.Agent.speed = _enemy.MoveSpeed;
            _mono.StartCoroutine(PatrulCoroutine());
        }
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        if (_enemy.Agent.isActiveAndEnabled == true)
            _enemy.Agent.ResetPath();
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
