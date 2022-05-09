using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntNode : Node
{
    public HuntNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        owner.anim.SetInteger("battle", 1);

        float distance = Vector3.Distance(owner.currentTarget, owner.transform.position);

        if (distance > AIManager.GetStoppingDist)
        {
            owner.NavComponent.isStopped = false;
            owner.NavComponent.speed = AIManager.GetRunSpeed;
            owner.anim.SetInteger("moving", 1);
            owner.NavComponent.SetDestination(owner.currentTarget);
            Debug.Log("Hunt -- chasing");
            return NodeState.RUNNING;
        }
        else if (distance < AIManager.GetStoppingDist)
        {
            owner.NavComponent.isStopped = true;
            owner.hunt = true;
            Debug.Log("Hunt -- complete");
            return NodeState.SUCCESS;
        }

        owner.NavComponent.isStopped = true;
        Debug.Log("Hunt -- failed");
        return NodeState.FAILURE;

    }
}
