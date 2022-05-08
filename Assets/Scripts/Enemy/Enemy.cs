using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof (EnemyMovement))]
public class Enemy : MonoBehaviour, IObjectForCollect, IShooter
{
    private StateMachine _stateMachine;
    private EnemyVision _enemyVision;
    private EnemiesAudioSourse _enemiesAudioSourse;
    private EnemyMovement _movement;
    private Animator _animator;
    private bool _isCollected;
    private ITarget _target;
    private IPosition _targetPosition;

    public ObjectForCollectType Type => ObjectForCollectType.Enemy;
    public Transform CurrentTransform => transform;
    public Weapon Weapon { get; private set; }
    public ITarget Target => _target;
    public IPosition TargetPosition => _targetPosition;
    public bool IsCollected => _isCollected;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyVision = GetComponent<EnemyVision>();
        _movement = GetComponent<EnemyMovement>();
        Weapon = GetComponentInChildren<Weapon>();
    }

    public void Initialize(EnemiesAudioSourse enemiesAudioSourse)
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new ChoicePatrolStateEnemy(_stateMachine, _movement))
            .AddState(new PatrolStandStateEnemy(_stateMachine, _animator))
            .AddState(new PatrolTurnStateEnemy(_stateMachine, _animator, _movement, this))
            .AddState(new PatrolLoopStateEnemy(_stateMachine, _movement, _animator, this))
            .AddState(new ShootStateEnemy(_stateMachine, _animator, _movement, this, this))
            .AddState(new CollectedStateEnemy(_stateMachine, _animator, _enemyVision))
            .AddState(new WantedStateEnemy(_stateMachine, _movement, _animator, this, this));

        _enemiesAudioSourse = enemiesAudioSourse;
        _stateMachine.Initialize();
        _movement.Initialize();
        Weapon.Initialize(_enemiesAudioSourse);
    }
    
    private void OnEnable()
    {
        _enemyVision.PlayerDetected += OnDetectedPlayer;
        _enemyVision.PlayerLost += OnLostPlayer;
    }

    private void OnDestroy()
    {
        _enemyVision.PlayerDetected -= OnDetectedPlayer;
        _enemyVision.PlayerLost -= OnLostPlayer;
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdateLogic();
    }

    private void OnLostPlayer()
    {
        _stateMachine.SwitchState<WantedStateEnemy>();
    }

    private void OnDetectedPlayer(ITarget newTarget)
    {
        _target = newTarget;
        _targetPosition = newTarget;
        _stateMachine.SwitchState<ShootStateEnemy>();    
    }

    public void SetStateCollected()
    {
        _isCollected = true;
        _movement.DestroyAgent();
        _enemiesAudioSourse.PlayCollectedAudio();
        _stateMachine.SwitchState<CollectedStateEnemy>();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
