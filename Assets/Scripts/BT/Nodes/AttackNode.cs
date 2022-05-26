using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node //causes the creature to run attack animation and lowers the player health
{
    public AttackNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        AttackAnimation(); //run a switch of animations
        AIManager.GetPlayer.GetComponent<PlayerScript>().ReduceHealth(); //reduce the player health
        owner.justAttacked = true; //set just attacked to true
        return NodeState.SUCCESS; //return success

    }

    private void AttackAnimation() //switches on random number and runs 1 0f the three animations based off this number
    {
        int ran = Random.Range(0, 3);
        switch (ran)
        {

            case 0:
                {
                    owner.anim.SetInteger("moving", 2);
                    break;
                }
            case 1:
                {
                    owner.anim.SetInteger("moving", 3);
                    break;
                }
            case 2:
                {
                    owner.anim.SetInteger("moving", 4);
                    break;
                }
        }
    }
}
