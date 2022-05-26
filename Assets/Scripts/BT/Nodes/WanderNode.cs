using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNode : Node //script for moving the creature to the random wander/hunt position
{
    int agro; //whether the creature needs aggressive or defauot animations
    FirstPersonController playerScript; //links to the first person controller scirpt - not made by me - from unity asset store
    public WanderNode(EnemyAI owner, int m_agro) : base(owner)
    {
        agro = m_agro; //sets agro to parameter
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>(); //connect first person controller script
    }

    public override NodeState Update()
    {
        owner.anim.SetInteger("battle", agro); //set animation

        if(IfSee() || IfHear()) //if the creature sees or hears the player
        {
            return NodeState.FAILURE; //return fail
        }

        if (owner.currentTarget != null)  //if noy null
        {
            float distance = Vector3.Distance(owner.currentTarget, owner.transform.position); //calculate distance from creature to target

            if (distance > AIManager.GetStoppingDist) //if bigger than stopping distance
            {
                owner.NavComponent.isStopped = false; //moving
                owner.NavComponent.speed = AIManager.GetWalkSpeed; //set speed to walk
                owner.anim.SetInteger("moving", 1); //set moving animation
                owner.NavComponent.SetDestination(owner.currentTarget); //move to target
                Debug.Log("wandering -- walking"); //debug log
                return NodeState.RUNNING;  //return running
            }
            else //if within stopping distance
            {
                owner.NavComponent.isStopped = true; //stop moving
                Debug.Log("wandering -- complete"); //debug log
                return NodeState.SUCCESS; //return success
            }
        }
        owner.NavComponent.isStopped = true; //stop moving
        Debug.Log("wandering -- failed"); //debug log
        return NodeState.FAILURE; //return fail

    }

    private bool IfSee() //same as the vision cone code from canSee script
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
                    return true;
                }
            }
        }

        return false;
    }

    private bool IfHear() //checks the hearing radius like canHear script, then checks if the player was jumping
    {
        float distance = Vector3.Distance(owner.transform.position, AIManager.GetPlayer.transform.position);

        if (distance <= AIManager.GetHearingRadius)
        {
            if (playerScript.isGrounded == false)
            {
                return true;
            }
        }
        return false;
    }
}
