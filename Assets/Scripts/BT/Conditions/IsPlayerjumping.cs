using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerjumping : Node
{
    FirstPersonController playerScript;
    public IsPlayerjumping(EnemyAI owner) : base(owner)
    {
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>();
    }

    public override NodeState Update()
    {

        if (playerScript.isGrounded == false)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;

    }
}
