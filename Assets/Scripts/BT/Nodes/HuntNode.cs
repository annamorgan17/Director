using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntNode : Node
{
    public HuntNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        owner.anim.SetInteger("battle", 1);

        if(IfSee())
        {
            return NodeState.FAILURE;
        }

        float distance = Vector3.Distance(owner.currentTarget, owner.transform.position);

        if (distance > AIManager.GetStoppingDist)
        {
            owner.NavComponent.isStopped = false;
            owner.NavComponent.speed = AIManager.GetRunSpeed;
            owner.anim.SetInteger("moving", 1);
            owner.NavComponent.SetDestination(owner.currentTarget);
            Debug.Log("Hunt -- chasing");
            return NodeState.RUNNING;
        }
        else if (distance < AIManager.GetStoppingDist)
        {
            owner.NavComponent.isStopped = true;
            owner.hunt = true;
            Debug.Log("Hunt -- complete");
            return NodeState.SUCCESS;
        }

        owner.NavComponent.isStopped = true;
        Debug.Log("Hunt -- failed");
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
