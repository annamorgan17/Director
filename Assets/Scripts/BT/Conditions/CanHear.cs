using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHear : Node
{
    public CanHear(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        float distance = Vector3.Distance(owner.transform.position, AIManager.GetPlayer.transform.position);

        if(distance <= AIManager.GetHearingRadius)
        {
            Debug.Log("heard");
            return NodeState.SUCCESS;
        }
        Debug.Log("not heard");
        return NodeState.FAILURE;

    }
}
