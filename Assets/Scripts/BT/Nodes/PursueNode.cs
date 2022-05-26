using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueNode : Node //causes the creature to run at the player if they are still visible 
{
    public PursueNode(EnemyAI owner) : base(owner) 
    {

    }

    public override NodeState Update()
    {
        if(IfSee()) //if the creature cant see the player
        {
            return NodeState.FAILURE; //return fail
        }

        owner.justAttacked = false; //reset just attacked
        owner.currentTarget = AIManager.GetPlayer.transform.position; //set current position to the player

        float distance = Vector3.Distance(owner.currentTarget, owner.transform.position); //calculate distance from creature to player

        if (distance > AIManager.GetStoppingDist) //if bigger than stopping distance
        {
            owner.anim.SetInteger("battle", 1); //start aggressive animation
            owner.NavComponent.isStopped = false; //not stopped
            owner.NavComponent.speed = AIManager.GetRunSpeed; //set spped to running
            owner.anim.SetInteger("moving", 1); //start running animation
            owner.NavComponent.SetDestination(owner.currentTarget); //move to player
            Debug.Log("pursuing -- chasing"); //debug log
            return NodeState.RUNNING; // return running
        }
        else if(distance <= AIManager.GetStoppingDist) //if within stopping distance
        {
            owner.NavComponent.isStopped = true; //stop moving
            Debug.Log("pursuing -- complete"); //debug log
            return NodeState.SUCCESS; //return success
        }

        owner.NavComponent.isStopped = true; //stop moving
        Debug.Log("pursuing -- failed"); //debug log
        return NodeState.FAILURE; //return fail

    }

    private bool IfSee() //uses the same vision cone code as canSee script with the returns swapped so that if the player isnt seen be true
    {
        Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, AIManager.GetSightDistance, LayerMask.GetMask("Player"));

        for (int i = 0; i < targetsInVR.Length; i++)
        {
            Transform target = targetsInVR[i].transform;
            Vector3 dirToTarget = (target.position - owner.transform.position).normalized;
            if (Vector3.Angle(owner.transform.forward, dirToTarget) < AIManager.GetSightRadius)
            {
                float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                if (!Physics.Raycast(owner.transform.position, dirToTarget, out RaycastHit hit, dstToTarget, LayerMask.GetMask("Object")))
                {
                    owner.lastKnownLocation = target.transform.position;
                    return false;
                }
            }
        }

        return true;
    }
}
