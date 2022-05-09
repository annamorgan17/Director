using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class closeWanderPos : Node
{
    Vector3 wanderLoc;
    public closeWanderPos(EnemyAI owner, Vector3 location) : base(owner)
    {
        wanderLoc = location;
    }

    public override NodeState Update()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * AIManager.GetHuntWander;
        randomDirection += wanderLoc;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navhit, AIManager.GetHuntWander, -1);

        if (navhit.position != Vector3.zero)
        {
            owner.currentTarget = navhit.position;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}
