using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField]
    private PlayerTasks tasks;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tasks.touched = true;
        }
    }
}
