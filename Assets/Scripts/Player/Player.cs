using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Bag))]
public class Player : MonoBehaviour, ITarget, ISender
{
    [SerializeField] private int _health;
    [SerializeField] private HealthBarDisplay _healthBarDisplay;

    private StateMachine _stateMachine;
    private PlayerMovement _movement;
    private Animator _animator; 
    private int _currentHealth;

    public Bag Bag { get; private set; }
    public bool IsDead { get; private set; }
    public Transform CurrentTransform => transform;
    public Vector3 VectorPosition => transform.position;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        Bag = GetComponent<Bag>();
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new IdelStatePlayer(_stateMachine))
            .AddState(new MoveStatePlayer(_stateMachine, _movement, _animator));

        _stateMachine.Initialize();
        _currentHealth = _health;
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdateLogic();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
        {
            IsDead = true;
            GlobalEventManager.SendPlayerDead();
        }

        _healthBarDisplay.Show();
        _currentHealth -= damage;
        float _healthFill = (float)_currentHealth / (float)_health;
        _healthBarDisplay.UpdateUIBar(_healthFill);
    }
}