using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSee : Node //checks if the player is within the creatures vision cone
{
    public CanSee(EnemyAI owner) : base(owner) 
    {

    }

    public override NodeState Update()
    {
        Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, AIManager.GetSightDistance, LayerMask.GetMask("Player"));//gets all colliders within distance labelled as player

        for (int i = 0; i < targetsInVR.Length; i++) //loop through them
        {
            Transform target = targetsInVR[i].transform; //set current transform to target
            Vector3 dirToTarget = (target.position - owner.transform.position).normalized; //calulate direction from creature to target
            if (Vector3.Angle(owner.transform.forward, dirToTarget) < AIManager.GetSightRadius) //if within vision angle
            {
                float dstToTarget = Vector3.Distance(target.position, owner.transform.position); //calculates distance from creature to target
                if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, LayerMask.GetMask("Object"))) //if there are no objects in the way
                {
                    owner.lastKnownLocation = target.transform.position; //set this as the last known location
                    return NodeState.SUCCESS; //return success
                    Debug.Log("see"); //debug log
                }
            }
        }

        return NodeState.FAILURE; //return fail
        Debug.Log("not see"); //debug log

    }
}
