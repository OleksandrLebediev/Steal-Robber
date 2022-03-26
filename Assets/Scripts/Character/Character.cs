using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public readonly float _moveSpeed = 2;
    public readonly float _turningSpeed = 10;

    public Animator Animator { get; private set; }
    public Bag Bag { get; private set; }
    public List<Character> Enemies { get; private set; }

    public readonly string MoveAnimation = "move";

    protected CharacterStateMachine _characterStateMachine;
    protected List<CharacterState> _allStates;

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Bag = GetComponent<Bag>();
    }


    protected virtual void Update()
    {
        _characterStateMachine.CurrentState.UpdateLogic();
    }
}
