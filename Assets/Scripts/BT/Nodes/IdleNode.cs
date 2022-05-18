using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : Node
{
    public IdleNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        owner.anim.SetInteger("battle", 1);
        owner.anim.SetInteger("moving", 0);
        owner.NavComponent.isStopped = true;

        owner.timer += Time.deltaTime;

        if(owner.timer >= 2.0f)
        {
            owner.timer = 0;
            owner.justAttacked = false;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
}
