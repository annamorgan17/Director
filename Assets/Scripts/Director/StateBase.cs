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

    public INTENSITY_STATE currentState;

    private float intensity;
    private float seenByPlayerTime;

    private void Update()
    {
        switch(currentState)
        {
            case INTENSITY_STATE.BUILD_UP://change wander target, speed
                {
                    break;
                }
            case INTENSITY_STATE.SUSTAIN_PEAK: //even closer target, hunt not wander
                {
                    break;
                }
            case INTENSITY_STATE.PEAK_FADE: //close area wander walk
                {
                    break;
                }
            case INTENSITY_STATE.RELAX: //normal wander until timer then hide
                {
                    break;
                }
        }
    }

    private void CalcIncreaseIntensity() // see the ai, ai in range, task set
    {
        //if in view
        if(Physics.Raycast(AIManager.GetPlayer.transform.position, AIManager.GetPlayer.transform.TransformDirection(Vector3.forward), out RaycastHit hit, AIManager.GetSightPlayerLength, LayerMask.GetMask("Creature")))
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
}
