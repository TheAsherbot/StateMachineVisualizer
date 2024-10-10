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
            [SerializeField, HideInInspector] public bool isFoldedOut;
            /// <summary>
            /// THIS IS FOR EDITOR USE ONLY!!!!!
            /// </summary>
            [SerializeField, HideInInspector] public Branch parent;
#endif

            [SerializeField, SerializeReference]
            public State state;

            [SerializeField]
            public List<Branch> childBranches = new List<Branch>();
        }


        [SerializeField]
        public Branch rootBranch;

    }
}