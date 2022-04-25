using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchNoiseCheck : Node
{
    public CrouchNoiseCheck(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        float distance = Vector3.Distance(owner.transform.position, AIManager.GetPlayer.transform.position);

        if (distance <= AIManager.GetInstantHeardRadius)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;

    }
}
