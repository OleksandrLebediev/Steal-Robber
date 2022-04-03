using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;


public class ShootStateEnemy : EnemyState
{
    public ShootStateEnemy(Enemy enemy, EnemyStateMachine stateMachine, MonoBehaviour mono) : base(enemy, stateMachine)
    {
        _mono = mono;
    }

    private MonoBehaviour _mono;
    private float _timeRecharge = 1;

    public override void Enter()
    {
        _mono.StartCoroutine(ShootCoroutine());
    }

    public override void Exit()
    {
        _mono.StopAllCoroutines();
        _enemy.Animator.SetBool(EnemyAnimationInfo.Shoot, false);
    }


    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            _enemy.Animator.SetBool(EnemyAnimationInfo.Shoot, true);
            yield return new WaitForSeconds(1f);
            _enemy.Animator.SetBool(EnemyAnimationInfo.Shoot, false);
            yield return new WaitForSeconds(1f);
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
