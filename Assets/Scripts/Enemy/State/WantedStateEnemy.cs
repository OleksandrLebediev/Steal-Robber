using System.Collections;
using UnityEngine;

public class WantedStateEnemy : BaseState
{
    public WantedStateEnemy(IStationStateSwitcher stateSwitcher, Enemy enemy, MonoBehaviour mono) : base(stateSwitcher)
    {
        _enemy = enemy;
        _mono = mono;
    }

    private Enemy _enemy;
    private MonoBehaviour _mono;
    private Vector3 _target;
    private int _countSearch = 3;

    private float _timeWay = 0;
    private float _timeDelay = 2f;

    public override void Enter()
    {

        _enemy.Animator.SetBool(EnemyAnimationInfo.Move, false);
        _enemy.Agent.speed = _enemy.MoveSpeed;
        _target = _enemy.CurrentTarget.position;
        _countSearch = 4;
        _timeWay = 0;
        _mono.StartCoroutine(WantedCoroutine());
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        if (_enemy.Agent.isActiveAndEnabled == true)
            _enemy.Agent.ResetPath();
    }

    private IEnumerator WantedCoroutine()
    {
        yield return new WaitForSeconds(1f);

        while (_countSearch > 0)
        {
            yield return _mono.StartCoroutine(MoveToTarget(_target));

            _enemy.Animator.SetBool(EnemyAnimationInfo.Move, false);

            Vector3 randomDirection = GetRandomLookTarget();
            yield return _mono.StartCoroutine(Rotation(randomDirection));

            _target = randomDirection;
            _countSearch--;

            yield return null;
        }

        _stateSwitcher.SwitchState<PatrolStateEnemy>();
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Move, true);
        Vector3 distance;

        while (true)
        {
            _enemy.Agent.SetDestination(target);
            distance = (_target - _enemy.transform.position);
            distance.y = 0f;
            if (distance.magnitude < 0.1f) yield break;

            _timeWay += Time.deltaTime;
            if (_timeWay >= _timeDelay)
            {
                _timeWay = 0;
                if (_enemy.Agent.velocity.magnitude == 0) yield break;

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

    private Vector3 GetRandomLookTarget()
    {
        Vector3 center = _enemy.transform.position;
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        return center + offset;
    }
}