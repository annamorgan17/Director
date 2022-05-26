using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTBase //runs the update function on the root node, starting the behaviour tree
{
    protected EnemyAI Owner { get; private set; }  //enemy ai getter /setter
    public Node Root { get; protected set; } //node getter / setter

    public BTBase(EnemyAI owner) //construcor which connect enemy ai to owner
    {
        Owner = owner;
    }
    public void Update() //runs the update function from the node class 
    {
        Root.Update();
    }
}
