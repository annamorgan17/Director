using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustAttacked : Node
{
    public JustAttacked(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.justAttacked == true)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}
