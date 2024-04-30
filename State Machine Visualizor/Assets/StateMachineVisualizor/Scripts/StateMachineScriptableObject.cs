using System;

using UnityEngine;

[CreateAssetMenu(fileName = "State Machine Scriptable Object", menuName = "State Machine/State Machine Scriptable Object", order = 0)]
public class StateMachineScriptableObject : ScriptableObject
{

    [Serializable]
    public class Branch
    {
        public Branch parent;
        public Branch[] children;
    }


    public Branch rootState;

}
