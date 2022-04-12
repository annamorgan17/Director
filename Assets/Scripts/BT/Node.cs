using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE,
    NONE
}

public abstract class Node 
{

    public EnemyAI owner { get; private set; } 

    public Node(EnemyAI owner) 
    {
        this.owner = owner;
    }

    public virtual NodeState Update() 
    {
        return NodeState.NONE;
    }
}



