using System;
using System.Collections.Generic;

using Sirenix.OdinInspector;
using Sirenix.Serialization;

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
            [HideInInspector, Obsolete("THIS IS FOR EDITOR USE ONLY!!!")] public bool isFoldedOut;
            [HideInInspector, Obsolete("THIS IS FOR EDITOR USE ONLY!!!")] public Branch parent;
#endif

            [SerializeReference]
            public State state;

            public List<Branch> children = new List<Branch>();
        }


        [SerializeField] public Branch rootState;
    }
}