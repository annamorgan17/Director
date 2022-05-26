using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWanderCheck : Node //checks if close wander or hunting has been activated
{
    public CloseWanderCheck(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        if(owner.hunt == true) //checks if bool is true
        {
            return NodeState.SUCCESS; //returns success
        }
        else
        {
            return NodeState.FAILURE; //returns fail
        }

    }
}
