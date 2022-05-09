using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, ITarget, ISender, IPlayerEvents
{
    [SerializeField] private int _health;
    [SerializeField] private PlayerDisplay _display;

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

    public void Initialize(Joystick joystick)
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new IdelStatePlayer(_stateMachine, joystick))
            .AddState(new MoveStatePlayer(_stateMachine, _movement, _animator, joystick))
            .AddState(new DiedStatePlayer(_stateMachine, _animator));

        _stateMachine.Initialize();
        _display.Initialize(Bag.Capacity);
        _animatorOverrider.Initialize(_animator, Bag);
        _currentHealth = _health;
        Subscribe();
    }
    
    private void Update()
    {
        _stateMachine.CurrentState.UpdateLogic();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            OnPlayerDead();
            _stateMachine.SwitchState<DiedStatePlayer>();            
        }

        _display.ShowHealth();
        float _healthFill = (float)_currentHealth / (float)_health;
        _display.UpdateHealth(_healthFill);
    }

    private void OnPlayerDead()
    {
        IsDead = true;
        _collector.Deactivate();
        Dead?.Invoke();
    }
    
    private void OnBagChanged(int amount)
    {
        _display.UpdateBag(amount);
        _display.ShowBagCapacity();
    }
    
    private void OnBagFull()
    {
        _display.ShowBagFullNotice();
    }
    
    private void Subscribe()
    {
        Bag.Changed += OnBagChanged;
        Bag.Full += OnBagFull;
    }

    private void Unsubscribe()
    {
        Bag.Changed -= OnBagChanged;
        Bag.Full -= OnBagFull;
    }
}