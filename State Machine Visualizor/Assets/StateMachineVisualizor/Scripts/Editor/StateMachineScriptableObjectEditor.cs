#if UNITY_EDITOR
// #if !ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;


namespace TheAshBot.StateMachine.Editor
{
    [UnityEditor.CustomEditor(typeof(StateMachineScriptableObject))]
    public class StateMachineScriptableObjectEditor : UnityEditor.Editor
    {

        /// <summary>
        /// Holds a list of all the types of all the scripts that inherited from State
        /// </summary>
        private List<Type> typesInheritedFromSateList;
        /// <summary>
        /// holds a list of all the names of all the scripts that inherited from State
        /// </summary>
        private List<string> typesInheritedFromStateNamesList;

        
        /// <summary>
        /// Is a event that is called when the current branch is done being drawn;
        /// </summary>
        private event Action OnChildrenReadyToDraw;
        /// <summary>
        /// Is used to set other actions to be empty
        /// </summary>
        private event Action EmptyAction;


        /// <summary>
        ///  Is the state machine that is being shown in the inspector
        /// </summary>
        private StateMachineScriptableObject stateMachine;
        

        /// <summary>
        /// Is the branch that was most recently being drawn onto the inspector
        /// </summary>
        // private StateMachineScriptableObject.Branch currentBranchBeingDrawn;


