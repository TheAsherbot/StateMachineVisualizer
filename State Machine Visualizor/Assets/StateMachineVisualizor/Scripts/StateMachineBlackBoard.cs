using System;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;


namespace TheAshBot.StateMachine
{
    public class StateMachineBlackBoard : SerializedMonoBehaviour
    {



        [SerializeField]
        public BlackBoard blackBoard;


#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button("Generate Black Board")]
#endif
#endif
        public void GenerateBlackBoard()
        {
            CreateDictionariesIfNeeded();

            StateMachineRunner stateMachineRunner = GetComponent<StateMachineRunner>();

            UpdateBackBoardFromBranch(stateMachineRunner.stateMachine.rootBranch);
        }

        private void CreateDictionariesIfNeeded()
        {
            blackBoard ??= new BlackBoard();
            blackBoard.componentBlackBoard ??= new Dictionary<string, Component>();
            blackBoard.intBlackBoard ??= new Dictionary<string, int>();
            blackBoard.floatBlackBoard ??= new Dictionary<string, float>();
            blackBoard.boolBlackBoard ??= new Dictionary<string, bool>();
            blackBoard.stringBlackBoard ??= new Dictionary<string, string>();
        }

        private void UpdateBackBoardFromBranch(StateMachineScriptableObject.Branch branch)
        {
            Dictionary<string, Type> objectBackBoard = branch.state.NeededBlackBoardItems();

            // Adding Each item to the correct blackboard
            foreach (KeyValuePair<string, Type> item in objectBackBoard)
            {
                if (item.Value == typeof(Component))
                {
                    if (blackBoard.componentBlackBoard.ContainsKey(item.Key))
                    {
                        continue;
                    }
                    blackBoard.componentBlackBoard.Add(item.Key, default);
                }
                else if (item.Value == typeof(float))
                {
                    if (blackBoard.floatBlackBoard.ContainsKey(item.Key))
                    {
                        continue;
                    }
                    blackBoard.floatBlackBoard.Add(item.Key, 0);
                }
                else if (item.Value == typeof(int))
                {
                    if (blackBoard.intBlackBoard.ContainsKey(item.Key))
                    {
                        continue;
                    }
                    blackBoard.intBlackBoard.Add(item.Key, 0);
                }
                else if (item.Value == typeof(string))
                {
                    if (blackBoard.stringBlackBoard.ContainsKey(item.Key))
                    {
                        continue;
                    }
                    blackBoard.stringBlackBoard.Add(item.Key, "");
                }
                else if (item.Value == typeof(bool))
                {
                    if (blackBoard.boolBlackBoard.ContainsKey(item.Key))
                    {
                        continue;
                    }
                    blackBoard.boolBlackBoard.Add(item.Key, false);
                }
                else
                {
                    Debug.Log("State " + branch.state + " has a element in the black board of type " + item.GetType() + ". The Back Board only supports the following types:\nint\nfloat\nbool\nstring\nUnityEngine.Component");
                }
            }
        }
    }
}
