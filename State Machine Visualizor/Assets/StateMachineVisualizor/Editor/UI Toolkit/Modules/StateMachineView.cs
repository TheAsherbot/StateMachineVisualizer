using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace TheAshBot.StateMachine.UIToolkit.Editor
{
    public class StateMachineView : GraphView
    {

        public new class UxmlFactory : UxmlFactory<StateMachineView, GraphView.UxmlTraits> { }
        public StateMachineView()
        {
            /*this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());*/
        }

    }
}