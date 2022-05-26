using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNode : Node //moves to the closest cave, waits for timer, then continues
{
    public HideNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        float dist = Vector3.Distance(owner.transform.position, owner.currentTarget); //calculate distance from creature to cave
        if(dist > 3.0f) //if not in cave
        {
            owner.anim.SetInteger("battle", 1); //set animation
            owner.NavComponent.isStopped = false; //moving
            owner.NavComponent.speed = AIManager.GetRunSpeed; //set to running
            owner.anim.SetInteger("moving", 1); //aggressive animation
            owner.NavComponent.SetDestination(owner.currentTarget); //set destination to cave
            Debug.Log("Hiding -- walking"); //debug log
            return NodeState.RUNNING; //return running

        }
        else
        {
            owner.anim.SetInteger("battle", 0); //default animation
            owner.NavComponent.isStopped = true; //stopped moving 
            owner.NavComponent.speed = AIManager.GetWalkSpeed; //set to walking
            owner.hunt = false; //not hunting
            owner.anim.SetInteger("moving", 0); //not aggressive animation

            owner.timer += Time.deltaTime; //start timer

            if(owner.timer >= 4.0f) //if above 4 seconds
            {
                return NodeState.SUCCESS; //return success
            }
            else //if time not up
            {
                Debug.Log("Hiding -- Hidden"); //debug log
                return NodeState.RUNNING; //return running
            }
        }
    }
}
