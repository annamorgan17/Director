using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerCrouching : Node
{
    FirstPersonController playerScript;
    public IsPlayerCrouching(EnemyAI owner) : base(owner)
    {
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>();
    }

    public override NodeState Update()
    {

        if (playerScript.isCrouched == true)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;

    }
}
