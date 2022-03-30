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
    private EnemyStateMachine _stateMachine;
    private EnemyVision _enemyVision;

    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Transform CurrentTarget { get; private set; }
    public Transform[] PathTargetList => _pathTargetList;
    public EnemyVision EnemyVision => _enemyVision;
    public ObjectForCollect Type => ObjectForCollect.Enemy;


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
        _enemyVision = GetComponent<EnemyVision>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _stateMachine = new EnemyStateMachine();
        _stateMachine
            .AddState(new PatrolStateEnemy(this, _stateMachine, this))
            .AddState(new WaitStateEnemy(this, _stateMachine))
            .AddState(new ShootStateEnemy(this, _stateMachine))
            .AddState(new CollectedStateEnemy(this, _stateMachine))
            .AddState(new WantedStateEnemy(this, _stateMachine, this));


        _stateMachine.Initialize();
    }

    private void OnEnable()
    {
        _enemyVision.PlayerDetected += OnDetectedPlayer;
        _enemyVision.PlayerLost += OnLostPlayer;
    }

    private void OnLostPlayer()
    {
        _stateMachine.SwitchState<WantedStateEnemy>();
    }

    private void OnDetectedPlayer(Transform newTarget)
    {
        CurrentTarget = newTarget;
        _stateMachine.SwitchState<ShootStateEnemy>();
    }

    public void SetStateCollected()
    {
        _isCollected = true;
        Agent.enabled = false;
        _stateMachine.SwitchState<CollectedStateEnemy>();

    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdateLogic();
    }
}
