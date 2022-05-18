using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueNode : Node
{
    public PursueNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if(IfSee())
        {
            return NodeState.FAILURE;
        }

        owner.justAttacked = false;
        owner.currentTarget = AIManager.GetPlayer.transform.position;

        float distance = Vector3.Distance(owner.currentTarget, owner.transform.position);

        if (distance > AIManager.GetStoppingDist)
        {
            owner.anim.SetInteger("battle", 1);
            owner.NavComponent.isStopped = false;
            owner.NavComponent.speed = AIManager.GetRunSpeed;
            owner.anim.SetInteger("moving", 1);
            owner.NavComponent.SetDestination(owner.currentTarget);
            Debug.Log("pursuing -- chasing");
            return NodeState.RUNNING; // if cant see or hear stop
        }
        else if(distance <= AIManager.GetStoppingDist)
        {
            owner.NavComponent.isStopped = true;
            Debug.Log("pursuing -- complete");
            return NodeState.SUCCESS;
        }

        owner.NavComponent.isStopped = true;
        Debug.Log("pursuing -- failed");
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
                    return false;
                }
            }
        }

        return true;
    }
}
