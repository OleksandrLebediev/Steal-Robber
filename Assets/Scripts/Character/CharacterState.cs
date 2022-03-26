using System.Collections;
using UnityEngine;

    public abstract class CharacterState
    {
        protected Character _character;
        protected CharacterStateMachine _stateMachine;

        public CharacterState(Character character, CharacterStateMachine stateMachine)
        {
            _character = character;
            _stateMachine = stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void UpdateLogic()
        {

        }
    }
