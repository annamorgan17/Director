using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState //states that a node can be in
{
    RUNNING,
    SUCCESS,
    FAILURE,
    NONE
}

public abstract class Node //inherited by the action and condtion nodes, used as a base
{

    public EnemyAI owner { get; private set; } //getter / setter for the enemy ai class

    public Node(EnemyAI owner) //constructor which connects the owner to enemy ai class
    {
        this.owner = owner;
    }

    public virtual NodeState Update() //acts like an update function and needs to have a node state returned
    {
        return NodeState.NONE;
    }
}



