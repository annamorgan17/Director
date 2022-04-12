using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNode : Node
{
    public WanderNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        if (owner.currentTarget != null) 
        {
            float distance = Vector3.Distance(owner.currentTarget, owner.transform.position);

            if (distance > AIManager.GetStoppingDist)
            {
                owner.NavComponent.isStopped = false;
                owner.anim.SetInteger("moving", 1);
                owner.NavComponent.SetDestination(owner.currentTarget);
                Debug.Log("wandering -- walking");
                return NodeState.RUNNING; 
            }
            else 
            {
                owner.NavComponent.isStopped = true;
                Debug.Log("wandering -- complete");
                return NodeState.SUCCESS; 
            }
        }
        owner.NavComponent.isStopped = true;
        Debug.Log("wandering -- failed");
        return NodeState.FAILURE;

    }
}
