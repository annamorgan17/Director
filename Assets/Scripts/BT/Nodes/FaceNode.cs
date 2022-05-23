using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceNode :  Node
{
    public FaceNode(EnemyAI owner) : base(owner)
    {
       
    }

    public override NodeState Update()
    {
        owner.transform.LookAt(AIManager.GetPlayer.transform.position);
    
        return NodeState.SUCCESS;

    }
}
