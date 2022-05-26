using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour //script attached to creature, acts as the owner to node scripts of the behaviour tree, connects the physical model to the code
{
    public NavMeshAgent NavComponent { get; private set; } //getter / setter for nav mesh agent 
    public Animator anim {get; private set;} //getter / setter for animator

    private BT bahaviourTree; //link to behaviour tree script

    public Vector3 currentTarget; //current target, set within nodes
    public Vector3 lastKnownLocation; //last know location of player, set within nodes
    public Vector3 wanderTarget; //target for wandering, set within nodes

    public bool hunt = false; //bool that checks whether ai should be hunting not wandering
    public bool justAttacked = false; //bool for checking whether ai has just attacked
    public float timer = 0; //timer, which is used within many nodes

    private void Start()
    {
        NavComponent = gameObject.GetComponent<NavMeshAgent>(); //connects nav mesh agent
        bahaviourTree = new BT(this); //connect behaviour tree script
        anim = GetComponent<Animator>(); //connects the animator

        lastKnownLocation = AIManager.GetPlayer.transform.position; //sets the last known location to the players location instantly 
}

    private void Update()
    {
        bahaviourTree.Update(); //runs the behaviour tree

    }

}
