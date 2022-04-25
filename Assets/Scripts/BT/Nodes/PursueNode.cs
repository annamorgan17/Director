using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueNode : Node
{
    public PursueNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {


        return NodeState.FAILURE;

    }
}
