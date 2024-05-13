using System;
using System.Collections.Generic;

using UnityEngine;

namespace TheAshBot.StateMachine
{
    [CreateAssetMenu(fileName = "State Machine Scriptable Object", menuName = "State Machine/State Machine Scriptable Object", order = 0)]
    public class StateMachineScriptableObject : ScriptableObject
    {

        [Serializable]
        public class Branch
        {
#if UNITY_EDITOR
            // For Use of the custom Editor.
            /// <summary>
            /// THIS IS FOR EDITOR USE ONLY!!!!!
            /// </summary>
            [HideInInspector] public bool isFoldedOut;
            /// <summary>
            /// THIS IS FOR EDITOR USE ONLY!!!!!
            /// </summary>
            [HideInInspector] public Branch parent;
#endif

            // [SerializeReference]
            public State state;

            public int _int = 0;

            public List<Branch> childBranches = new List<Branch>();
        }


        public Branch rootBranch;

        public RefBranch refBranch;

        [Serializable]
        public struct RefBranch
        {
            public int _int;
        }
    }
}