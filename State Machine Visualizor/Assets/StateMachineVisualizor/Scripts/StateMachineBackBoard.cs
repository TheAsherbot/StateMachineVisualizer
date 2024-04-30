using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

#if ODIN_INSPECTOR
public class StateMachineBackBoard : SerializedMonoBehaviour
{
#else
public class StateMachineBackBoard : MonoBehaviour
#endif

    [SerializeField] public Dictionary<string, int> intBlackBoard = new Dictionary<string, int>();
    [SerializeField] public Dictionary<string, float> floatBlackBoard = new Dictionary<string, float>();
    [SerializeField] public Dictionary<string, string> stringBlackBoard = new Dictionary<string, string>();
    [SerializeField] public Dictionary<string, bool> boolBlackBoard = new Dictionary<string, bool>();
    [SerializeField] public Dictionary<string, Component> componentBlackBoard = new Dictionary<string, Component>();

}
