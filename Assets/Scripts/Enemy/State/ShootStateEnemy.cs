using System.Collections;
using UnityEngine;


public class ShootStateEnemy : BaseState
{
    public ShootStateEnemy(IStationStateSwitcher stateSwitcher, Animator animator,
        EnemyMovement movement, IShooter shooter, MonoBehaviour mono) : base(stateSwitcher)
    {
        _shooter = shooter;
        _movement = movement;
        _animator = animator;
        _mono = mono;
    }

    private MonoBehaviour _mono;
    private Animator _animator;
    private EnemyMovement _movement;
    private IShooter _shooter;

    private float _timeLastFired;
    private float _shootDelay = .5f;
    private float _delayBeforeFiring = 0.25f;

    public override void Enter()
    {
        _timeLastFired = 0;
        _mono.StartCoroutine(ShootCoroutine());
        _mono.StartCoroutine(_movement.RotationCoroutine(_shooter.Target.CurrentTransform));
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        _animator.SetBool(EnemyAnimationInfo.Shoot, false);
    }

    private IEnumerator ShootCoroutine()
    {
        _animator.SetBool(EnemyAnimationInfo.Shoot, true);
        yield return new WaitForSeconds(_delayBeforeFiring);

        while (true)
        {
            if ((_timeLastFired + _shootDelay) <= Time.time)
            {
                _timeLastFired = Time.time;
                _shooter.Weapon.Fire(_shooter.Target);
            }
            yield return null;
        }
    }
}
