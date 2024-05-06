using System;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(StateMachineBlackBoard))]
public class StateMachineBackBoardEditor : Editor
{
    private StateMachineBlackBoard blackBoard;

    public override void OnInspectorGUI()
    {
        blackBoard = (StateMachineBlackBoard)target;
        base.OnInspectorGUI();

        if (GUILayout.Button("GenerateBlackBoard"))
        {
            StateMachineRunner stateMachineRunner = blackBoard.GetComponent<StateMachineRunner>();

            UpdateBackBoardFromBranch(stateMachineRunner.stateMachine.rootState);
        }
    }

    private void UpdateBackBoardFromBranch(StateMachineScriptableObject.Branch branch)
    {
        Dictionary<string, Type> objectBackBoard = branch.state.NeededBlackBoardItems();
        foreach (KeyValuePair<string, Type> item in objectBackBoard)
        {
            if (item.Value == typeof(Component))
            {
                if (blackBoard.blackBoard.componentBlackBoard.ContainsKey(item.Key)) continue;
                blackBoard.blackBoard.componentBlackBoard.Add(item.Key, default);
            }
            else if (item.Value == typeof(float))
            {
                if (blackBoard.blackBoard.floatBlackBoard.ContainsKey(item.Key)) continue;
                blackBoard.blackBoard.floatBlackBoard.Add(item.Key, 0);
            }
            else if (item.Value == typeof(int))
            {
                if (blackBoard.blackBoard.intBlackBoard.ContainsKey(item.Key)) continue;
                blackBoard.blackBoard.intBlackBoard.Add(item.Key, 0);
            }
            else if (item.Value == typeof(string))
            {
                if (blackBoard.blackBoard.stringBlackBoard.ContainsKey(item.Key)) continue;
                blackBoard.blackBoard.stringBlackBoard.Add(item.Key, "");
            }
            else if (item.Value == typeof(bool))
            {
                if (blackBoard.blackBoard.boolBlackBoard.ContainsKey(item.Key)) continue;
                blackBoard.blackBoard.boolBlackBoard.Add(item.Key, false);
            }
            else
            {
                Debug.Log("State " + branch.state.name + " has a element in the black board of type " + item.GetType() + ". The Back Board only supports the following types:\nint\nfloat\nbool\nstring\nUnityEngine.Component");
            }
        }

        try
        {
            for (int i = 0; i < branch.children.Length; i++)
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
