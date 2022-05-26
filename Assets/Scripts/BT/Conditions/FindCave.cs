using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCave : Node //finds the closest cave and sets it as a target
{
    public FindCave(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        GameObject[] caves = GameObject.FindGameObjectsWithTag("Cave"); //find all objects tagged as cave
        GameObject closeCave = null; //create a temp object to hold current closest 
        float minDist = Mathf.Infinity; //acts as  temp value for current minimum distance
        Vector3 currentPos = owner.transform.position; //current position of creature
        foreach (GameObject c in caves) //loop through all caves
        {
            float dist = Vector3.Distance(c.transform.position, currentPos); //calculate distance from creature to current cave
            if (dist < minDist) //if distance is smaller than temp minimum distance
            {
                closeCave = c; //set current cave to temp object 
                minDist = dist; //set temp distance to current distance
            }
        }
        //after all caves are looped through
        owner.currentTarget = closeCave.transform.position; //set current target to closest cave
        return NodeState.SUCCESS; //return success

    }
}
