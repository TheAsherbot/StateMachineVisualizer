#if !ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEditor;

using UnityEngine.UIElements;


namespace TheAshBot.StateMachine.Editor
{
    [CustomEditor(typeof(StateMachineScriptableObject))]
    public class StateMachineScriptableObjectEditor : UnityEditor.Editor
    {

        private SerializedProperty rootState;

        private List<Type> typesUnderBaseTypeList;
        private List<string> typesUnderBaseTypeNamesList;

        private event Action onFinishCurrentLevelOfChildren;
        private event Action emptyAction;


        private StateMachineScriptableObject stateMachine;

        private StateMachineScriptableObject.Branch currentRenderedBranched;


        private void OnEnable()
        {
            typesUnderBaseTypeList = FindScriptsThatInheritFrom(typeof(State), typeof(State).Assembly);

            typesUnderBaseTypeNamesList = new List<string>();

            foreach (Type type in typesUnderBaseTypeList)
            {
                typesUnderBaseTypeNamesList.Add(type.Name);
            }


            rootState = serializedObject.FindProperty("rootState");
        }

        public override VisualElement CreateInspectorGUI()
        {
            stateMachine = (StateMachineScriptableObject)target;

            VisualElement root = new VisualElement();
            root.name = "root";

            currentRenderedBranched = stateMachine.rootState;
            AddBranch(root, null);


            return root;
        }



        private List<Type> FindScriptsThatInheritFrom(Type baseScriptType, Assembly assembly)
        {
            Type[] typesInAssemblyArray = assembly.GetTypes();
            List<Type> typesUnderBaseTypeList = new List<Type>();

            foreach (Type type in typesInAssemblyArray)
            {
                if (type.IsSubclassOf(baseScriptType) || type.GetInterfaces().Any(i => i == baseScriptType))
                {
                    typesUnderBaseTypeList.Add(type);
                }
            }

            return typesUnderBaseTypeList;
        }


        private void AddBranch(VisualElement parent, StateMachineScriptableObject.Branch parentBranch)
        {
            onFinishCurrentLevelOfChildren = emptyAction;

            StateMachineScriptableObject.Branch currentBranch = currentRenderedBranched;

            currentBranch.parent = parentBranch;

            #region Fouldout
            Foldout foldout = new Foldout();
            foldout.value = currentBranch.isFoldedOut;
            foldout.RegisterValueChangedCallback((ChangeEvent<bool> callback) =>
            {
                currentBranch.isFoldedOut = callback.newValue;
            });
            #endregion

            #region stateDropdownField
            int defaultStateDropdownFieldIndex = 0;
            if (currentRenderedBranched.state != null)
            {
                for (int i = 0; i < typesUnderBaseTypeList.Count; i++)
                {
                    if (typesUnderBaseTypeList[i] == currentRenderedBranched.state.GetType())
                    {
                        defaultStateDropdownFieldIndex = i;
                        break;
                    }
                }
            }
            DropdownField stateDropdownField = new DropdownField("State", typesUnderBaseTypeNamesList, defaultStateDropdownFieldIndex);

            stateDropdownField.RegisterValueChangedCallback((ChangeEvent<string> callback) =>
            {
                int index = typesUnderBaseTypeNamesList.IndexOf(callback.newValue);

                foldout.text = typesUnderBaseTypeNamesList[index];


                ConstructorInfo[] constructorArray = typesUnderBaseTypeList[index].GetConstructors();

                // Debug.Log("Construct info for " + typesUnderBaseTypeNamesList[index] + ", Name: " + constructor.Name);

                object stateObject = constructorArray[0].Invoke(new object[] { });
                

                if (stateObject is State state)
                {
                    currentBranch.state = state;
                }

            });

            foldout.text = stateDropdownField.value;

            foldout.Add(stateDropdownField);
            #endregion

            #region addNewChildButton
            Button addNewChildButton = new Button(() => 
            {
                currentBranch.children.Add(new StateMachineScriptableObject.Branch());
                currentRenderedBranched = currentBranch.children[currentBranch.children.Count - 1];
                AddBranch(foldout, currentBranch);
            });
            addNewChildButton.text = "Add New Child";

            foldout.Add(addNewChildButton);
            #endregion

            #region Remove Self From Parent Button
            if (currentRenderedBranched != stateMachine.rootState)
            {
                Button removeSelfFromParentButton = new Button(() =>
                {
                    currentBranch.parent.children.Remove(currentBranch);
                    foldout.parent.Remove(foldout);
                });
                removeSelfFromParentButton.text = "Remove Self";

                foldout.Add(removeSelfFromParentButton);
            }
            #endregion

            parent.Add(foldout);


            // Check for children, and Get ready to update them
            for (int i = 0; i < currentRenderedBranched.children.Count; i++)
            {
                int j = i;
                onFinishCurrentLevelOfChildren += () =>
                {
                    currentRenderedBranched = currentBranch.children[j];
                    AddBranch(foldout, currentBranch);
                };
            }

            // Go to next line
            if (onFinishCurrentLevelOfChildren == null)
            {
                return;
            }
            else
            {
                onFinishCurrentLevelOfChildren.Invoke();
            }

        }
    }
}
#endif
