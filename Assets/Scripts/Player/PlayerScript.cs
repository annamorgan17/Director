using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Slider healthBar;

    public bool sprintedTooLong = false;

    private float noiseValue = 0;

    private FirstPersonController controller;
    private PlayerTasks tasks;

    private void Start()
    {
        controller = gameObject.GetComponent<FirstPersonController>();
        tasks = gameObject.GetComponent<PlayerTasks>();
    }
    void Update()
    {
        healthBar.value = AIManager.GetPlayerHealth;

        noiseValue = (float)System.Math.Round(noiseValue, 2);

        if (controller.isSprinting == true)
        {
            float timer = 0;
            timer += (Time.deltaTime % 60);
            noiseValue += timer;
        }

        if(noiseValue > 0.9f)
        {
            sprintedTooLong = true;
            noiseValue = 0;
        }

    }

    public void ReduceHealth()
    {
        AIManager.SetPlayerHealth = AIManager.GetPlayerHealth - 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Creature")
        {
            ReduceHealth();
        }
    }

}
