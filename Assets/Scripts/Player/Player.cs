using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, ITarget, ISender
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _turningSpeed = 10;
    [SerializeField] private HealthBarDisplay _healthBarDisplay;

    public float MoveSpeed => _moveSpeed;
    public float TurningSpeed => _turningSpeed;
    private StateMachine _stateMachine;

    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;
    public Rigidbody Rigidbody;
    public Animator Animator { get; private set; }
    public Bag Bag { get; private set; }
    public Transform Position => transform;
    public bool IsDead => _isDead;

    private int _currentHealth;
    private bool _isDead;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        Bag = GetComponent<Bag>();
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new IdelStatePlayer(_stateMachine))
            .AddState(new MoveStatePlayer(_stateMachine, this));

        _stateMachine.Initialize();
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
        _stateMachine.CurrentState.UpdateLogic();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
        {
            _isDead = true;
            GlobalEventManager.SendPlayerDead();
        }

        _healthBarDisplay.Show();
        _currentHealth -= damage;
        float _healthFill = (float)_currentHealth / (float)_maxHealth;
        _healthBarDisplay.UpdateUIBar(_healthFill);
    }

}