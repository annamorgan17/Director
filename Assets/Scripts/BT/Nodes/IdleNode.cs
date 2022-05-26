using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : Node //makes the creature idle for a second after attacking
{
    public IdleNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        owner.anim.SetInteger("battle", 1); //runs animation
        owner.anim.SetInteger("moving", 0); //stops running / walking animation
        owner.NavComponent.isStopped = true; //stops moving

        owner.timer += Time.deltaTime; //starts timer

        if(owner.timer >= 1.0f) //if over a second
        {
            owner.timer = 0; //reset timer
            owner.justAttacked = false; //reset just attacked bool
            return NodeState.SUCCESS; //return success
        }
        else //if timer not finished
        {
            return NodeState.RUNNING; // return running
        }
    }
}
