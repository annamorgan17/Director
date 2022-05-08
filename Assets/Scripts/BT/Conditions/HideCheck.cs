using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCheck : Node
{
    public HideCheck(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
    
        if(AIManager.GetHideBool)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}
