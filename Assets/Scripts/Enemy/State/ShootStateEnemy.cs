using System.Collections;
using UnityEngine;


public class ShootStateEnemy : EnemyState
{
    public ShootStateEnemy(Enemy enemy, EnemyStateMachine stateMachine, MonoBehaviour mono) : base(enemy, stateMachine)
    {
        _mono = mono;
    }

    private MonoBehaviour _mono;
    private float timeLastFired;
    private float shotDelay = .5f;

    public override void Enter()
    {
        timeLastFired = 0;
        _mono.StartCoroutine(ShootCoroutine());
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        _enemy.Animator.SetBool(EnemyAnimationInfo.Shoot, false);
    }


    private IEnumerator ShootCoroutine()
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Shoot, true);
        yield return new WaitForSeconds(0.25f);

        while (true)
        {

            if ((timeLastFired + shotDelay) <= Time.time)
            {
                timeLastFired = Time.time;
                _enemy.Weapon.Fire();
                _enemy.TargetPlayer.TakeDamage(10);
            }
            yield return null;
        }
    }

    public override void UpdateLogic()
    {
        Vector3 lookPos = _enemy.CurrentTarget.position - _enemy.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, rotation, _enemy.RotateSpeed * Time.deltaTime);
    }
}
