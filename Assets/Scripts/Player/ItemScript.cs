using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour //checks if the item was touched by player
{
    [SerializeField]
    private PlayerTasks tasks; //connects to player task script
    private void OnTriggerEnter(Collider other) //once trigger has been entered
    {
        if(other.tag == "Player") //by object tagged as player
        {
            tasks.touched = true; //set touched to true
        }
    }
}
