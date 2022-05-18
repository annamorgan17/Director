using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNode : Node
{
    int agro;
    FirstPersonController playerScript;
    public WanderNode(EnemyAI owner, int m_agro) : base(owner)
    {
        agro = m_agro;
        playerScript = AIManager.GetPlayer.GetComponent<FirstPersonController>();
    }

    public override NodeState Update()
    {
        owner.anim.SetInteger("battle", agro);

        if(IfSee() || IfHear())
        {
            return NodeState.FAILURE;
        }

        if (owner.currentTarget != null) 
        {
            float distance = Vector3.Distance(owner.currentTarget, owner.transform.position);

            if (distance > AIManager.GetStoppingDist)
            {
                owner.NavComponent.isStopped = false;
                owner.NavComponent.speed = AIManager.GetWalkSpeed;
                owner.anim.SetInteger("moving", 1);
                owner.NavComponent.SetDestination(owner.currentTarget);
                Debug.Log("wandering -- walking");
                return NodeState.RUNNING; 
            }
            else 
            {
                owner.NavComponent.isStopped = true;
                Debug.Log("wandering -- complete");
                return NodeState.SUCCESS; 
            }
        }
        owner.NavComponent.isStopped = true;
        Debug.Log("wandering -- failed");
        return NodeState.FAILURE;

    }

    private bool IfSee()
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

    private bool IfHear()
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
