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
    private AudioSource _audioSource;
    private EnemyMovement _movement;
    private Animator _animator;
    private bool _isCollected;
    private ITarget _target;
    private IPosition _targePosition;

    public ObjectForCollectType Type => ObjectForCollectType.Enemy;
    public Transform CurrentTransform => transform;
    public Weapon Weapon { get; private set; }
    public ITarget Target => _target;
    public IPosition TargetPosition => _targePosition;
    public bool IsCollected => _isCollected;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyVision = GetComponent<EnemyVision>();
        _audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<EnemyMovement>();
        Weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new PatrolLoopStateEnemy(_stateMachine, _movement,_animator ,this))
            .AddState(new ShootStateEnemy(_stateMachine, _animator, _movement, this, this))
            .AddState(new CollectedStateEnemy(_stateMachine, _animator, _enemyVision))
            .AddState(new WantedStateEnemy(_stateMachine, _movement ,_animator , this, this));

        _stateMachine.Initialize();
        Weapon.Initialize(_audioSource);
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
        _targePosition = newTarget;
        _stateMachine.SwitchState<ShootStateEnemy>();    
    }

    public void SetStateCollected()
    {
        _isCollected = true;
        _movement.DestroyAgent();
        _audioSource.Play();
        _stateMachine.SwitchState<CollectedStateEnemy>();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
