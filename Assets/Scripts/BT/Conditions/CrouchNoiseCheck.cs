using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchNoiseCheck : Node //checks whether player is within the insatnt hearing radius, only used when the player is crouching
{
    public CrouchNoiseCheck(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        float distance = Vector3.Distance(owner.transform.position, AIManager.GetPlayer.transform.position); // calculate distance from creature to player

        if (distance <= AIManager.GetInstantHeardRadius) //if within instant hearing radius 
        {
            return NodeState.SUCCESS;//return success 
        }

        return NodeState.FAILURE; //return fail

    }
}
