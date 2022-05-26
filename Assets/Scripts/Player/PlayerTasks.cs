using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTasks : MonoBehaviour //triggers a random key item to light up and need the player to find and touch it to complete the task
{
    public GameObject[] keyItems; //list of items
    public GameObject[] locations; //location for beam of light
    public Text textBox; //text box for player to be tolf of task
    public bool touched = false; //if the player has been touched by player

    private float timer; //timer between setting tasks
    private bool triggered = false; // is a task current active
    void Update()
    {
        timer += Time.deltaTime; //begin timer

        if(timer >= 30.0f && triggered == false) //if above timer and a task isnt active 
        {
            int ran = Random.Range(0, keyItems.Length); //create a random number within the amount of items

            textBox.text = "Go Touch " + keyItems[ran].name; //set the ui text to say what item needs to be touched
            locations[ran].SetActive(true); //set that items light as active
            triggered = true; //set trigger to true

        }

        if(touched == true) //if item is touched
        {
            foreach(GameObject l in locations) //loop through all items lights
            {
                l.SetActive(false); //set back to inactive
            }

            textBox.text = null; ///clear ui text

            timer = 0; //reset timer

            triggered = false; //reset triggered
            touched = false; //reset touched
        }
    }
}
