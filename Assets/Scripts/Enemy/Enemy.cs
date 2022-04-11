using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IObjectForCollect
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform[] _pathTargetList;

    private bool _isCollected;
    private StateMachine _stateMachine;
    private EnemyVision _enemyVision;
    private CapsuleCollider _capsuleCollider;
    private AudioSource _audioSource;

    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Transform CurrentTarget { get; private set; }
    public ITarget TargetPlayer { get; private set; }
    public Weapon Weapon { get; private set; }
    public Transform[] PathTargetList => _pathTargetList;
    public EnemyVision EnemyVision => _enemyVision;
    public ObjectForCollectType Type => ObjectForCollectType.Enemy;


    public Transform CurrentTransform => transform;
    public bool isCollected => _isCollected;
    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public Vector3 StatrPosition => _startPosition;


    private Vector3 _startPosition;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Weapon = GetComponentInChildren<Weapon>();
        _enemyVision = GetComponent<EnemyVision>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _audioSource = GetComponent<AudioSource>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new PatrolStateEnemy(_stateMachine, this, this))
            .AddState(new ShootStateEnemy(_stateMachine, this, this))
            .AddState(new CollectedStateEnemy(_stateMachine, this))
            .AddState(new WantedStateEnemy(_stateMachine, this, this));

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
        CurrentTarget = newTarget.Position;
        TargetPlayer = newTarget;
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
