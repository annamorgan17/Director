using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCheck : Node //checks whether director has instructed the creature to hide
{
    public HideCheck(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
    
        if(AIManager.GetHideBool) //if bool is true
        {
            return NodeState.SUCCESS; //return success
        }
        else
        {
            return NodeState.FAILURE; //return fail
        }

    }
}
