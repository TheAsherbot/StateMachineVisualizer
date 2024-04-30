using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TheAshBot.StateMachine.UIToolkit.Editor
{
    public class StateMachine : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        [MenuItem("TheAsherBot/Visualizors/StateMachine")]
        public static void ShowExample()
        {
            StateMachine wnd = GetWindow<StateMachine>();
            wnd.titleContent = new GUIContent("StateMachine");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            VisualElement label = new Label("Hello World! From C#");
            root.Add(label);

            // Instantiate UXML
            VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
            root.Add(labelFromUXML);
        }
    }
}