using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerCrouching : Node //checks if the player is crouching
{
    FirstPersonController playerScript; //link to the first person controller - controller not made by me -  from unity asset store
    public IsPlayerCrouching(EnemyAI owner) : base(owner)
    {
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>(); //connects first person controller script
    }

    public override NodeState Update()
    {

        if (playerScript.isCrouched == true) //checks bool which is triggered when the player is crouching
        {
            return NodeState.SUCCESS; //return success
        }

        return NodeState.FAILURE; //return fail

    }
}
