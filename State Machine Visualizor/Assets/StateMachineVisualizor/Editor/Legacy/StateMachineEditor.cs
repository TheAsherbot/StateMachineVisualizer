using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

using UnityEditor;
using UnityEditor.Callbacks;

using UnityEngine;

namespace TheAshBot.StateMachine.Legacy.Editor
{
    public class StateMachineEditor : EditorWindow
    {

        [MenuItem("TheAsherBot/Visualizors/Legacy/StateMachine")]
        public static void OpenWindow()
        {
            EditorWindow editorWIndow = GetWindow<StateMachineEditor>();
            editorWIndow.minSize = new Vector2(300, 200);
        }

        [OnOpenAsset]
        private static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject is StateMachineScriptableObject)
            {
                OpenWindow();
                return true;
            }
            return false;
        }




        private float BaseFontSize
        {
            get
            {
                return position.height < position.width ? position.height : position.width;
            }
        }
        private Color NodeColor
        {
            get
            {
                return EditorGUIUtility.isProSkin ? Color.red : Color.HSVToRGB(0, 0, 0.6f);
            }
        }
        private GUIStyle labelStyle;
        private GUIStyle LabelStyle
        {
            get
            {
                if (labelStyle == null)
                {
                    labelStyle = CopyGUISyle(EditorStyles.label);
                    labelStyle.fontStyle = FontStyle.Bold;
                    labelStyle.wordWrap = true;
                }

                labelStyle.fontSize = Mathf.RoundToInt(BaseFontSize / 20);
                return labelStyle;
            }
        }

        private StateMachineScriptableObject stateMachine;


        private void OnGUI()
        {
            if (stateMachine == null)
            {
                EditorGUILayout.LabelField("Please Select a StateMachineScriptableObject to use this window", LabelStyle);
                return;
            }


            // Creates the Context Menu
            GenericMenu menu = new GenericMenu();

            // Adds a item to the menu
            menu.AddItem(new GUIContent("TEST!"), false, () =>
            {

            });

            // Test to see if the mouse button is down.
            if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
            {
                Debug.Log("Right Button Down!");
                menu.ShowAsContext();
            }

            int x = 0;
            for (int i = 0; i < 5; i++)
            {
                EditorGUI.DrawRect(new Rect(x, 10, 50, 50), NodeColor);
                x += 60;
            }
        }

        private void OnSelectionChange()
        {
            if (Selection.activeObject is StateMachineScriptableObject)
            {
                Init();
            }
            else
            {
                stateMachine = null;
            }
        }

        private void Init()
        {
            if (Selection.activeObject is StateMachineScriptableObject stateMachine)
            {
                this.stateMachine = stateMachine;
            }
            else
            {
                stateMachine = null;
            }
        }

        private GUIStyle CopyGUISyle(GUIStyle copyedStyle)
        {
            GUIStyle style = new GUIStyle();
            style.active = copyedStyle.active;
            style.alignment = copyedStyle.alignment;
            style.border = copyedStyle.border;
            style.clipping = copyedStyle.clipping;
            style.contentOffset = copyedStyle.contentOffset;
            style.fixedHeight = copyedStyle.fixedHeight;
            style.fixedWidth = copyedStyle.fixedWidth;
            style.focused = copyedStyle.focused;
            style.font = copyedStyle.font;
            style.fontSize = copyedStyle.fontSize;
            style.fontStyle = copyedStyle.fontStyle;
            style.hover = copyedStyle.hover;
            style.imagePosition = copyedStyle.imagePosition;
            style.margin = copyedStyle.margin;
            style.name = copyedStyle.name;
            style.normal = copyedStyle.normal;
            style.onActive = copyedStyle.onActive;
            style.onFocused = copyedStyle.onFocused;
            style.onHover = copyedStyle.onHover;
            style.onNormal = copyedStyle.onNormal;
            style.overflow = copyedStyle.overflow;
            style.padding = copyedStyle.padding;
            style.richText = copyedStyle.richText;
            style.stretchHeight = copyedStyle.stretchHeight;
            style.stretchWidth = copyedStyle.stretchWidth;
            style.wordWrap = copyedStyle.wordWrap;

            return style;
        }

    }
}
