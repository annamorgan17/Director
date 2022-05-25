using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    public enum INTENSITY_STATE{
        RELAX,
        PEAK_FADE,
        SUSTAIN_PEAK,
        BUILD_UP
    };

    public INTENSITY_STATE currentState = INTENSITY_STATE.BUILD_UP;

    [SerializeField]
    private float intensity;
    private float seenByPlayerTime;
    private PlayerScript player;

    private void Start()
    {
        player = AIManager.GetPlayer.gameObject.GetComponent<PlayerScript>();
    }

    private void Update()
    {
        CalcIncreaseIntensity();
        CalcDecreaseIntensity();

        switch(currentState)
        {
            //low intensity
            case INTENSITY_STATE.BUILD_UP://normal wander but around player
                {
                    if(intensity >= AIManager.GetPeakIntensity)
                    {
                        currentState = INTENSITY_STATE.SUSTAIN_PEAK;
                    }
                    AIManager.SetHideBool = false; // stops hide
                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = false; //stops close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().wanderTarget =  AIManager.GetPlayer.transform.position; //normal wander around player
                    AIManager.SetWalkSpeed = 7;
                    AIManager.SetPassiveWander = AIManager.GetHuntWander;
                    break;
                }
            case INTENSITY_STATE.SUSTAIN_PEAK: //close wander around player
                {
                    if (intensity >= AIManager.GetFadeIntensity)
                    {
                        currentState = INTENSITY_STATE.PEAK_FADE;
                    }
                    if (intensity <= AIManager.GetPeakIntensity)
                    {
                        currentState = INTENSITY_STATE.BUILD_UP;
                    }

                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = true; //allows close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().lastKnownLocation = AIManager.GetPlayer.transform.position; //wanders around player location
                    AIManager.SetRunSpeed = 9;
                    AIManager.SetPassiveWander = 90;
                    break;
                }
            case INTENSITY_STATE.PEAK_FADE: //wide area hunt
                {
                    if (intensity >= AIManager.GetRelaxIntensity)
                    {
                        currentState = INTENSITY_STATE.RELAX;
                    }
                    if (intensity <= AIManager.GetFadeIntensity)
                    {
                        currentState = INTENSITY_STATE.SUSTAIN_PEAK;
                    }
                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = true; //allows close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().lastKnownLocation = Vector3.zero; //wanders around centre
                    AIManager.SetHuntWander = 50;
                    break;
                }
                //high intensity
            case INTENSITY_STATE.RELAX: //hide for a timer then normal wander
                {
                    if (intensity <= AIManager.GetBuildIntensity)
                    {
                        currentState = INTENSITY_STATE.BUILD_UP;
                    }

                    AIManager.SetHideBool = true; // allows hide
                    AIManager.GetAI.GetComponent<EnemyAI>().hunt = false; //stops close wander
                    AIManager.GetAI.GetComponent<EnemyAI>().justAttacked = false; //stops idle
                    AIManager.SetRunSpeed = 6;
                    AIManager.SetWalkSpeed = 4;
                    AIManager.SetHuntWander = 40;
                    break;
                }
        }
    }

    private void CalcIncreaseIntensity() // see the ai, ai in range, task set
    {
        //if in view
        if(VisionCheck() == true)
        {
            seenByPlayerTime += Time.deltaTime; // want mutliplier to increase with time after 5 seconds
            if(seenByPlayerTime >= 5.0f)
            {
                intensity = intensity + 0.5f;
            }
        }
        else
        {
            seenByPlayerTime = 0;
        }
        //if in range
        float distance = Vector3.Distance(AIManager.GetPlayer.transform.position, AIManager.GetAI.transform.position);
        if(distance < AIManager.GetEnemyDistance)
        {
            intensity = intensity + 0.2f;
        }

        if(player.sprintedTooLong == true)
        {
            intensity = intensity + 0.7f;
            player.sprintedTooLong = false;
        }
    }

    private void CalcDecreaseIntensity() // ai not in range, task complete
    {
        //if in range
        float distance = Vector3.Distance(AIManager.GetPlayer.transform.position, AIManager.GetAI.transform.position);
        if (distance > AIManager.GetCalmDistance)
        {
            intensity = intensity - 0.2f;
        }
    }

    private bool VisionCheck()
    {
        Collider[] targetsInVR = Physics.OverlapSphere(AIManager.GetPlayer.transform.position, AIManager.GetSightPlayerLength, LayerMask.GetMask("Creature"));

        for (int i = 0; i < targetsInVR.Length; i++)
        {
            Transform target = targetsInVR[i].transform;
            Vector3 dirToTarget = (target.position - AIManager.GetPlayer.transform.position).normalized;
            if (Vector3.Angle(AIManager.GetPlayer.transform.forward, dirToTarget) < AIManager.GetSightPlayerAngle)
            {
                float dstToTarget = Vector3.Distance(target.position, AIManager.GetPlayer.transform.position);
                if (!Physics.Raycast(AIManager.GetPlayer.transform.position, dirToTarget, dstToTarget, LayerMask.GetMask("Object"))) //if there are no objects in the way
                {
                    return true;
                }
            }
        }

        return false;
    }
}
