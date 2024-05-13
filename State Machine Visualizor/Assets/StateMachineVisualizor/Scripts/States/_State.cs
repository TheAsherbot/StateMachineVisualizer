using System;
using System.Collections.Generic;

namespace TheAshBot.StateMachine
{
    public abstract class State
    {

        // public event Action OnComplete;

        protected State parent;
        protected State[] children;
        protected bool isComplete;


        public virtual void Start()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Enter()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void UpdateBranch()
        {

        }
        public virtual void FixedUpdate()
        {

        }
        public virtual void FixedUpdateBranch()
        {

        }
        public virtual void Exit()
        {

        }

        /// <summary>
        /// Gets all the needed items from the blackboard for itself, and all it children
        /// </summary>
        /// <returns>a dictionary with a string for a key, and type for the value. The type is the type of object it needs to be on the blackboard.</returns>
        public virtual Dictionary<string, Type> NeededBlackBoardItems()
        {
            Dictionary<string, Type> blackboard = new Dictionary<string, Type>();

            if (children != null)
            {
                foreach (State child in children)
                {
                    foreach (KeyValuePair<string, Type> keyValuePair in child.NeededBlackBoardItems())
                    {
                        blackboard.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }

            return blackboard;
        }
    }
}