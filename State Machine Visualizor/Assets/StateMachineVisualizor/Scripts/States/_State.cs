using System;
using System.Collections.Generic;

namespace TheAshBot.StateMachine
{
    public abstract class State
    {

        public State()
        {

        }

        public event Action OnComplete;

        protected State parent;
        protected State[] children;
        protected bool isComplete;


        public virtual void Start()
        {

        }
        public virtual void Enter()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void FixedUpdate()
        {

        }
        public virtual void Exit()
        {

        }

        public virtual Dictionary<string, Type> NeededBlackBoardItems()
        {
            return default;
        }
    }
}