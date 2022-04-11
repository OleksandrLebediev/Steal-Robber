using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class Player : Character, IPlayer, ITarget, ISender
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthBarDisplay _healthBarDisplay;
    public AudioSource _audioSource;

    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;
    public Transform Position => transform;
    public bool IsDead => _isDead;

    private int _currentHealth;
    private bool _isDead;

    //public event UnityAction Dead;

    protected override void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        base.Awake();
    }

    private void Start()
    {
        _characterStateMachine = new CharacterStateMachine();
        _characterStateMachine
            .AddState(new IdelStatePlayer(this, _characterStateMachine))
            .AddState(new MoveStatePlayer(this, _characterStateMachine, this));
        
        _characterStateMachine.Initialize();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
        {
            _isDead = true;
            GlobalEventManager.SendPlayerDead();
            //Dead?.Invoke();
        }

        _healthBarDisplay.Show();
        _currentHealth -= damage;
        float _healthFill = (float)_currentHealth / (float)_maxHealth;
        _healthBarDisplay.UpdateUIBar(_healthFill);
    }    

}