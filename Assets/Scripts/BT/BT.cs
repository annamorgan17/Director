using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : BTBase
{
    //composities
    private Selector rootSelector; //wander attack hunt hide
    private Sequencer wanderSeq;

    //nodes
    private WanderNode wanderNode;

    //conditions
    private RandomPos randomPos;
    private SetPlace setPlace;

    public BT(EnemyAI owner) : base(owner)
    {
        //connections
        //composites
        rootSelector = new Selector(owner);
        wanderSeq = new Sequencer(owner);

        //nodes
        wanderNode = new WanderNode(owner);

        //conditions
        randomPos = new RandomPos(owner);
        setPlace = new SetPlace(owner);

        //adding root
        Root = rootSelector;

        //adding actions to lists
        rootSelector.AddNode(wanderSeq);

        wanderSeq.AddNode(randomPos);
        wanderSeq.AddNode(wanderNode);
    }
}
