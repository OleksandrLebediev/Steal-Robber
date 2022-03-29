using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : Character, IPlayer, ITargetPlayer
{
    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;
    public Vector3 Position => transform.position;

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
    }

}