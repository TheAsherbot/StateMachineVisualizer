using System;
using System.Collections.Generic;

using UnityEngine;

public class WalkState : State
{



    public override Dictionary<string, Type> NeededBlackBoardItems()
    {
        return new Dictionary<string, Type>()
        {
            { "Transform", typeof(Component) },
            { "RigidBody", typeof(Component) }
        };
    }

}
