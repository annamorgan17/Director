using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerWalking : Node
{
    FirstPersonController playerScript;
    public IsPlayerWalking(EnemyAI owner) : base(owner)
    {
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>();
    }

    public override NodeState Update()
    {

        if (playerScript.isWalking == true)
        {
            Debug.Log("walk heard");
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;

    }
}
