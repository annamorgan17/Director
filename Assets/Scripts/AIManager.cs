using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager instance;

    [Space(20)]
    public float passiveWanderDistance;
    public float stoppingDistance;
    public GameObject setPoint; //test
    public GameObject player;
    public float playerSightLength;

    public static float GetPassiveWander { get { return instance.passiveWanderDistance; } }
    public static float GetStoppingDist { get { return instance.stoppingDistance; } }
    public static GameObject GetSetPoint { get { return instance.setPoint; } }
    public static GameObject GetPlayer { get { return instance.player; } }
    public static float GetSightPlayerLength { get { return instance.playerSightLength; } }


    private void Update()
    {
        instance = this;
    }
}

