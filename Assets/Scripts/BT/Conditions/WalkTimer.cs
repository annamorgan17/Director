using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTimer : Node //uses a timer to check how long the player has been walking for
{
    public WalkTimer(EnemyAI owner) : base(owner)
    {
        
    }

    public override NodeState Update()
    {

        owner.timer += Time.deltaTime; //start timer

        if(owner.timer >= 5.0f) //if 5 seconds or over
        {
            owner.lastKnownLocation = AIManager.GetPlayer.transform.position; //set last known location to the player location
            return NodeState.SUCCESS; //return success
        }
        else
        {
            return NodeState.FAILURE; //return fail
        }

    }
}
