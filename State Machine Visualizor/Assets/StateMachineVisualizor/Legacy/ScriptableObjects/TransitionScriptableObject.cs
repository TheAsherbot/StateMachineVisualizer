using UnityEngine;

namespace TheAshBot.StateMachine.Legacy
{
    public class TransitionScriptableObject : MonoBehaviour
    {
        public StateScriptableObject fromState;
        public StateScriptableObject toState;
    }
}
