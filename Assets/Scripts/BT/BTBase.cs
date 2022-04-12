using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTBase 
{
    protected EnemyAI Owner { get; private set; } 
    public Node Root { get; protected set; } 

    public BTBase(EnemyAI owner)
    {
        Owner = owner;
    }
    public void Update()
    {
        Root.Update();
    }
}
