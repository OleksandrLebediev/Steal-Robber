using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof (EnemyMovement))]
public class Enemy : MonoBehaviour, IObjectForCollect, IShooter
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;


    private bool _isCollected;
    private StateMachine _stateMachine;
    private EnemyVision _enemyVision;
    private CapsuleCollider _capsuleCollider;
    private AudioSource _audioSource;
    private EnemyMovement _movement;
    private Animator _animator;

    public Animator Animator => _animator;

    public NavMeshAgent Agent { get; private set; }
    public Transform CurrentTarget { get; private set; }
    public ITarget TargetPlayer { get; private set; }
    public Weapon Weapon { get; private set; }
    public EnemyVision EnemyVision => _enemyVision;
    public ObjectForCollectType Type => ObjectForCollectType.Enemy;


    public Transform CurrentTransform => transform;
    public bool IsCollected => _isCollected;
    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;

    public ITarget Target => _target;
    public IPosition TargetPosition => _targePosition;

    private ITarget _target;
    private IPosition _targePosition;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Weapon = GetComponentInChildren<Weapon>();
        _enemyVision = GetComponent<EnemyVision>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new PatrolLoopStateEnemy(_stateMachine, _movement,_animator ,this))
            .AddState(new ShootStateEnemy(_stateMachine, _animator, _movement, this, this))
            .AddState(new CollectedStateEnemy(_stateMachine, this))
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
        CurrentTarget = newTarget.CurrentTransform;
        TargetPlayer = newTarget;


        _target = newTarget;
        _targePosition = newTarget;
        _stateMachine.SwitchState<ShootStateEnemy>();    
    }

    public void SetStateCollected()
    {
        _isCollected = true;
        Agent.enabled = false;
        _capsuleCollider.isTrigger = true;
        _audioSource.Play();
        _stateMachine.SwitchState<CollectedStateEnemy>();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
