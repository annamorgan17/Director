using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    public AttackNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

       
        return NodeState.FAILURE;

    }
}
