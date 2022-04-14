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

    private void CalcIntensity() // see the ai, ai in range, ai not in range, task complete
    {
        if(Physics.Raycast(AIManager.GetPlayer.transform.position, AIManager.GetPlayer.transform.TransformDirection(Vector3.forward), out RaycastHit hit, AIManager.GetSightPlayerLength, LayerMask.GetMask("Creature")))
        {
            seenByPlayerTime += Time.deltaTime; // want mutliplier to increase with time after 5 seconds
            if(seenByPlayerTime == 5.0f)
            {
                intensity *= 0.5f;
            }
        }
        else
        {
            seenByPlayerTime = 0;
        }
    }
}