        private void OnEnable()
        {
            typesInheritedFromSateList = FindScriptsThatInheritFrom(typeof(State), typeof(State).Assembly);

            typesInheritedFromStateNamesList = new List<string>();

            foreach (Type type in typesInheritedFromSateList)
            {
                typesInheritedFromStateNamesList.Add(type.Name);
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            stateMachine = (StateMachineScriptableObject)target;

            VisualElement root = new VisualElement();
            root.name = "root";

            
            SerializedProperty listProperty = serializedObject.FindProperty("rootBranch");

            // Get the SerializedProperty for the individual elements in the list
            SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(0);


            // currentBranchBeingDrawn = stateMachine.rootBranch;
            AddUIForBranch(root, new int[0]);
            // AddUIForBranch(root, null);

            // Debug.Log("stateMachine.rootBranch.state: " + currentBranchBeingDrawn.state);
            return root;
        }


        /// <summary>
        /// Use reflection to find all of the scripts that inherit from another script
        /// </summary>
        /// <param name="baseScriptType">is the type of the parentVisualElement script</param>
        /// <param name="assembly">is the assembly of the parentVisualElement script. Just use the following code. "typeof([INSERT_YOUR_PARENT_SCRIPT_HERE]).Assembly"</param>
        /// <returns>a list of all types from the scripts that inherit from the baseScriptType</returns>
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

        /// <summary>
        /// Adds all the necessary UI for a branch. You have to set the "currentBranchBeingDrawn" to the branch being rendered before calling this function.
        /// </summary>
        /// <param name="parentVisualElement">the parent element that the foldout with all the data.</param>
        /// <param name="parentBranch">the parent of the branch that is being added. This is very useful </param>
        /// <see cref="currentBranchBeingDrawn"/>
        private void AddUIForBranch(VisualElement parentVisualElement, int[] traceBackPath)
        {
            serializedObject.Update();
            PropertyField refBranchIntField = new PropertyField(serializedObject.FindProperty("refBranch._int"));

            refBranchIntField.RegisterValueChangeCallback((SerializedPropertyChangeEvent callback) =>
            {
                // stateMachine.refBranch._int = callback.newValue;
                serializedObject.ApplyModifiedProperties();
            });

            parentVisualElement.Add(refBranchIntField);


            // Cannot easily remove all childBranches from events. Instead the value to be the same as an empty event.
            OnChildrenReadyToDraw = EmptyAction;


            string currentBranchPath = "rootBranch";
            StateMachineScriptableObject.Branch currentBranch = stateMachine.rootBranch;
            for (int i = 0; i < traceBackPath.Length; i++)
            {
                currentBranchPath += ".childBranches.Array.data[" + traceBackPath[i] + "]";
                Debug.Log(currentBranchPath);
                Debug.Log(serializedObject.FindProperty(currentBranchPath + "._int"));
                currentBranch = currentBranch.childBranches[traceBackPath[i]];
            }

            // Might not need this?
            // currentBranch.parent = parentBranch;

            #region Fouldout
            Debug.Log("" +(serializedObject.FindProperty(currentBranchPath + ".isFoldedOut").displayName));
            Foldout foldout = new Foldout();
            foldout.value = currentBranch.isFoldedOut;
            foldout.RegisterValueChangedCallback((ChangeEvent<bool> callback) =>
            {
                currentBranch.isFoldedOut = callback.newValue;
            });
            #endregion

            #region stateDropdownField
            int defaultStateDropdownFieldIndex = 0;
            if (currentBranch.state != null)
            {
                for (int i = 0; i < typesInheritedFromSateList.Count; i++)
                {
                    if (typesInheritedFromSateList[i] == currentBranch.state.GetType())
                    {
                        defaultStateDropdownFieldIndex = i;
                        break;
                    }
                }
            }
            DropdownField stateDropdownField = new DropdownField("State", typesInheritedFromStateNamesList, defaultStateDropdownFieldIndex);

            Debug.Log(currentBranch == stateMachine.rootBranch);
            bool isRoot = currentBranch == stateMachine.rootBranch;
            StateMachineScriptableObject.Branch testCurrentBranch = currentBranch;
            stateDropdownField.RegisterValueChangedCallback((ChangeEvent<string> callback) =>
            {
                Debug.Log("Is Root: " + isRoot);

                int index = typesInheritedFromStateNamesList.IndexOf(callback.newValue);

                foldout.text = typesInheritedFromStateNamesList[index];


                ConstructorInfo[] constructorArray = typesInheritedFromSateList[index].GetConstructors();

                Debug.Log("Construct info for " + typesInheritedFromStateNamesList[index] + ", Name: " + constructorArray[0].Name);

                object stateObject = constructorArray[0].Invoke(new object[] { });


                if (stateObject is State state)
                {
                    Debug.Log("state: " + state);
                    Debug.Log("currentBranch == stateMachine.rootBranch: " + (testCurrentBranch == stateMachine.rootBranch));
                    testCurrentBranch.state = state;
                    Debug.Log("stateMachine.rootBranch.state: " + stateMachine.rootBranch.state);
                }

            });

            foldout.text = stateDropdownField.value;

            foldout.Add(stateDropdownField);
            #endregion

            IntegerField integerField = new IntegerField("Test Int");
            integerField.value = currentBranch._int;

            integerField.RegisterValueChangedCallback((ChangeEvent<int> callback) =>
            {
                currentBranch._int = callback.newValue;
            });

            foldout.Add(integerField);

            #region addNewChildButton
            Button addNewChildButton = new Button(() =>
            {
                currentBranch.childBranches.Add(new StateMachineScriptableObject.Branch());
                List<int> traceBackPathList = traceBackPath.ToList();
                traceBackPathList.Add(0);
                AddUIForBranch(foldout, traceBackPathList.ToArray());
            });
            addNewChildButton.text = "Add New Child";

            foldout.Add(addNewChildButton);
            #endregion

            #region Remove Self From Parent Button
            if (currentBranch != stateMachine.rootBranch)
            {
                Button removeSelfFromParentButton = new Button(() =>
                {
                    currentBranch.parent.childBranches.Remove(currentBranch);
                    foldout.parent.Remove(foldout);
                });
                removeSelfFromParentButton.text = "Remove Self";

                foldout.Add(removeSelfFromParentButton);
            }
            #endregion

            parentVisualElement.Add(foldout);


            // Check for childBranches, and Get ready to update them
            for (int i = 0; i < currentBranch.childBranches.Count; i++)
            {
                int j = i;
                OnChildrenReadyToDraw += () =>
                {
                    currentBranch = currentBranch.childBranches[j];
                    List<int> traceBackPathList = traceBackPath.ToList();
                    traceBackPathList.Add(j);
                    AddUIForBranch(foldout, traceBackPathList.ToArray());
                };
            }

            // Go to next line if there is a next line
            if (OnChildrenReadyToDraw == null)
            {
                return;
            }
            else
            {
                OnChildrenReadyToDraw.Invoke();
            }

        }
        
        
        
        /*
        /// <summary>
        /// Adds all the necessary UI for a branch. You have to set the "currentBranchBeingDrawn" to the branch being rendered before calling this function.
        /// </summary>
        /// <param name="parentVisualElement">the parent element that the foldout with all the data.</param>
        /// <param name="parentBranch">the parent of the branch that is being added. This is very useful </param>
        /// <see cref="currentBranchBeingDrawn"/>
        private void AddUIForBranch(VisualElement parentVisualElement, StateMachineScriptableObject.Branch parentBranch)
        {
            // Cannot easily remove all childBranches from events. Instead the value to be the same as an empty event.
            OnChildrenReadyToDraw = EmptyAction;

            // Coping the current branch so this branch can be saved for use in LAMBDAs
            StateMachineScriptableObject.Branch currentBranch = currentBranchBeingDrawn;

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
            if (currentBranchBeingDrawn.state != null)
            {
                for (int i = 0; i < typesInheritedFromSateList.Count; i++)
                {
                    if (typesInheritedFromSateList[i] == currentBranchBeingDrawn.state.GetType())
                    {
                        defaultStateDropdownFieldIndex = i;
                        break;
                    }
                }
            }
            DropdownField stateDropdownField = new DropdownField("State", typesInheritedFromStateNamesList, defaultStateDropdownFieldIndex);

            stateDropdownField.RegisterValueChangedCallback((ChangeEvent<string> callback) =>
            {
                int index = typesInheritedFromStateNamesList.IndexOf(callback.newValue);

                foldout.text = typesInheritedFromStateNamesList[index];


                ConstructorInfo[] constructorArray = typesInheritedFromSateList[index].GetConstructors();

                Debug.Log("Construct info for " + typesInheritedFromStateNamesList[index] + ", Name: " + constructorArray[0].Name);

                object stateObject = constructorArray[0].Invoke(new object[] { });
                

                if (stateObject is State state)
                {
                    currentBranch.state = state;
                }

            });

            foldout.text = stateDropdownField.value;

            foldout.Add(stateDropdownField);
            #endregion

            IntegerField integerField = new IntegerField("Test Int");
            integerField.value = currentBranchBeingDrawn._int;

            integerField.RegisterValueChangedCallback((ChangeEvent<int> callback) =>
            {
                currentBranchBeingDrawn._int = callback.newValue;
            });

            foldout.Add(integerField);

            #region addNewChildButton
            Button addNewChildButton = new Button(() => 
            {
                currentBranch.childBranches.Add(new StateMachineScriptableObject.Branch());
                currentBranchBeingDrawn = currentBranch.childBranches[currentBranch.childBranches.Count - 1];
                AddUIForBranch(foldout, currentBranch);
            });
            addNewChildButton.text = "Add New Child";

            foldout.Add(addNewChildButton);
            #endregion

            #region Remove Self From Parent Button
            if (currentBranchBeingDrawn != stateMachine.rootBranch)
            {
                Button removeSelfFromParentButton = new Button(() =>
                {
                    currentBranch.parent.childBranches.Remove(currentBranch);
                    foldout.parent.Remove(foldout);
                });
                removeSelfFromParentButton.text = "Remove Self";

                foldout.Add(removeSelfFromParentButton);
            }
            #endregion

            parentVisualElement.Add(foldout);


            // Check for childBranches, and Get ready to update them
            for (int i = 0; i < currentBranchBeingDrawn.childBranches.Count; i++)
            {
                int j = i;
                OnChildrenReadyToDraw += () =>
                {
                    currentBranchBeingDrawn = currentBranch.childBranches[j];
                    AddUIForBranch(foldout, currentBranch);
                };
            }

            // Go to next line if there is a next line
            if (OnChildrenReadyToDraw == null)
            {
                return;
            }
            else
            {
                OnChildrenReadyToDraw.Invoke();
            }

        }
*/

    }
}

// #endif
#endif