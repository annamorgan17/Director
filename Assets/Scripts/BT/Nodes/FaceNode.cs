using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceNode :  Node //turns the creature to face the player
{
    public FaceNode(EnemyAI owner) : base(owner)
    {
       
    }

    public override NodeState Update()
    {
        owner.transform.LookAt(AIManager.GetPlayer.transform.position); //rotates the creature to look at player
    
        return NodeState.SUCCESS; //returns success

    }
}
