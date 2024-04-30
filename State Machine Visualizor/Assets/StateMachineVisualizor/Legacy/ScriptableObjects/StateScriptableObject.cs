using System.Collections.Generic;

using UnityEngine;

namespace TheAshBot.StateMachine.Legacy
{
    public class StateScriptableObject : ScriptableObject
    {

        public List<TransitionScriptableObject> outputTransitions;
        public List<TransitionScriptableObject> inputTransitions;

    }
}