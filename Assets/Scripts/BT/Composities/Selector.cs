using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BaseComp //inherits from base comp, going through node list and only continuing if the previous node fails
{
    public Selector(EnemyAI owner) : base(owner)
    {

    }
    public override NodeState Update()
    {
        NodeState _nodeState = NodeState.FAILURE;  //default to fail
        Node currentNode = nodes[currentNodeIndex]; //set current node to the current node

        if (currentNode != null) //if not null
        {
            NodeState currentNodeState = currentNode.Update();  //run update

            if (currentNodeState == NodeState.FAILURE) //if fails
            {
                if (currentNodeIndex == nodes.Count - 1) // and out of nodes
                {
                    _nodeState = NodeState.FAILURE;  //fail the selector
                }
                else //if more nodes
                {
                    ++currentNodeIndex; //move to next node
                    _nodeState = NodeState.RUNNING; //selector running
                }
            }
            else //anything but fail
            {
                _nodeState = currentNodeState; //node state is the current node state
            }
        }
        //after all nodes if fail or success
        if (_nodeState == NodeState.SUCCESS || _nodeState == NodeState.FAILURE) 
        {
             ResetIndex(); //reset current node
        }

        return _nodeState; //return state
    }
}



