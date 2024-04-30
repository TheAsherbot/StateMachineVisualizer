using System.Collections.Generic;

using UnityEngine;

namespace TheAshBot.StateMachine.Legacy
{
    [CreateAssetMenu(fileName = "StateMachine", menuName = "TheAshBot/StateMachine", order = 0)]
    public class StateMachineScriptableObject : ScriptableObject
    {

        public List<StateScriptableObject> states;


        public void Add(StateScriptableObject state)
        {
            states.Add(state);
        }

    }
}