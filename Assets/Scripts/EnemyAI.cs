using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent NavComponent { get; private set; }
    public Renderer RenderComponent { get; private set; }
    public Animator anim {get; private set;}

    private BT bahaviourTree;

    public Vector3 currentTarget;

    public bool justAttacked = false;
    public float timer = 0;

    private void Start()
    {
        NavComponent = gameObject.GetComponent<NavMeshAgent>();
        bahaviourTree = new BT(this);
        RenderComponent = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        bahaviourTree.Update();

    }

}
