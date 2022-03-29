using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected Enemy _enemy;
    protected EnemyStateMachine _stateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void UpdateLogic()
    {

    }
}
