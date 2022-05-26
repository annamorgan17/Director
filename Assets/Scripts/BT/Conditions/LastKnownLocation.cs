using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastKnownLocation : Node //sets the last known location of the player to the current target
{
    public LastKnownLocation(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        owner.lastKnownLocation = owner.currentTarget; //sets last known location to target

        return NodeState.SUCCESS; //return success

    }
}
