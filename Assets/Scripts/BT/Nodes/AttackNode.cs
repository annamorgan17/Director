using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    public AttackNode(EnemyAI owner) : base(owner)
    {

    }

    public override NodeState Update()
    {

        AttackAnimation();
        AIManager.GetPlayer.GetComponent<PlayerScript>().ReduceHealth();
        owner.justAttacked = true;
        return NodeState.SUCCESS;

    }

    private void AttackAnimation()
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
