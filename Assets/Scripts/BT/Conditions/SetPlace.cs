using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlace : Node //no longer used -  was used for testing moving the creature to a set point - used to test nav mesh links
{
    public SetPlace(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (AIManager.GetSetPoint != null) //if not null
        {
            owner.currentTarget = AIManager.GetSetPoint.transform.position; //set current target to the set point
            return NodeState.SUCCESS; //return success 
        }
        else
        {
            return NodeState.FAILURE; //return fail
        }


    }
}
