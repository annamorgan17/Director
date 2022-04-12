using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPos : Node
{
    public RandomPos(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * AIManager.GetPassiveWander;
        randomDirection += Vector3.zero;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navhit, AIManager.GetPassiveWander, -1);

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
