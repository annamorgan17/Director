using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerWalking : Node //checks if the player is walking
{
    FirstPersonController playerScript; //links to the first perosn controller script -  controller not by me - from unity asset store
    public IsPlayerWalking(EnemyAI owner) : base(owner)
    {
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>(); //connects to the first person controller script
    }

    public override NodeState Update()
    {

        if (playerScript.isWalking == true) //if the bool is true, checks if the player is currently walking
        {
            Debug.Log("walk heard"); //debug log
            return NodeState.SUCCESS; // retruns success
        }

        return NodeState.FAILURE; //returns fail

    }
}
