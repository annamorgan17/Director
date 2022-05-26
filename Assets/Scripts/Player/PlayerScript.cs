using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour //controls the player health and calculates how long the player has been sprinting
{
    public Slider healthBar; //slider which is the player current health

    public bool sprintedTooLong = false; //bool which is used to tell the diretcor the player has been sprinting for too long

    private float noiseValue = 0; //the current value of noise made by the player

    private FirstPersonController controller; //links to the first person controller script - not made by me - from the unity asset store

    private void Start()
    {
        controller = gameObject.GetComponent<FirstPersonController>(); //connects to the first person controller script
    }
    void Update()
    {
        healthBar.value = AIManager.GetPlayerHealth; //sets the health bar to the current player health

        noiseValue = (float)System.Math.Round(noiseValue, 2); //set the noise value to only 2 decimal points

        if (controller.isSprinting == true) //if the player is sprinting
        {
            float timer = 0; //set the timer to 0
            timer += (Time.deltaTime % 60); //add to it each second
            noiseValue += timer; //noise value = time
        }

        if(noiseValue > 0.9f) //if noise is about the timer of a full sprint bar
        {
            sprintedTooLong = true; //set to true
            noiseValue = 0; //reset noise value
        }

    }

    public void ReduceHealth() //reduces the player health by one whole number
    {
        AIManager.SetPlayerHealth = AIManager.GetPlayerHealth - 1;
    }

    private void OnTriggerEnter(Collider other) //if the player if touched by the creature reduce the player health
    {
        if(other.tag == "Creature")
        {
            ReduceHealth();
        }
    }

}
