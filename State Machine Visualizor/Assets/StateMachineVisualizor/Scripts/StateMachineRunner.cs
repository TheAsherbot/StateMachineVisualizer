using UnityEngine;


namespace TheAshBot.StateMachine
{
    [RequireComponent(typeof(StateMachineBlackBoard))]
    public class StateMachineRunner : MonoBehaviour
    {


        public StateMachineScriptableObject stateMachine;

        private State rootState;

        private bool hasStarted;


        private void Awake()
        {
            Debug.Log("stateMachine: " + stateMachine);
            Debug.Log("stateMachine.rootBranch: " + stateMachine.rootBranch);
            Debug.Log("stateMachine.rootBranch.state: " + stateMachine.rootBranch.state);
            Debug.Log("rootState : " + rootState);
            rootState = stateMachine.rootBranch.state;
        }

        private void Start()
        {
            hasStarted = true;
            rootState.Start();
        }

        private void OnEnable()
        {
            if (hasStarted)
            {
                rootState.Start();
            }
        }

        private void Update()
        {
            rootState.UpdateBranch();
        }

        private void FixedUpdate()
        {
            rootState.FixedUpdateBranch();
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            rootState.Exit();
        }




    }
}
