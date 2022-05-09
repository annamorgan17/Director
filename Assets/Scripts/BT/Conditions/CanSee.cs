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
        Collider[] targetsInVR = Physics.OverlapSphere(AIManager.GetPlayer.transform.position, AIManager.GetSightPlayerLength, LayerMask.GetMask("Player"));

        for (int i = 0; i < targetsInVR.Length; i++)
        {
            Transform target = targetsInVR[i].transform;
            Vector3 dirToTarget = (target.position - AIManager.GetPlayer.transform.position).normalized;
            if (Vector3.Angle(AIManager.GetPlayer.transform.forward, dirToTarget) < AIManager.GetSightPlayerAngle)
            {
                float dstToTarget = Vector3.Distance(target.position, AIManager.GetPlayer.transform.position);
                if (!Physics.Raycast(AIManager.GetPlayer.transform.position, dirToTarget, out hit, dstToTarget, LayerMask.GetMask("Object"))) //if there are no objects in the way
                {
                    owner.lastKnownLocation = hit.transform.position;
                    return NodeState.SUCCESS;
                }
            }
        }

        return NodeState.FAILURE;

    }
}
