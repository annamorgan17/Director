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
    private PlayerTasks tasks;

    private float intensity;
    private float seenByPlayerTime;

    private void Update()
    {
        switch(currentState)
        {
            //low intensity
            case INTENSITY_STATE.BUILD_UP://change wander target, speed
                {
                    if(intensity >= AIManager.GetPeakIntensity)
                    {
                        currentState = INTENSITY_STATE.SUSTAIN_PEAK;
                    }
                    break;
                }
            case INTENSITY_STATE.SUSTAIN_PEAK: //even closer target, hunt not wander
                {
                    if (intensity >= AIManager.GetFadeIntensity)
                    {
                        currentState = INTENSITY_STATE.PEAK_FADE;
                    }
                    break;
                }
            case INTENSITY_STATE.PEAK_FADE: //close area wander walk
                {
                    if (intensity >= AIManager.GetRelaxIntensity)
                    {
                        currentState = INTENSITY_STATE.RELAX;
                    }
                    break;
                }
                //high intensity
            case INTENSITY_STATE.RELAX: //normal wander until timer then hide
                {
                    if (intensity <= AIManager.GetBuildIntensity)
                    {
                        currentState = INTENSITY_STATE.BUILD_UP;
                    }
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
