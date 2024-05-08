using System;
using System.Collections.Generic;

using UnityEngine;


namespace TheAshBot.StateMachine
{
    public class WalkState : State
    {
        public WalkState() : base()
        {

        }

        public override void Start()
        {
            base.Start();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }


        public override Dictionary<string, Type> NeededBlackBoardItems()
        {
            return new Dictionary<string, Type>()
        {
            { "Transform", typeof(Component) },
            { "RigidBody", typeof(Component) }
        };
        }




    }
}
