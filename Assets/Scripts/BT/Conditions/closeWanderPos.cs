using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class closeWanderPos : Node //creates a random position within hunt distance
{
    Vector3 wanderLoc; //centre of the hunt distance / close wander distance 
    public closeWanderPos(EnemyAI owner, Vector3 location) : base(owner)
    {
        wanderLoc = location; // set as parameter
    }

    public override NodeState Update()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * AIManager.GetHuntWander; //find a random position within hunt distance
        randomDirection += wanderLoc; //adjust with the centre posotion
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navhit, AIManager.GetHuntWander, -1); //sample a nav mesh posotion using random pos created 

        if (navhit.position != Vector3.zero) //if not 0
        {
            owner.currentTarget = navhit.position; //set as current target
            return NodeState.SUCCESS; //return success
        }
        else
        {
            return NodeState.FAILURE; //return fail
        }

    }
}
