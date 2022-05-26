using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustAttacked : Node //checks whether the creature has just attacked the player
{
    public JustAttacked(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {
        if (owner.justAttacked == true) //if bool is true
        {
            return NodeState.SUCCESS; //return success
        }
        else
        {
            return NodeState.FAILURE; //return fail
        }

    }
}
