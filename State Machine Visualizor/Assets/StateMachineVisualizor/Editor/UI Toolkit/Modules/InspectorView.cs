using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TheAshBot.StateMachine.UIToolkit.Editor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }
        public InspectorView()
        {

        }

        private UnityEditor.Editor editor;

        public void UpdateSelection(/*NodeView nodeView*/)
        {
            /*Clear();

            Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(nodeView.node);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (editor.target)
                {
                    editor.OnInspectorGUI();
                }
            });
            Add(container);*/
        }
    }
}