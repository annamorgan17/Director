using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlace : Node
{
    public SetPlace(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (AIManager.GetSetPoint != null)
        {
            owner.currentTarget = AIManager.GetSetPoint.transform.position;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }


    }
}
