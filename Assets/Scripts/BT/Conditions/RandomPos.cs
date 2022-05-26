using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPos : Node //creates a random location within wander distance
{
    Vector3 target; // position of the centre of the wander
    public RandomPos(EnemyAI owner, Vector3 m_target) : base(owner)
    {
        target = m_target; //sets parameter to target
    }

    public override NodeState Update()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * AIManager.GetPassiveWander; //creates a random position within wander distance
        randomDirection += target; //adjust to centre target
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navhit, AIManager.GetPassiveWander, -1); //uses the nav mesh to create a point with the new random position

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
