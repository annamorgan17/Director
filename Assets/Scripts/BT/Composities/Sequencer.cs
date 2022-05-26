using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sequencer : BaseComp 
{
     public Sequencer(EnemyAI owner) : base(owner) //inherts from base comp, will only move to next node if previous succeded
     {

     }
     public override NodeState Update()
     {
        NodeState _nodeState = NodeState.FAILURE;//fail as default
        Node currentNode = nodes[currentNodeIndex]; //current node set to current node

        if (currentNode != null)  //if not null
        {
            NodeState currentNodeState = currentNode.Update();   //run update

            if (currentNodeState == NodeState.SUCCESS) //if success
            {
                if (currentNodeIndex == nodes.Count - 1) //out of nodes
                {
                    _nodeState = NodeState.SUCCESS; //set as success
                }
                else //if more nodes
                {
                    ++currentNodeIndex; //move to next node
                    _nodeState = NodeState.RUNNING; //set as running
                }
            }
            else //if anything but success
            {
                _nodeState = currentNodeState; //set to current node state
            }
        }
        //after all nodes if it succeeded or failed 
        if (_nodeState == NodeState.SUCCESS || _nodeState == NodeState.FAILURE)
        {
            ResetIndex(); //reset curent node
        }

        return _nodeState; //return state 
     }
}


