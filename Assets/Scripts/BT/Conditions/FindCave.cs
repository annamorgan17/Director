using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCave : Node
{
    public FindCave(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        GameObject[] caves = GameObject.FindGameObjectsWithTag("Cave");
        GameObject closeCave = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = owner.transform.position;
        foreach (GameObject c in caves)
        {
            float dist = Vector3.Distance(c.transform.position, currentPos);
            if (dist < minDist)
            {
                closeCave = c;
                minDist = dist;
            }
        }

        owner.currentTarget = closeCave.transform.position;
        return NodeState.SUCCESS;

    }
}
