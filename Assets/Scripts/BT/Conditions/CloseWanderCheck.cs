using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWanderCheck : Node
{
    public CloseWanderCheck(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        if(owner.hunt == true)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}
