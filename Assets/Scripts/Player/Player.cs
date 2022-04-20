using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, ITarget, ISender, IPlayerEvents
{
    [SerializeField] private int _health;
    [SerializeField] private HealthBarDisplay _healthBarDisplay;

    private StateMachine _stateMachine;
    private PlayerMovement _movement;
    private PlayerWallet _wallet;
    private Collector _collector;
    private PlayerAnimatorOverrider _animatorOverrider;
    private Animator _animator;
    private int _currentHealth;

    public Bag Bag { get; private set; }
    public bool IsDead { get; private set; }
    public Transform CurrentTransform => transform;
    public Vector3 VectorPosition => transform.position;
    public IAcceptingMoney Accepting => _wallet;
    public PlayerWallet Wallet => _wallet;

    public event UnityAction Dead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _animatorOverrider = GetComponent<PlayerAnimatorOverrider>();
        _collector = GetComponentInChildren<Collector>();
        _wallet = GetComponentInChildren<PlayerWallet>();
        Bag = GetComponentInChildren<Bag>();
    }

    public void Initialize()
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new IdelStatePlayer(_stateMachine))
            .AddState(new MoveStatePlayer(_stateMachine, _movement, _animator))
            .AddState(new DiedStatePlayer(_stateMachine, _animator));

        _stateMachine.Initialize();
        _animatorOverrider.Initialize(_animator, Bag);
        _currentHealth = _health;
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdateLogic();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            OnPlayerDead();
            _stateMachine.SwitchState<DiedStatePlayer>();            
        }

        _healthBarDisplay.Show();
        float _healthFill = (float)_currentHealth / (float)_health;
        _healthBarDisplay.UpdateUIBar(_healthFill);
    }

    private void OnPlayerDead()
    {
        IsDead = true;
        _collector.Deactivate();
        Dead?.Invoke();
    }
}