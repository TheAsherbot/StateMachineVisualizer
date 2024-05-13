using System;
using System.Collections.Generic;

using UnityEngine;


namespace TheAshBot.StateMachine
{
    public class WalkState : State
    {


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
            Dictionary<string, Type> blackboard = base.NeededBlackBoardItems();

            blackboard.Add("Transform", typeof(Component));
            blackboard.Add("Rigidbody2D", typeof(Component));

            return blackboard;
        }




    }
}
