using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTasks : MonoBehaviour
{
    public GameObject[] keyItems;
    public GameObject[] locations;
    public Text textBox;
    public bool touched = false;

    private float timer;
    private bool triggered = false;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 10.0f && triggered == false)
        {
            int ran = Random.Range(0, keyItems.Length);

            textBox.text = "Go Touch " + keyItems[ran].name;
            locations[ran].SetActive(true);
            triggered = true;

        }

        if(touched == true)
        {
            foreach(GameObject l in locations)
            {
                l.SetActive(false);
            }

            textBox.text = null;

            timer = 0;

            triggered = false;
            touched = false;
        }
    }
}
