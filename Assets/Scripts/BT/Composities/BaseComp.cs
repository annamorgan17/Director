using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseComp : Node //used to store shared data and functions by sequencer and selector
{
    public List<Node> nodes { get; private set; } //list of the nodes
    protected int currentNodeIndex = 0; //current node

    protected BaseComp(EnemyAI owner) : base(owner) //constructor
    {
        currentNodeIndex = 0; 
        nodes = new List<Node>(); 
    }

    public void AddNode(Node newNode)  //function for adding new nodes to list
    {
        nodes.Add(newNode);
    }

    protected void ResetIndex() //function for resetting current node
    {
        currentNodeIndex = 0;
    }
}


