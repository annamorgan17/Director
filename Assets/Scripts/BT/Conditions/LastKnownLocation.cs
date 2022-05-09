using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastKnownLocation : Node
{
    public LastKnownLocation(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        owner.lastKnownLocation = owner.currentTarget;

        return NodeState.SUCCESS;

    }
}
