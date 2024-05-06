using System.Collections.Generic;

using UnityEngine;

public class BlackBoard
{
    public Dictionary<string, bool> boolBlackBoard = new Dictionary<string, bool>();
    public Dictionary<string, int> intBlackBoard = new Dictionary<string, int>();
    public Dictionary<string, float> floatBlackBoard = new Dictionary<string, float>();
    public Dictionary<string, string> stringBlackBoard = new Dictionary<string, string>();
    public Dictionary<string, Component> componentBlackBoard = new Dictionary<string, Component>();
}