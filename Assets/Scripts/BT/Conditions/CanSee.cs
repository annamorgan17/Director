using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSee : Node
{
    RaycastHit hit;
    public CanSee(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, AIManager.GetSightDistance, LayerMask.GetMask("Player"));

        for (int i = 0; i < targetsInVR.Length; i++)
        {
            Transform target = targetsInVR[i].transform;
            Vector3 dirToTarget = (target.position - owner.transform.position).normalized;
            if (Vector3.Angle(owner.transform.forward, dirToTarget) < AIManager.GetSightRadius)
            {
                float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                if (!Physics.Raycast(owner.transform.position, dirToTarget, out hit, dstToTarget, LayerMask.GetMask("Object"))) //if there are no objects in the way
                {
                    owner.lastKnownLocation = target.transform.position;
                    return NodeState.SUCCESS;
                    Debug.Log("see");
                }
            }
        }

        return NodeState.FAILURE;
        Debug.Log("not see");

    }
}
