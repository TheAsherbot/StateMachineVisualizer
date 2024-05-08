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



#if ODIN_INSPECTOR
        [Button("Generate Black Board")]
#endif
        public void GenerateBlackBoard()
        {
            CreateDictionariesIfNeeded();

            StateMachineRunner stateMachineRunner = GetComponent<StateMachineRunner>();

            UpdateBackBoardFromBranch(stateMachineRunner.stateMachine.rootState);
        }

        private void CreateDictionariesIfNeeded()
        {
            CreateNewInstanceIfNeeded(ref blackBoard, () => new BlackBoard());
            CreateNewInstanceIfNeeded(ref blackBoard.componentBlackBoard, () => new Dictionary<string, Component>());
            CreateNewInstanceIfNeeded(ref blackBoard.intBlackBoard, () => new Dictionary<string, int>());
            CreateNewInstanceIfNeeded(ref blackBoard.floatBlackBoard, () => new Dictionary<string, float>());
            CreateNewInstanceIfNeeded(ref blackBoard.boolBlackBoard, () => new Dictionary<string, bool>());
            CreateNewInstanceIfNeeded(ref blackBoard.stringBlackBoard, () => new Dictionary<string, string>());


            void CreateNewInstanceIfNeeded<TClass>(ref TClass testedClass, Func<TClass> constructer)
            {
                testedClass ??= constructer();
            }
        }

        private void UpdateBackBoardFromBranch(StateMachineScriptableObject.Branch branch)
        {
            Dictionary<string, Type> objectBackBoard = branch.state.NeededBlackBoardItems();
            foreach (KeyValuePair<string, Type> item in objectBackBoard)
            {
                if (item.Value == typeof(Component))
                {
                    if (blackBoard.componentBlackBoard.ContainsKey(item.Key)) continue;
                    blackBoard.componentBlackBoard.Add(item.Key, default);
                }
                else if (item.Value == typeof(float))
                {
                    if (blackBoard.floatBlackBoard.ContainsKey(item.Key)) continue;
                    blackBoard.floatBlackBoard.Add(item.Key, 0);
                }
                else if (item.Value == typeof(int))
                {
                    if (blackBoard.intBlackBoard.ContainsKey(item.Key)) continue;
                    blackBoard.intBlackBoard.Add(item.Key, 0);
                }
                else if (item.Value == typeof(string))
                {
                    if (blackBoard.stringBlackBoard.ContainsKey(item.Key)) continue;
                    blackBoard.stringBlackBoard.Add(item.Key, "");
                }
                else if (item.Value == typeof(bool))
                {
                    if (blackBoard.boolBlackBoard.ContainsKey(item.Key)) continue;
                    blackBoard.boolBlackBoard.Add(item.Key, false);
                }
                else
                {
                    Debug.Log("State " + branch.state + " has a element in the black board of type " + item.GetType() + ". The Back Board only supports the following types:\nint\nfloat\nbool\nstring\nUnityEngine.Component");
                }
            }

            try
            {
                for (int i = 0; i < branch.children.Count; i++)
                {
                    UpdateBackBoardFromBranch(branch.children[i]);
                }
            }
            catch (OverflowException exception)
            {
                Debug.LogError(exception);
            }
        }


    }
}
