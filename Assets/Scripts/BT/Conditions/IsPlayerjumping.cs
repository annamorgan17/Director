using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerjumping : Node //checks if the player is jumping
{
    FirstPersonController playerScript; //link to the first person controller - controller not made by me - from unity asset store
    public IsPlayerjumping(EnemyAI owner) : base(owner)
    {
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>(); //connects first person controller script
    }

    public override NodeState Update()
    {

        if (playerScript.isGrounded == false) //if bool is false, checks whether the player is in the air
        {
            return NodeState.SUCCESS; //return success 
        }

        return NodeState.FAILURE; //return fail

    }
}
