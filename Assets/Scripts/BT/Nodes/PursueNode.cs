using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueNode : Node
{
    public PursueNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        owner.anim.SetInteger("battle", 1);

        owner.currentTarget = AIManager.GetPlayer.transform.position;

        float distance = Vector3.Distance(owner.currentTarget, owner.transform.position);

        if (distance > AIManager.GetStoppingDist)
        {
            owner.NavComponent.isStopped = false;
            owner.NavComponent.speed = AIManager.GetRunSpeed;
            owner.anim.SetInteger("moving", 1);
            owner.NavComponent.SetDestination(owner.currentTarget);
            Debug.Log("pursuing -- chasing");
            return NodeState.RUNNING; // if cant see or hear stop
        }
        else if(distance < AIManager.GetStoppingDist)
        {
            owner.NavComponent.isStopped = true;
            Debug.Log("pursuing -- complete");
            return NodeState.SUCCESS;
        }

        owner.NavComponent.isStopped = true;
        Debug.Log("pursuing -- failed");
        return NodeState.FAILURE;

    }
}
