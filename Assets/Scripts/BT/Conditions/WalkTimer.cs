using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTimer : Node
{
    public WalkTimer(EnemyAI owner) : base(owner)
    {
        
    }

    public override NodeState Update()
    {

        owner.timer += Time.deltaTime;

        if(owner.timer >= 5.0f)
        {
            owner.lastKnownLocation = AIManager.GetPlayer.transform.position;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}
