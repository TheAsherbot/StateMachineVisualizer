#if UNITY_EDITOR
#if !ODIN_INSPECTOR
using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;


namespace TheAshBot.StateMachine.Editor
{
    
    [CustomEditor(typeof(StateMachineBlackBoard))]
    public class StateMachineBackBoardEditor : UnityEditor.Editor
    {
        private StateMachineBlackBoard blackBoard;
    
        public override void OnInspectorGUI()
        {
            blackBoard = (StateMachineBlackBoard)target;
            base.OnInspectorGUI();
    
            if (GUILayout.Button("GenerateBlackBoard"))
            {
                blackBoard.GenerateBlackBoard();
            }
        }
    
    }
}
#endif
#endif