using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNode : Node
{
    public HideNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        

        float dist = Vector3.Distance(owner.transform.position, owner.currentTarget);
        if(dist > 1.0f)
        {
            owner.anim.SetInteger("battle", 1);
            owner.NavComponent.isStopped = false;
            owner.NavComponent.speed = AIManager.GetRunSpeed;
            owner.anim.SetInteger("moving", 1);
            owner.NavComponent.SetDestination(owner.currentTarget);
            Debug.Log("Hiding -- walking");
            return NodeState.RUNNING;

        }
        else
        {
            owner.anim.SetInteger("battle", 0);
            owner.NavComponent.isStopped = true;
            owner.NavComponent.speed = AIManager.GetWalkSpeed;
            owner.anim.SetInteger("moving", 0);

            owner.timer += Time.deltaTime;
            if(owner.timer > 5.0f)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                Debug.Log("Hiding -- Hidden");
                return NodeState.RUNNING;
            }
        }
    }
}
