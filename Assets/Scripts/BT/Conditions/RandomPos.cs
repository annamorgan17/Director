using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPos : Node
{
    Vector3 target;
    public RandomPos(EnemyAI owner, Vector3 m_target) : base(owner)
    {
        target = m_target;
    }

    public override NodeState Update()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * AIManager.GetPassiveWander;
        randomDirection += target;
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
