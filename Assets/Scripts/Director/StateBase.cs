using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour //director, utility ai which sends instructions and changes values within the behaviour tree. Switches states based on intensity
                                       //which is calculated by interactions the player has
{
    public enum INTENSITY_STATE{  //states in which the director can be in
        RELAX,
        PEAK_FADE,
        SUSTAIN_PEAK,
        BUILD_UP
    };

    public INTENSITY_STATE currentState = INTENSITY_STATE.BUILD_UP; //current and starting state

    [SerializeField]
    private float intensity; //value used to change state
    private float seenByPlayerTime; //timer used for calculating intensity
    private PlayerScript player; //link to player script
    private PlayerTasks task; //link to task script

    private void Start()
    {
        player = AIManager.GetPlayer.gameObject.GetComponent<PlayerScript>(); //connection to player script
        task = AIManager.GetPlayer.gameObject.GetComponent<PlayerTasks>(); //connection to task script
    }

    private void Update()
    {
        CalcIncreaseIntensity(); //calulate the intensity increase
        CalcDecreaseIntensity(); //calculate the intensity decrease

        switch(currentState) //switch statement for the utility states
        {
            //low intensity
            case INTENSITY_STATE.BUILD_UP://normal wander but around player
                {
                    if(intensity >= AIManager.GetPeakIntensity)
                    {
                        currentState = INTENSITY_STATE.SUSTAIN_PEAK; //when intensity increases move up a state
                    }
                    AIManager.SetHideBool = false; // stops hide
                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = false; //stops close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().wanderTarget =  AIManager.GetPlayer.transform.position; //normal wander around player
                    AIManager.SetWalkSpeed = 7; //increase walk speed
                    AIManager.SetPassiveWander = AIManager.GetHuntWander; //decrease wander distance
                    break;
                }
            case INTENSITY_STATE.SUSTAIN_PEAK: //close wander around player
                {
                    if (intensity >= AIManager.GetFadeIntensity)
                    {
                        currentState = INTENSITY_STATE.PEAK_FADE;//when intensity increases move up a state
                    }
                    if (intensity <= AIManager.GetPeakIntensity)
                    {
                        currentState = INTENSITY_STATE.BUILD_UP; //when intensity decreases move down a state
                    }

                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = true; //allows close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().lastKnownLocation = AIManager.GetPlayer.transform.position; //wanders around player location
                    AIManager.SetRunSpeed = 9; //increase run speed
                    AIManager.SetPassiveWander = 90; //reset wander distance
                    break;
                }
            case INTENSITY_STATE.PEAK_FADE: //wide area hunt
                {
                    if (intensity >= AIManager.GetRelaxIntensity)
                    {
                        currentState = INTENSITY_STATE.RELAX;//when intensity increases move up a state
                    }
                    if (intensity <= AIManager.GetFadeIntensity)
                    {
                        currentState = INTENSITY_STATE.SUSTAIN_PEAK;//when intensity decreases move down a state
                    }
                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = true; //allows close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().lastKnownLocation = Vector3.zero; //wanders around centre
                    AIManager.SetHuntWander = 50; //increase hunt distance
                    break;
                }
                //high intensity
            case INTENSITY_STATE.RELAX: //hide for a timer then normal wander
                {
                    if (intensity <= AIManager.GetBuildIntensity)
                    {
                        currentState = INTENSITY_STATE.BUILD_UP; //when intensity decreases move down a state
                    }

                    AIManager.SetHideBool = true; // allows hide
                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = false; //stops close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().justAttacked = false; //stops idle
                    AIManager.SetRunSpeed = 6; //reset run speed
                    AIManager.SetWalkSpeed = 4; //reset walk speed
                    AIManager.SetHuntWander = 40; //reset hunt distance
                    break;
                }
        }
    }

    private void CalcIncreaseIntensity() 
    {
        //if in view
        if(VisionCheck() == true) //if in view for over 5 seconds increase intensity
        {
            seenByPlayerTime += Time.deltaTime; // want mutliplier to increase with time after 5 seconds
            if(seenByPlayerTime >= 5.0f) //when above 5 seconds
            {
                intensity = intensity + 0.5f; //increase intensity
            }
        }
        else 
        {
            seenByPlayerTime = 0; //reset timer
        }
        //if in range
        float distance = Vector3.Distance(AIManager.GetPlayer.transform.position, AIManager.GetAI.transform.position); //get distance from player to ai
        if(distance < AIManager.GetEnemyDistance) //if smaller 
        {
            intensity = intensity + 0.2f; //increase intensity
        }

        if(player.sprintedTooLong == true) //if the player has been sprinting for too long as calculated within player script
        {
            intensity = intensity + 0.7f; //increase intensity
            player.sprintedTooLong = false; //reset bool
        }
    }

    private void CalcDecreaseIntensity() 
    {
        //if in range
        float distance = Vector3.Distance(AIManager.GetPlayer.transform.position, AIManager.GetAI.transform.position); //distancr from player to ai
        if (distance > AIManager.GetCalmDistance) //if above
        {
            intensity = intensity - 0.2f; //lower intensity
        }
        //if player completes task
        if(task.touched)
        {
            intensity = intensity - 0.5f; //lower intensity
        }
    }

    private bool VisionCheck() //creates a vision cone from player to ai
    {
        Collider[] targetsInVR = Physics.OverlapSphere(AIManager.GetPlayer.transform.position, AIManager.GetSightPlayerLength, LayerMask.GetMask("Creature")); //get all colliders in radius labelled as creatre

        for (int i = 0; i < targetsInVR.Length; i++) //loop through those
        {
            Transform target = targetsInVR[i].transform; //set target to current
            Vector3 dirToTarget = (target.position - AIManager.GetPlayer.transform.position).normalized; //calculate direction from player to target
            if (Vector3.Angle(AIManager.GetPlayer.transform.forward, dirToTarget) < AIManager.GetSightPlayerAngle) //if within vision angle
            {
                float dstToTarget = Vector3.Distance(target.position, AIManager.GetPlayer.transform.position); //calculate distance from player to target
                if (!Physics.Raycast(AIManager.GetPlayer.transform.position, dirToTarget, dstToTarget, LayerMask.GetMask("Object"))) //if there are no objects in the way
                {
                    return true; //return true as seen
                }
            }
        }

        return false; //return false as not seen
    }
}
