using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHear : Node //checks if the player is within the creatures hearing radius
{
    public CanHear(EnemyAI owner) : base(owner) 
    {

    }

    public override NodeState Update()
    {
        float distance = Vector3.Distance(owner.transform.position, AIManager.GetPlayer.transform.position); //calulate distance from creature to player

        if(distance <= AIManager.GetHearingRadius) //if within hearing distance
        {
            Debug.Log("heard"); //debug log
            return NodeState.SUCCESS; //return success
        }
        Debug.Log("not heard"); //debug log
        return NodeState.FAILURE; //return fail

    }
}
