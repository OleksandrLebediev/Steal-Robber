using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;

public class ShootStateEnemy : EnemyState
{
    public ShootStateEnemy(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    private float _nextTimeToFire = 0f;
    private float _fireRate = 15f;

  
    public override void Exit()
    {
     
    }

    public override void UpdateLogic()
    {
        if(Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Debug.Log("Shoot");
        }

        Vector3 lookPos = _enemy.CurrentTarget.position - _enemy.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, rotation, _enemy.RotateSpeed * Time.deltaTime);
    }
}
